using UnityEngine;

public class TownHall : MonoBehaviour
{
    private GameSceneController gameSceneController;

    void Start()
    {
        gameSceneController = GameObject.Find("GameSceneController").GetComponent<GameSceneController>();
        var position = gameObject.transform.position;
        gameSceneController.grid.registerOnGrid(new GridObject(false, 4, 5, (int) position.x, (int) position.y));
    }
}