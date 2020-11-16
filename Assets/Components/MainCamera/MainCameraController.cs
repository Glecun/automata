using System;
using Unity.Mathematics;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] private ApplicationController applicationController;
    private Camera myCamera;
    private const float zoomChangeAmount = 100f;
    private const float edgeSize = 100f;
    private const float moveAmount = 5f;
    private const float accModifier = 0.1f;
    private const float zoomMin = 5f;
    private const float zoomMax = 10f;
    private Vector2 xLimit;
    private Vector2 yLimit;

    private void Start()
    {
        myCamera = transform.GetComponent<Camera>();
        var settings = applicationController.GetComponent<Settings>();
        xLimit = new Vector2(0, settings.numberOfCellInLine);
        yLimit = new Vector2(0, settings.numberOfCellInColumn);
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
            myCamera.orthographicSize -= zoomChangeAmount * Time.deltaTime;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            myCamera.orthographicSize += zoomChangeAmount * Time.deltaTime;
        }

        myCamera.orthographicSize = Mathf.Clamp(myCamera.orthographicSize, zoomMin, zoomMax);

        var xWorldSize = xLimit.y - xLimit.x;
        var yWorldSize = yLimit.y - yLimit.x;
        var maxZoomWidth = xWorldSize * ((float) Screen.height / Screen.width) / 2;
        var maxZoomHeight = yWorldSize / 2;
        var zoomMaxScreen = Math.Min(maxZoomHeight, maxZoomWidth);
        myCamera.orthographicSize = Mathf.Clamp(myCamera.orthographicSize, 0, zoomMaxScreen);
    }

    private void HandleEdgeMovement()
    {
        var tmp = transform.position;
        if (Input.mousePosition.x > Screen.width - edgeSize)
        {
            //EDGE RIGHT
            var acc = (Input.mousePosition.x - (Screen.width - edgeSize)) * accModifier;
            tmp.x += (moveAmount + acc) * Time.deltaTime;
        }

        if (Input.mousePosition.x < edgeSize)
        {
            //EDGE LEFT
            var acc = (edgeSize - Input.mousePosition.x) * accModifier;
            tmp.x -= (moveAmount + acc) * Time.deltaTime;
        }

        if (Input.mousePosition.y > Screen.height - edgeSize)
        {
            //EDGE UP
            var acc = (Input.mousePosition.y - (Screen.height - edgeSize)) * accModifier;
            tmp.y += (moveAmount + acc) * Time.deltaTime;
        }

        if (Input.mousePosition.y < edgeSize)
        {
            //EDGE DOWN
            var acc = (edgeSize - Input.mousePosition.y) * accModifier;
            tmp.y -= (moveAmount + acc) * Time.deltaTime;
        }

        transform.position = HandleLimit(tmp);
    }

    private Vector3 HandleLimit(Vector3 position)
    {
        var screenSize = getScreenSize();
        var halfScreenSizeX = screenSize.x / 2;
        var halfScreenSizeY = screenSize.y / 2;

        position.x = Mathf.Clamp(position.x, xLimit.x + halfScreenSizeX, xLimit.y - halfScreenSizeX);
        position.y = Mathf.Clamp(position.y, yLimit.x + halfScreenSizeY, yLimit.y - halfScreenSizeY);
        return position;
    }

    private float2 getScreenSize()
    {
        float height = myCamera.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;
        return new float2(width, height);
    }
}