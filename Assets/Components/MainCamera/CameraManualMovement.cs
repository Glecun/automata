using UnityEngine;

public class CameraManualMovement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = new Vector3(0, 0);
        if (Input.GetKey(KeyCode.Z))
        {
            moveDir.y = +1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDir.y = -1;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            moveDir.x = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDir.x = +1;
        }

        float manualCameraSpeed = 80f;
        transform.position += moveDir * (manualCameraSpeed * Time.deltaTime);
    }
}