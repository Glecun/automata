using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow;

    private Vector3 cameraFollowPosition;
    private float zoom = 10f;

    private const float zoomChangeAmount = 150f;
    private const float edgeSize = 50f;
    private const float moveAmount = 30f;
    private const float zoomMin = 5f;
    private const float zoomMax = 10f;

    private void Awake()
    {
        cameraFollow.Setup(() => cameraFollowPosition, () => zoom, new Vector2(0, 22), new Vector2(0, 14));
    }

    private void Update()
    {
        HandleEdgeMovement();
        HandleZoom();
    }

    private void HandleZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            zoom -= zoomChangeAmount * Time.deltaTime;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            zoom += zoomChangeAmount * Time.deltaTime;
        }

        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
    }

    private void HandleEdgeMovement()
    {
        if (Input.mousePosition.x > Screen.width - edgeSize)
        {
            //EDGE RIGHT
            cameraFollowPosition.x += moveAmount * Time.deltaTime;
        }

        if (Input.mousePosition.x < edgeSize)
        {
            //EDGE LEFT
            cameraFollowPosition.x -= moveAmount * Time.deltaTime;
        }

        if (Input.mousePosition.y > Screen.height - edgeSize)
        {
            //EDGE UP
            cameraFollowPosition.y += moveAmount * Time.deltaTime;
        }

        if (Input.mousePosition.y < edgeSize)
        {
            //EDGE DOWN
            cameraFollowPosition.y -= moveAmount * Time.deltaTime;
        }
    }
}