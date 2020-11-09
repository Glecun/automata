using UnityEngine;

public class BackgroundGridController : MonoBehaviour
{
    private void Start()
    {
        setPosition(GameObject.Find("GameSceneController").GetComponent<GameSceneController>().grid);
    }

    // I didnt find how to set pivot to botomleft with tilemap, so i move the whole tilemap so that the bottom left is at 0,0 
    private void setPosition(GridScript gridScript)
    {
        transform.position = new Vector3(
            transform.position.x + (gridScript.width / 2),
            transform.position.y + (gridScript.height / 2),
            transform.position.z
        );
    }
}