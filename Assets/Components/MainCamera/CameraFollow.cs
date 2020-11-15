using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera myCamera;
    private Func<Vector3> GetCameraFollowPositionFunc;
    private Func<float> GetCameraZoomFunc;
    private Vector2 xLimit;
    private Vector2 yLimit;

    private const float cameraZoomSpeed = 10f;
    private const float cameraMoveSpeed = 2f;

    public void Setup(Func<Vector3> GetCameraFollowPositionFunc, Func<float> GetCameraZoomFunc, Vector2 xLimit,
        Vector2 yLimit)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
        this.GetCameraZoomFunc = GetCameraZoomFunc;
        this.xLimit = xLimit;
        this.yLimit = yLimit;
    }

    private void Start()
    {
        myCamera = transform.GetComponent<Camera>();
    }

    public void SetCameraFollowPosition(Vector3 cameraFollowPosition)
    {
        SetGetCameraFollowPositionFunc(() => cameraFollowPosition);
    }

    public void SetGetCameraFollowPositionFunc(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }

    public void SetCameraZoom(float cameraZoom)
    {
        SetGetCameraZoomFunc(() => cameraZoom);
    }

    public void SetGetCameraZoomFunc(Func<float> GetCameraZoomFunc)
    {
        this.GetCameraZoomFunc = GetCameraZoomFunc;
    }


    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleZoom();

        HandleLimit();
    }

    private void HandleLimit()
    {
        Vector3 transformPosition;
        var position = transform.position;
        transformPosition.x = Mathf.Clamp(position.x, xLimit.x, xLimit.y);
        transformPosition.y = Mathf.Clamp(position.y, yLimit.x, yLimit.y);
        transformPosition.z = position.z;
        transform.position = transformPosition;
    }

    private void HandleMovement()
    {
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;

        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);

        if (distance > 0.1f)
        {
            Vector3 newCameraPosition =
                transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;

            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);

            if (distanceAfterMoving > distance)
            {
                // Overshot the target
                newCameraPosition = cameraFollowPosition;
            }

            transform.position = newCameraPosition;
        }
    }

    private void HandleZoom()
    {
        float cameraZoom = GetCameraZoomFunc();

        float cameraZoomDifference = cameraZoom - myCamera.orthographicSize;

        myCamera.orthographicSize += cameraZoomDifference * cameraZoomSpeed * Time.deltaTime;

        if (cameraZoomDifference > 0)
        {
            if (myCamera.orthographicSize > cameraZoom)
            {
                myCamera.orthographicSize = cameraZoom;
            }
        }
        else
        {
            if (myCamera.orthographicSize < cameraZoom)
            {
                myCamera.orthographicSize = cameraZoom;
            }
        }
    }
}