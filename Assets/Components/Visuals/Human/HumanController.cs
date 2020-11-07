using UnityEngine;
using static Utils;

public class HumanController : MonoBehaviour
{
    private HumanDecisionController humanDecisionController;
    private HumanActionController humanActionController;

    private Countdown betweenEachDecisionMaking;
    private const float durationBetweenEachDecisionMaking = 1f;

    private void Awake()
    {
        humanDecisionController = new HumanDecisionController();
        humanActionController = new HumanActionController(gameObject);

        betweenEachDecisionMaking = gameObject.AddComponent<Countdown>();
    }

    private void Update()
    {
        waitOrDo(updateDecision, betweenEachDecisionMaking, durationBetweenEachDecisionMaking);
    }

    private void updateDecision()
    {
        humanDecisionController.updateDecision();
        humanActionController.doAction(humanDecisionController.currentDecision);
    }
}