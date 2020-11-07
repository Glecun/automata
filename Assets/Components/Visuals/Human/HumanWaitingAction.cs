using System;
using UnityEngine;
using static Utils;

public class HumanWaitingAction : MonoBehaviour
{
    private Boolean active;

    private Countdown waitBeforeNextStep;
    private readonly Func<float> durationWaitBeforeNextStep = () => random(1, 4);

    private void Awake()
    {
        active = false;
        waitBeforeNextStep = gameObject.AddComponent<Countdown>();
    }

    void Update()
    {
        if (active)
        {
            waitOrDo(move, waitBeforeNextStep, durationWaitBeforeNextStep());
        }
    }

    private void move()
    {
        Debug.Log("move !");
    }

    public void doAction()
    {
        active = true;
    }
}