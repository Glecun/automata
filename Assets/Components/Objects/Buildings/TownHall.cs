using System.Collections.Generic;
using UnityEngine;

public class TownHall : MonoBehaviour
{
    [SerializeField] private GameObject infoPopupPrefab = null;
    [SerializeField] private GameObject humanPrefab = null;
    [SerializeField] private Sprite door_close = null;
    [SerializeField] private Sprite door_open = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    private GameSceneController gameSceneController;
    private ResourceStorage resourceStorage;
    private Countdown countdownBeforeCreateHuman;
    private const float countdownBeforeCreateHumanDuration = 20f;
    private Countdown countdownKeepDoorOpen;
    private const float countdownKeepDoorOpenDuration = 1f;
    private const int HEIGHT = 5;
    private const int WIDTH = 4;

    void Start()
    {
        spriteRenderer.sprite = door_close;
        gameSceneController = GameObject.Find("GameSceneController").GetComponent<GameSceneController>();
        var position = gameObject.transform.position;
        gameSceneController.grid.registerOnGrid(new GridObject(
            false,
            WIDTH,
            HEIGHT,
            (int) position.x,
            (int) position.y,
            new ReferenceToObject(new GOTTownHall(), this)
        ));

        resourceStorage = new ResourceStorage(new List<ResourceAmount>());

        countdownBeforeCreateHuman = gameObject.AddComponent<Countdown>();
        countdownKeepDoorOpen = gameObject.AddComponent<Countdown>();
    }

    private void Update()
    {
        void createHuman()
        {
            spriteRenderer.sprite = door_open;
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


        Utils.waitAndDo(() => { spriteRenderer.sprite = door_close; }, countdownKeepDoorOpen,
            countdownKeepDoorOpenDuration, spriteRenderer.sprite == door_open);
    }

    private Vector3 getDoorPosition()
    {
        var position = transform.position;
        return new Vector3(position.x + (float) WIDTH / 2 - 0.5f, position.y);
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