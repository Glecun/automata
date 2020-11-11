using UnityEngine;

public enum Decision
{
    WAITING,
    GATHER_WOOD
}

public class HumanDecisionController : MonoBehaviour
{
    private TownHall townHall;

    private void Start()
    {
        townHall = GameObject.Find("TownHall").GetComponent<TownHall>();
    }

    public Decision getNewDecision()
    {
        if (townHall.resourceStorage.get(ResourceEnum.WOOD).amount <= 50)
        {
            return Decision.GATHER_WOOD;
        }

        return Decision.WAITING;
    }
}