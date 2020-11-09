using System;
using Unity.Mathematics;
using UnityEngine;
using static Utils;

public class HumanWaitingAction : MonoBehaviour
{
    private bool active;
    private HumanController humanController;
    private PathInProgress pathInProgress;

    private Countdown waitBeforeNextStep;
    private readonly Func<float> durationWaitBeforeNextStep = () => random(1, 3);

    private void Awake()
    {
        active = false;
        waitBeforeNextStep = gameObject.AddComponent<Countdown>();
        humanController = gameObject.GetComponent<HumanController>();
        pathInProgress = PathInProgress.NOT_MOVING;
    }

    void Update()
    {
        if (active)
        {
            waitOrDo(setRandomTargetPositionNearby, waitBeforeNextStep, durationWaitBeforeNextStep(),
                !pathInProgress.isMoving());
            transform.position = pathInProgress.changePosition(transform.position, humanController.speed);
        }
    }

    private void setRandomTargetPositionNearby()
    {
        var end = new int2(humanController.getPosition().x + random(-1, 1),
            humanController.getPosition().y + random(-1, 1));
        pathInProgress = humanController.goTo(end);
    }

    public void doAction()
    {
        active = true;
    }
}