using System.Collections.Generic;
using UnityEngine;

public class TownHall : MonoBehaviour
{
    public GameObject infoPopupPrefab;

    private GameSceneController gameSceneController;
    private ResourceStorage resourceStorage;
    private const int HEIGHT = 5;

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

    public void depositResource(ResourceAmount resourceAmount)
    {
        var currentResourceAmount = resourceStorage.get(resourceAmount.resourceEnum);
        currentResourceAmount.amount += resourceAmount.amount;
        resourceStorage.set(currentResourceAmount);
        InfoPopupController.Create(infoPopupPrefab, Utils.getTopPosition(transform, HEIGHT, 0.2f),
            "+" + resourceAmount.amount);
    }

    public ResourceAmount getResource(ResourceEnum resourceEnum)
    {
        return resourceStorage.get(resourceEnum);
    }
}