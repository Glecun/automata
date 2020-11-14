﻿using System;
using Unity.Mathematics;
using UnityEngine;
using static Utils;

public class HumanWaitingAction : MonoBehaviour
{
    private HumanController humanController;
    private HumanAnimationController humanAnimationController;
    private PathInProgress pathInProgress;

    private Countdown waitBeforeNextStep;
    private readonly Func<float> durationWaitBeforeNextStep = () => random(1, 3);

    private void Awake()
    {
        humanAnimationController = gameObject.GetComponent<HumanAnimationController>();
        waitBeforeNextStep = gameObject.AddComponent<Countdown>();
        humanController = gameObject.GetComponent<HumanController>();
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
            humanController.humanMovementController.getPosition().x + random(-1, 1),
            humanController.humanMovementController.getPosition().y + random(-1, 1)
        );
        pathInProgress = humanController.humanMovementController.goTo(end);
    }

    public void destroy()
    {
        Destroy(waitBeforeNextStep);
        humanAnimationController.resetAnimations();
        Destroy(this);
    }
}