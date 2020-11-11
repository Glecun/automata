using System;
using UnityEngine;
using static Utils;

public class HumanController : MonoBehaviour
{
    public HumanAnimationController humanAnimationController;
    public Animator animator = null;
    private HumanDecisionController humanDecisionController;
    private HumanActionController humanActionController;
    public HumanMovementController humanMovementController;
    public HumanResourceController humanResourceController;

    private Countdown betweenEachDecisionMaking;
    private const float durationBetweenEachDecisionMaking = 1f;


    [NonSerialized] public const float speed = 4f;

    private void Awake()
    {
        humanAnimationController = new HumanAnimationController(animator);
        humanResourceController = new HumanResourceController();
        humanMovementController = gameObject.AddComponent<HumanMovementController>();

        humanDecisionController = gameObject.AddComponent<HumanDecisionController>();
        humanActionController = gameObject.AddComponent<HumanActionController>();
        betweenEachDecisionMaking = gameObject.AddComponent<Countdown>();
    }

    private void Update()
    {
        waitOrDo(updateDecision, betweenEachDecisionMaking, durationBetweenEachDecisionMaking);
        humanAnimationController.updateAnimations();
    }

    private void updateDecision()
    {
        humanActionController.doAction(humanDecisionController.getNewDecision());
    }
}