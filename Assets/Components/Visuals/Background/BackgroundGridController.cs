using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGridController : MonoBehaviour
{

    public GameSceneController gameSceneController;
    private void Start()
    {
        gameSceneController = GameObject.Find("GameSceneController").GetComponent<GameSceneController>();
        transform.position = new Vector3(transform.position.x + (gameSceneController.grid.x / 2), transform.position.y + (gameSceneController.grid.y / 2), transform.position.z);
    }
}
