using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundGridController : MonoBehaviour
{
    private void Start()
    {
        moveBackgroundToOrigin();
    }

    private void moveBackgroundToOrigin()
    {
        Tilemap tilemap = GetComponentsInChildren<Tilemap>()[0];
        var cellBounds = tilemap.cellBounds;
        var cellBottomLeft = new Vector3(cellBounds.min.x, cellBounds.min.y);
        var position = transform.position;
        position = new Vector3(position.x - cellBottomLeft.x, position.y - cellBottomLeft.y, position.z);
        transform.position = position;
    }
}