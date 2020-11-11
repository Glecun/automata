using UnityEngine;

internal enum State
{
    GO_GATHER,
    RETURN_RESOURCE,
}

public class HumanGatherWoodAction : MonoBehaviour
{
    private HumanController humanController;
    private PathInProgress pathInProgress;
    private State state;

    private void Awake()
    {
        humanController = gameObject.GetComponent<HumanController>();
        pathInProgress = PathInProgress.NOT_MOVING;
        state = determineState();
    }

    private void Update()
    {
        if (state == State.GO_GATHER)
            goGather();

        transform.position = pathInProgress.changePosition(transform.position, HumanController.speed);
        humanController.humanAnimationController.isMoving = pathInProgress.isMoving();
    }

    private void goGather()
    {
        humanController.humanMovementController.getIfCloseTo()
        if (!pathInProgress.isMoving())
        {
            pathInProgress = humanController.humanMovementController.goToNearest(new GOTTree(TreeStateEnum.FULL));
        }
    }

    private State determineState()
    {
        if (humanController.humanResourceController.resourceStorage.get(ResourceEnum.WOOD).amount >= 10)
        {
            return State.RETURN_RESOURCE;
        }

        return State.GO_GATHER;
    }
}