using System.Collections.Generic;
using UnityEngine;

public class TownHall : MonoBehaviour
{
    private GameSceneController gameSceneController;

    public ResourceStorage resourceStorage;

    void Start()
    {
        gameSceneController = GameObject.Find("GameSceneController").GetComponent<GameSceneController>();
        var position = gameObject.transform.position;
        gameSceneController.grid.registerOnGrid(new GridObject(
            false,
            4,
            5,
            (int) position.x,
            (int) position.y,
            new ReferenceToObject(new GOTTownHall(), this)
        ));

        resourceStorage = new ResourceStorage(new List<ResourceAmount>());
    }
}