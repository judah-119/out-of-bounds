using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollowAndConfine2D : MonoBehaviour
{
    public Transform target;

    [Header("Follow Settings")]
    public float smoothTime = 0.2f;
    public Vector2 offset;

    [Header("Bounds")]
    public BoxCollider2D boundsCollider;

    private Camera cam;
    private Vector3 velocity = Vector3.zero;

    private float camHeight;
    private float camWidth;

    void Start()
    {
        cam = GetComponent<Camera>();

        camHeight = cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
    }

    void LateUpdate()
    {
        FollowTarget();
        ConfineCamera();
    }

    void FollowTarget()
    {
        Vector3 targetPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z
        );

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );
    }

    void ConfineCamera()
    {
        Bounds bounds = boundsCollider.bounds;

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x,
            bounds.min.x + camWidth,
            bounds.max.x - camWidth);

        pos.y = Mathf.Clamp(pos.y,
            bounds.min.y + camHeight,
            bounds.max.y - camHeight);

        transform.position = pos;
    }
}