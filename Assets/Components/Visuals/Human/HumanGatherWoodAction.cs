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
        if (state == State.RETURN_RESOURCE)
            goReturnResource();

        state = determineState();

        transform.position = pathInProgress.changePosition(transform.position, HumanController.speed);
        humanController.humanAnimationController.isMoving = pathInProgress.isMoving();
    }

    private void goReturnResource()
    {
        if (!pathInProgress.isMoving())
        {
            var townHallController = (TownHall) humanController.humanMovementController.getIfInRange(new GOTTownHall());
            if (townHallController != null)
            {
                humanController.humanResourceController.resourceStorage.set(new ResourceAmount(0, ResourceEnum.WOOD));
                //TODO a modifier
                townHallController.resourceStorage.set(
                    humanController.humanResourceController.resourceStorage.get(ResourceEnum.WOOD));
            }

            pathInProgress = humanController.humanMovementController.goToNearest(new GOTTownHall());
        }
    }

    private void goGather()
    {
        if (!pathInProgress.isMoving())
        {
            var treeController =
                (TreeController) humanController.humanMovementController.getIfInRange(new GOTTree(TreeStateEnum.FULL));
            if (treeController != null)
            {
                humanController.humanResourceController.resourceStorage.set(treeController.RetrieveResourceAmount());
            }
            else
            {
                pathInProgress = humanController.humanMovementController.goToNearest(new GOTTree(TreeStateEnum.FULL));
            }
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