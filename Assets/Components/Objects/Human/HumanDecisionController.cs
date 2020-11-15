using UnityEngine;

public enum Decision
{
    WAITING,
    GATHER_WOOD
}

public class HumanDecisionController : MonoBehaviour
{
    private TownHall townHall;

    private void Update()
    {
        initWhenFound();
    }

    public Decision getNewDecision()
    {
        if (townHall != null && townHall.getResource(ResourceEnum.WOOD).amount <= 50)
        {
            return Decision.GATHER_WOOD;
        }

        return Decision.WAITING;
    }


    private void initWhenFound()
    {
        if (townHall) return;
        var townHallGameObject = GameObject.Find("TownHall");
        townHall = townHallGameObject != null ? townHallGameObject.GetComponent<TownHall>() : null;
    }
}