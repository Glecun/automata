using UnityEngine;

public enum Decision
{
    NONE,
    JUST_BORN,
    WAITING,
    GATHER_WOOD
}

public class HumanDecisionController : MonoBehaviour
{
    private TownHall townHall;
    private bool justBornActionDone = false;

    private void Update()
    {
        initWhenFound();
        checkIfJustBornActionDone();
    }

    public Decision getNewDecision()
    {
        if (!justBornActionDone)
        {
            return Decision.JUST_BORN;
        }

        if (townHall != null && townHall.getResource(ResourceEnum.WOOD).amount <= 50)
        {
            return Decision.GATHER_WOOD;
        }

        return Decision.WAITING;
    }

    private void checkIfJustBornActionDone()
    {
        if (justBornActionDone) return;
        var humanJustBornAction = GetComponent<HumanJustBornAction>();
        if (humanJustBornAction != null && humanJustBornAction.isFinished())
        {
            justBornActionDone = true;
        }
    }

    private void initWhenFound()
    {
        if (townHall) return;
        var townHallGameObject = GameObject.Find("TownHall");
        townHall = townHallGameObject != null ? townHallGameObject.GetComponent<TownHall>() : null;
    }
}