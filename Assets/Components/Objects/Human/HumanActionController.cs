using UnityEngine;

public class HumanActionController : MonoBehaviour
{
    [SerializeField] private HumanDecisionController humanDecisionController = null;

    private HumanWaitingAction humanWaitingAction = null;
    private HumanGatherWoodAction humanGatherWoodAction = null;
    private HumanGatherFoodAction humanGatherFoodAction = null;
    private HumanJustBornAction humanJustBornAction = null;

    private Countdown betweenEachDecisionMaking;
    private const float durationBetweenEachDecisionMaking = 1f;

    public Decision currentDecision = Decision.NONE;

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
        doAction(humanDecisionController.getNewDecision(currentDecision));
    }

    public void doAction(Decision decision)
    {
        if (currentDecision != decision)
        {
            removeAllActions();
            switch (decision)
            {
                case Decision.JUST_BORN:
                    humanJustBornAction = gameObject.AddComponent<HumanJustBornAction>();
                    break;
                case Decision.WAITING:
                    humanWaitingAction = gameObject.AddComponent<HumanWaitingAction>();
                    break;
                case Decision.GATHER_WOOD:
                    humanGatherWoodAction = gameObject.AddComponent<HumanGatherWoodAction>();
                    break;
                case Decision.GATHER_FOOD:
                    humanGatherFoodAction = gameObject.AddComponent<HumanGatherFoodAction>();
                    break;
            }

            currentDecision = decision;
        }
    }

    private void removeAllActions()
    {
        if (humanWaitingAction != null)
            humanWaitingAction.destroy();
        if (humanGatherWoodAction != null)
            humanGatherWoodAction.destroy();
        if (humanJustBornAction != null)
            Destroy(humanJustBornAction);
        if (humanGatherFoodAction != null)
            humanGatherFoodAction.destroy();
    }
}