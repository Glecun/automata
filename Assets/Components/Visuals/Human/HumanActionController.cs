using UnityEngine;

public class HumanActionController : MonoBehaviour
{
    private readonly HumanWaitingAction humanWaitingAction = null;
    private readonly HumanGatherWoodAction humanGatherWoodAction = null;

    private Decision currentDecision;

    public void doAction(Decision decision)
    {
        if (currentDecision != decision)
        {
            removeAllActions();
            if (decision == Decision.WAITING)
                gameObject.AddComponent<HumanWaitingAction>();
            if (decision == Decision.GATHER_WOOD)
                gameObject.AddComponent<HumanGatherWoodAction>();

            currentDecision = decision;
        }
    }

    private void removeAllActions()
    {
        Destroy(humanWaitingAction);
        Destroy(humanGatherWoodAction);
    }
}