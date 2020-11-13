using System;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : MonoBehaviour
{
    [SerializeField] private GameObject infoPopupPrefab = null;
    [SerializeField] private GameObject humanPrefab = null;

    private GameSceneController gameSceneController;
    private ResourceStorage resourceStorage;
    private Countdown countdownBeforeCreateHuman;
    private float countdownBeforeCreateHumanDuration = 20f;
    private const int HEIGHT = 5;
    private const int WIDTH = 5;

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

        countdownBeforeCreateHuman = gameObject.AddComponent<Countdown>();
    }

    private void Update()
    {
        void createHuman()
        {
            InstantiateUtils.Instantiate(humanPrefab, getDoorPosition(), Quaternion.identity);
        }

        if (resourceStorage.get(ResourceEnum.WOOD).amount >= 10 && !countdownBeforeCreateHuman.isCountingDown)
        {
            InfoPopupController.Create(infoPopupPrefab, Utils.getTopPosition(transform, HEIGHT, 0.7f),
                "Création d'un humain");
            var currentResourceAmount = resourceStorage.get(ResourceEnum.WOOD);
            currentResourceAmount.amount -= 10;
            resourceStorage.set(currentResourceAmount);
            Utils.waitAndDo(createHuman, countdownBeforeCreateHuman, countdownBeforeCreateHumanDuration, true);
        }
    }

    private Vector3 getDoorPosition()
    {
        return new Vector3(transform.position.x + (float) Math.Ceiling(WIDTH / 2m), transform.position.y);
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