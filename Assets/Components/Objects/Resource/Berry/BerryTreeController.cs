using System;
using UnityEngine;

public class BerryTreeController : MonoBehaviour, IHarvestable
{
    public ResourceAmount resourceAmount;

    [SerializeField] private Sprite berryEmpty = null;
    [SerializeField] private Sprite initialSprite = null;
    [SerializeField] private GameObject infoPopupPrefab = null;

    private MonoBehaviour whoIsCurrentlyCutting = null;
    private TreeStateEnum _currentTreeState;
    private Countdown countdown;
    private SpriteRenderer spriteRenderer;
    private const float durationOfGrowing = 10f;
    private GameSceneController gameSceneController;
    private const int HEIGHT = 1;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = initialSprite;

        _currentTreeState = TreeStateEnum.FULL;

        countdown = gameObject.AddComponent<Countdown>();

        resourceAmount = new ResourceAmount(10, ResourceEnum.FOOD);
    }

    private void Start()
    {
        gameSceneController = GameObject.Find("GameSceneController").GetComponent<GameSceneController>();
        registerOnGrid();
    }

    public ResourceAmount RetrieveResourceAmount()
    {
        if (_currentTreeState != TreeStateEnum.FULL)
        {
            throw new Exception("Trying to cut a Berry Tree not full");
        }

        var resourceAmountToRetrieve = resourceAmount.copy();
        resourceAmount = new ResourceAmount(0, ResourceEnum.FOOD);
        setCurrentState(TreeStateEnum.CUT);
        spriteRenderer.sprite = berryEmpty;
        InfoPopupController.Create(infoPopupPrefab, Utils.getTopPosition(transform, HEIGHT, 0.2f),
            "-" + resourceAmountToRetrieve.amount);
        return resourceAmountToRetrieve;
    }

    private void Update()
    {
        if (_currentTreeState == TreeStateEnum.CUT)
        {
            setCurrentState(TreeStateEnum.GROWING);
            countdown.wait(durationOfGrowing);
        }

        if (hasFinishedGrowing())
        {
            setCurrentState(TreeStateEnum.FULL);
            resourceAmount = new ResourceAmount(10, ResourceEnum.FOOD);
            spriteRenderer.sprite = initialSprite;
        }
    }

    private bool hasFinishedGrowing()
    {
        return _currentTreeState == TreeStateEnum.GROWING && !countdown.isCountingDown;
    }

    private void setCurrentState(TreeStateEnum treeStateEnum)
    {
        _currentTreeState = treeStateEnum;
        registerOnGrid();
    }

    private void registerOnGrid()
    {
        var position = gameObject.transform.position;
        gameSceneController.grid.registerOnGrid(new GridObject(
            false,
            (int) position.x,
            (int) position.y,
            new ReferenceToObject(new GOTBerryTree(_currentTreeState, whoIsCurrentlyCutting), this)
        ));
    }

    public void setWhoIsCurrentlyCutting(MonoBehaviour newWhoIsCurrentlyCutting)
    {
        whoIsCurrentlyCutting = newWhoIsCurrentlyCutting;
        registerOnGrid();
    }
}