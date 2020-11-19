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
    private bool retry = true;

    private Countdown timeToGatherCountdown;
    private float durationTimeToGather = 3f;
    private Countdown timeToDepositCountdown;
    private float durationTimeToDeposit = 3f;
    private Countdown timeBeforeRetryCountdown;
    private float durationTimeBeforeRetry = 1f;

    private void Awake()
    {
        humanController = gameObject.GetComponent<HumanController>();
        humanAnimationController = gameObject.GetComponent<HumanAnimationController>();
        humanMovementController = gameObject.GetComponent<HumanMovementController>();
        pathInProgress = PathInProgress.NOT_MOVING;
        state = determineState();
        timeToGatherCountdown = gameObject.AddComponent<Countdown>();
        timeToDepositCountdown = gameObject.AddComponent<Countdown>();
        timeBeforeRetryCountdown = gameObject.AddComponent<Countdown>();
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
        if (!pathInProgress.isMoving() && retry)
        {
            var townHallController = (TownHall) humanMovementController.getIfInRange(new GOTTownHall());
            if (townHallController != null)
            {
                depositWoodInto(townHallController);
            }
            else
            {
                pathInProgress = humanMovementController.goToNearest(new GOTTownHall());
                if (!pathInProgress.isMoving())
                {
                    waitBeforeRetry();
                }
            }
        }
    }

    private void goGather()
    {
        if (!pathInProgress.isMoving() && retry)
        {
            var treeController = getNearestTreeMaybeCurrentlyCut();
            if (treeController != null)
            {
                cutTree(treeController);
            }
            else
            {
                pathInProgress = humanMovementController.goToNearest(new GOTTree(TreeStateEnum.FULL, null));
                if (!pathInProgress.isMoving())
                {
                    waitBeforeRetry();
                }
            }
        }
    }

    private void waitBeforeRetry()
    {
        retry = false;
        Utils.waitAndDo(() => retry = true, timeBeforeRetryCountdown, durationTimeBeforeRetry, true);
    }

    private void depositWoodInto(TownHall townHallController)
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

    private void cutTree(TreeController treeController)
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