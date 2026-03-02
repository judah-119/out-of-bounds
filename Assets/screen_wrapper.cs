using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 screenBounds;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 viewPos = transform.position;

        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        Vector3 camPos = mainCamera.transform.position;

        float minX = camPos.x - camWidth;
        float maxX = camPos.x + camWidth;
        float minY = camPos.y - camHeight;
        float maxY = camPos.y + camHeight;

        if (viewPos.x > maxX)
            viewPos.x = minX;
        else if (viewPos.x < minX)
            viewPos.x = maxX;

        if (viewPos.y > maxY)
            viewPos.y = minY;
        else if (viewPos.y < minY)
            viewPos.y = maxY;

        transform.position = viewPos;
    }
}