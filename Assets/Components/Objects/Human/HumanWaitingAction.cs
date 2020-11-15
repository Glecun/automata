using System;
using Unity.Mathematics;
using UnityEngine;
using static Utils;

public class HumanWaitingAction : MonoBehaviour
{
    private HumanAnimationController humanAnimationController;
    private HumanMovementController humanMovementController;
    private PathInProgress pathInProgress;

    private Countdown waitBeforeNextStep;
    private readonly Func<float> durationWaitBeforeNextStep = () => random(1, 3);

    private void Awake()
    {
        humanAnimationController = gameObject.GetComponent<HumanAnimationController>();
        humanMovementController = gameObject.GetComponent<HumanMovementController>();
        waitBeforeNextStep = gameObject.AddComponent<Countdown>();
        pathInProgress = PathInProgress.NOT_MOVING;
    }

    void Update()
    {
        waitAndDo(setRandomTargetPositionNearby, waitBeforeNextStep, durationWaitBeforeNextStep(),
            !pathInProgress.isMoving());
        transform.position = pathInProgress.changePosition(transform.position, HumanController.speed);
        humanAnimationController.isMoving = pathInProgress.isMoving();
    }

    private void setRandomTargetPositionNearby()
    {
        var end = new int2(
            humanMovementController.getPosition().x + random(-1, 1),
            humanMovementController.getPosition().y + random(-1, 1)
        );
        pathInProgress = humanMovementController.goTo(end);
    }

    public void destroy()
    {
        Destroy(waitBeforeNextStep);
        humanAnimationController.resetAnimations();
        Destroy(this);
    }
}