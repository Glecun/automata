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

    private Countdown timeToGatherCountdown;
    private float durationTimeToGather = 3f;
    private Countdown timeToDepositCountdown;
    private float durationTimeToDeposit = 3f;

    private void Awake()
    {
        humanController = gameObject.GetComponent<HumanController>();
        pathInProgress = PathInProgress.NOT_MOVING;
        state = determineState();
        timeToGatherCountdown = gameObject.AddComponent<Countdown>();
        timeToDepositCountdown = gameObject.AddComponent<Countdown>();
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
                void depositResource()
                {
                    humanController.humanAnimationController.isDoing = false;
                    var resourceAmount = humanController.humanResourceController.resourceStorage.get(ResourceEnum.WOOD);
                    townHallController.depositResource(resourceAmount);
                    humanController.humanResourceController.resourceStorage.set(
                        new ResourceAmount(0, ResourceEnum.WOOD));
                    InfoPopupController.Create(humanController.infoPopupPrefab,
                        humanController.humanMovementController.getTopPosition(0.2f), "-" + resourceAmount.amount);
                }

                humanController.humanAnimationController.isDoing = true;
                Utils.waitAndDo(depositResource, timeToDepositCountdown, durationTimeToDeposit, true);
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
                void getResource()
                {
                    humanController.humanAnimationController.isDoing = false;
                    var resourceAmount = treeController.RetrieveResourceAmount();
                    humanController.humanResourceController.resourceStorage.set(resourceAmount);
                    InfoPopupController.Create(humanController.infoPopupPrefab,
                        humanController.humanMovementController.getTopPosition(0.2f), "+" + resourceAmount.amount);
                }

                humanController.humanAnimationController.isDoing = true;
                Utils.waitAndDo(getResource, timeToGatherCountdown, durationTimeToGather, true);
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