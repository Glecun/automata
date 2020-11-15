using UnityEngine;

public class HumanActionController : MonoBehaviour
{
    [SerializeField] private HumanDecisionController humanDecisionController = null;

    private HumanWaitingAction humanWaitingAction = null;
    private HumanGatherWoodAction humanGatherWoodAction = null;

    private Countdown betweenEachDecisionMaking;
    private const float durationBetweenEachDecisionMaking = 1f;

    private Decision currentDecision;

    private void Awake()
    {
        betweenEachDecisionMaking = gameObject.AddComponent<Countdown>();
    }

    private void Update()
    {
        Utils.doAndWait(updateDecision, betweenEachDecisionMaking, durationBetweenEachDecisionMaking);
    }

    private void updateDecision()
    {
        doAction(humanDecisionController.getNewDecision());
    }

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