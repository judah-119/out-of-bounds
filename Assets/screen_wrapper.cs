using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 viewPos = transform.position;
        Vector3 camPos = mainCamera.transform.position;

        if (mainCamera.orthographic)
        {
            // Keep your old orthographic logic
            float camHeight = mainCamera.orthographicSize;
            float camWidth = camHeight * mainCamera.aspect;

            float minX = camPos.x - camWidth;
            float maxX = camPos.x + camWidth;
            float minY = camPos.y - camHeight;
            float maxY = camPos.y + camHeight;

            Wrap(ref viewPos, minX, maxX, minY, maxY);
        }
        else
        {
            // Perspective camera logic
            float distance = Mathf.Abs(viewPos.z - camPos.z);

            float frustumHeight = 2f * distance * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            float frustumWidth = frustumHeight * mainCamera.aspect;

            float minX = camPos.x - frustumWidth / 2f;
            float maxX = camPos.x + frustumWidth / 2f;
            float minY = camPos.y - frustumHeight / 2f;
            float maxY = camPos.y + frustumHeight / 2f;

            Wrap(ref viewPos, minX, maxX, minY, maxY);
        }

        transform.position = viewPos;
    }

    void Wrap(ref Vector3 pos, float minX, float maxX, float minY, float maxY)
    {
        if (pos.x > maxX)
            pos.x = minX;
        else if (pos.x < minX)
            pos.x = maxX;

        if (pos.y > maxY)
            pos.y = minY;
        else if (pos.y < minY)
            pos.y = maxY;
    }
}