using System;
using UnityEngine;
using Random = System.Random;

public enum TreeStateEnum
{
    CUT,
    GROWING,
    FULL
}

public class TreeController : MonoBehaviour, IHarvestable
{
    public ResourceAmount resourceAmount;

    [SerializeField] private Sprite tree_cut = null;
    [SerializeField] private Sprite[] treeList = null;
    [SerializeField] private GameObject infoPopupPrefab = null;
    private Sprite initialSprite;
    private TreeStateEnum _currentTreeState;
    private Countdown countdown;
    private SpriteRenderer spriteRenderer;
    private const float durationOfGrowing = 10f;
    private GameSceneController gameSceneController;
    private const int HEIGHT = 2;

    private void Awake()
    {
        initialSprite = getRandomTreeSprite();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = initialSprite;

        _currentTreeState = TreeStateEnum.FULL;

        countdown = gameObject.AddComponent<Countdown>();

        resourceAmount = new ResourceAmount(10, ResourceEnum.WOOD);
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
            throw new Exception("Trying to cut a Tree not full");
        }

        var resourceAmountToRetrieve = resourceAmount.copy();
        resourceAmount = new ResourceAmount(0, ResourceEnum.WOOD);
        setCurrentState(TreeStateEnum.CUT);
        GetComponent<SpriteRenderer>().sprite = tree_cut;
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
            resourceAmount = new ResourceAmount(10, ResourceEnum.WOOD);
            spriteRenderer.sprite = initialSprite;
        }
    }

    private bool hasFinishedGrowing()
    {
        return _currentTreeState == TreeStateEnum.GROWING && !countdown.isCountingDown;
    }

    private Sprite getRandomTreeSprite()
    {
        var rnd = new Random();
        return treeList[rnd.Next(0, treeList.Length)];
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
            new ReferenceToObject(new GOTTree(_currentTreeState), this)
        ));
    }
}