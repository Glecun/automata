using UnityEngine;

public class HumanActionController
{
    private readonly HumanWaitingAction humanWaitingAction;

    public HumanActionController(GameObject gameObject)
    {
        humanWaitingAction = gameObject.AddComponent<HumanWaitingAction>();
    }

    public void doAction(Decision action)
    {
        if (action == Decision.WAITING)
            humanWaitingAction.doAction();
    }
}