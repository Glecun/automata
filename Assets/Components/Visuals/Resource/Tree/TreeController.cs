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
    private Sprite initialSprite;
    private TreeStateEnum _currentTreeState;
    private Countdown countdown;
    private SpriteRenderer spriteRenderer;
    private const float durationOfGrowing = 3f;
    private GameSceneController gameSceneController;

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
        var resourceAmountToRetrieve = resourceAmount;
        resourceAmount = new ResourceAmount(0, ResourceEnum.WOOD);
        setCurrentState(TreeStateEnum.CUT);
        GetComponent<SpriteRenderer>().sprite = tree_cut;
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
        gameSceneController.grid.registerOnGrid(new GridObject(false, (int) position.x, (int) position.y,
            new GOTTree(_currentTreeState)));
    }
}