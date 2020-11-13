using UnityEngine;

public class HumanActionController : MonoBehaviour
{
    private HumanWaitingAction humanWaitingAction = null;
    private HumanGatherWoodAction humanGatherWoodAction = null;

    private Decision currentDecision;

    public void doAction(Decision decision)
    {
        if (currentDecision != decision)
        {
            removeAllActions();
            if (decision == Decision.WAITING)
                humanWaitingAction = gameObject.AddComponent<HumanWaitingAction>();
            if (decision == Decision.GATHER_WOOD)
                humanGatherWoodAction = gameObject.AddComponent<HumanGatherWoodAction>();

            currentDecision = decision;
        }
    }

    private void removeAllActions()
    {
        if (humanWaitingAction != null)
            humanWaitingAction.destroy();
        if (humanGatherWoodAction != null)
            humanGatherWoodAction.destroy();
    }
}