using UnityEngine;

internal enum State
{
    GO_GATHER,
    RETURN_RESOURCE,
}

public class HumanGatherWoodAction : MonoBehaviour
{
    private HumanController humanController;
    private HumanAnimationController humanAnimationController;
    private HumanMovementController humanMovementController;

    private PathInProgress pathInProgress;
    private State state;
    private TreeController currentlyCuttingTree;

    private Countdown timeToGatherCountdown;
    private float durationTimeToGather = 3f;
    private Countdown timeToDepositCountdown;
    private float durationTimeToDeposit = 3f;

    private void Awake()
    {
        humanController = gameObject.GetComponent<HumanController>();
        humanAnimationController = gameObject.GetComponent<HumanAnimationController>();
        humanMovementController = gameObject.GetComponent<HumanMovementController>();
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
        humanAnimationController.isMoving = pathInProgress.isMoving();
    }

    private void goReturnResource()
    {
        if (!pathInProgress.isMoving())
        {
            var townHallController = (TownHall) humanMovementController.getIfInRange(new GOTTownHall());
            if (townHallController != null)
            {
                void depositResource()
                {
                    humanAnimationController.isDoing = false;
                    var resourceAmount = humanController.humanResourceController.resourceStorage.get(ResourceEnum.WOOD);
                    townHallController.depositResource(resourceAmount);
                    humanController.humanResourceController.resourceStorage.set(
                        new ResourceAmount(0, ResourceEnum.WOOD));
                    InfoPopupController.Create(humanController.infoPopupPrefab,
                        humanMovementController.getTopPosition(0.2f), "-" + resourceAmount.amount);
                }

                humanAnimationController.isDoing = true;
                Utils.waitAndDo(depositResource, timeToDepositCountdown, durationTimeToDeposit, true);
            }
            else
            {
                pathInProgress = humanMovementController.goToNearest(new GOTTownHall());
            }
        }
    }

    private void goGather()
    {
        if (!pathInProgress.isMoving())
        {
            var treeController = getNearestTreeMaybeCurrentlyCut();
            if (treeController != null)
            {
                void getResource()
                {
                    humanAnimationController.isDoing = false;
                    treeController.setWhoIsCurrentlyCutting(null);
                    currentlyCuttingTree = null;
                    var resourceAmount = treeController.RetrieveResourceAmount();
                    humanController.humanResourceController.resourceStorage.set(resourceAmount);
                    InfoPopupController.Create(humanController.infoPopupPrefab,
                        humanMovementController.getTopPosition(0.2f), "+" + resourceAmount.amount);
                }

                humanAnimationController.isDoing = true;
                treeController.setWhoIsCurrentlyCutting(this);
                currentlyCuttingTree = treeController;
                Utils.waitAndDo(getResource, timeToGatherCountdown, durationTimeToGather, true);
            }
            else
            {
                pathInProgress = humanMovementController.goToNearest(new GOTTree(TreeStateEnum.FULL, null));
            }
        }
    }

    private TreeController getNearestTreeMaybeCurrentlyCut()
    {
        if (humanAnimationController.isDoing)
        {
            return (TreeController) humanMovementController.getIfInRange(new GOTTree(TreeStateEnum.FULL,
                this));
        }

        return (TreeController) humanMovementController.getIfInRange(new GOTTree(TreeStateEnum.FULL,
            null));
    }

    private State determineState()
    {
        if (humanController.humanResourceController.resourceStorage.get(ResourceEnum.WOOD).amount >= 10)
        {
            return State.RETURN_RESOURCE;
        }

        return State.GO_GATHER;
    }

    public void destroy()
    {
        Destroy(timeToDepositCountdown);
        Destroy(timeToGatherCountdown);
        humanAnimationController.resetAnimations();
        if (currentlyCuttingTree != null)
            currentlyCuttingTree.setWhoIsCurrentlyCutting(null);
        Destroy(this);
    }
}