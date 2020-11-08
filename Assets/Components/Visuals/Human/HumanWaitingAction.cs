using System;
using Unity.Mathematics;
using UnityEngine;
using static Utils;

public class HumanWaitingAction : MonoBehaviour
{
    private bool active;

    private Countdown waitBeforeNextStep;
    private readonly Func<float> durationWaitBeforeNextStep = () => random(1, 3);
    private PathInProgress pathInProgress;

    private Pathfinding pathfinding;

    // TODO: should not be here
    private float speed = 7f;

    private void Awake()
    {
        active = false;
        waitBeforeNextStep = gameObject.AddComponent<Countdown>();
    }

    private void Start()
    {
        //TODO here ? or HumanController ?
        var grid = GameObject.Find("GameSceneController").GetComponent<GameSceneController>().grid;

        //TODO: move from here ?
        pathfinding = grid.generatePathfinding();
    }

    void Update()
    {
        if (active)
        {
            //TODO : should start timer only when finishing movement
            waitOrDo(setRandomTargetPositionNearby, waitBeforeNextStep, durationWaitBeforeNextStep());
            transform.position = pathInProgress.changePosition(transform.position, speed);
        }
    }

    private void setRandomTargetPositionNearby()
    {
        var start = getPosition();
        var end = new int2(getPosition().x + random(-1, 1), getPosition().y + random(-1, 1));
        pathInProgress = PathInProgress.setPath(start, end, pathfinding);
    }

    public void doAction()
    {
        active = true;
    }

    //TODO should be in Human controller ?
    private int2 getPosition()
    {
        return new int2((int) gameObject.transform.position.x, (int) gameObject.transform.position.y);
    }
}