using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    void Start()
    {
        float aspectRatio = Camera.main.aspect; //(width divided by height)
        float camSize = Camera.main.orthographicSize; //The size value mentioned earlier
        float correctPositionX = aspectRatio * camSize;
        Camera.main.transform.position = new Vector3(correctPositionX, camSize, -10);
    }
}
