using UnityEngine;

public class HumanGatherFoodAction : MonoBehaviour
{
    internal enum State
    {
        GO_GATHER,
        RETURN_RESOURCE,
    }

    private HumanController humanController;
    private HumanAnimationController humanAnimationController;
    private HumanMovementController humanMovementController;

    private PathInProgress pathInProgress;
    private State state;
    private BerryTreeController currentlyCuttingBerryTree;
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
                depositFoodInto(townHallController);
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
            var berryTreeController = getNearestBerryTreeMaybeCurrentlyCut();
            if (berryTreeController != null)
            {
                cutBerryTree(berryTreeController);
            }
            else
            {
                pathInProgress = humanMovementController.goToNearest(new GOTBerryTree(TreeStateEnum.FULL, null));
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

    private void depositFoodInto(TownHall townHallController)
    {
        void depositResource()
        {
            humanAnimationController.isDoing = false;
            var resourceAmount = humanController.humanResourceController.resourceStorage.get(ResourceEnum.FOOD);
            townHallController.depositResource(resourceAmount);
            humanController.humanResourceController.resourceStorage.set(new ResourceAmount(0, ResourceEnum.FOOD));
            InfoPopupController.Create(humanController.infoPopupPrefab,
                humanMovementController.getTopPosition(0.2f), "-" + resourceAmount.amount);
        }

        humanAnimationController.isDoing = true;
        Utils.waitAndDo(depositResource, timeToDepositCountdown, durationTimeToDeposit, true);
    }

    private void cutBerryTree(BerryTreeController berryTreeController)
    {
        void getResource()
        {
            humanAnimationController.isDoing = false;
            berryTreeController.setWhoIsCurrentlyCutting(null);
            currentlyCuttingBerryTree = null;
            var resourceAmount = berryTreeController.RetrieveResourceAmount();
            humanController.humanResourceController.resourceStorage.set(resourceAmount);
            InfoPopupController.Create(humanController.infoPopupPrefab,
                humanMovementController.getTopPosition(0.2f), "+" + resourceAmount.amount);
        }

        humanAnimationController.isDoing = true;
        berryTreeController.setWhoIsCurrentlyCutting(this);
        currentlyCuttingBerryTree = berryTreeController;
        Utils.waitAndDo(getResource, timeToGatherCountdown, durationTimeToGather, true);
    }

    private BerryTreeController getNearestBerryTreeMaybeCurrentlyCut()
    {
        if (humanAnimationController.isDoing)
        {
            return (BerryTreeController) humanMovementController.getIfInRange(new GOTBerryTree(TreeStateEnum.FULL,
                this));
        }

        return (BerryTreeController) humanMovementController.getIfInRange(new GOTBerryTree(TreeStateEnum.FULL,
            null));
    }

    private State determineState()
    {
        if (humanController.humanResourceController.resourceStorage.get(ResourceEnum.FOOD).amount >= 10)
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
        if (currentlyCuttingBerryTree != null)
            currentlyCuttingBerryTree.setWhoIsCurrentlyCutting(null);
        Destroy(this);
    }
}