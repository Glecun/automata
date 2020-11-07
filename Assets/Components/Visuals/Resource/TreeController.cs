using UnityEngine;
using Random = System.Random;

internal enum StateEnum 
{
    CUT,
    GROWING,
    FULL
}

public class TreeController : MonoBehaviour , IHarvestable
{

    public ResourceAmount resourceAmount;

    [SerializeField]
    private Sprite tree_cut;
    [SerializeField]
    private Sprite[] treeList;
    private Sprite intialSprite;
    private StateEnum currentState;
    private Countdown countdown;
    private SpriteRenderer spriteRenderer;
    private const int durationOfGrowing = 3;

    private void Awake()
    {
        intialSprite = getRandomTreeSprite();
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = intialSprite;
        
        currentState = StateEnum.FULL;
        
        countdown = gameObject.AddComponent<Countdown>();
        
        resourceAmount = new ResourceAmount(10, ResourceEnum.WOOD);
    }

    public ResourceAmount RetrieveResourceAmount()
    {
        var resourceAmountToRetrieve = resourceAmount;
        resourceAmount = new ResourceAmount(0, ResourceEnum.WOOD);
        currentState = StateEnum.CUT;
        GetComponent<SpriteRenderer>().sprite = tree_cut;
        return resourceAmountToRetrieve;
    }
    
    private void Update()
    {
        if (currentState == StateEnum.CUT)
        {
            currentState = StateEnum.GROWING;
            countdown.of(durationOfGrowing);
        }

        if (hasFinishedGrowing())
        {
            currentState = StateEnum.FULL;
            spriteRenderer.sprite = intialSprite;
        }
    }

    private bool hasFinishedGrowing()
    {
        return currentState == StateEnum.GROWING && !countdown.isCountingDown;
    }
    
    private Sprite getRandomTreeSprite()
    {
        var rnd = new Random();
        return treeList[rnd.Next(0, treeList.Length)];
    }
}
