using Unity.Mathematics;
using UnityEngine;

public class HumanJustBornAction : MonoBehaviour
{
    private HumanAnimationController humanAnimationController;
    private HumanMovementController humanMovementController;
    private PathInProgress pathInProgress;

    private void Awake()
    {
        humanAnimationController = gameObject.GetComponent<HumanAnimationController>();
        humanMovementController = gameObject.GetComponent<HumanMovementController>();

        var oneSquareBelow = new int2(humanMovementController.getPosition().x,
            humanMovementController.getPosition().y - 1);
        pathInProgress = humanMovementController.goTo(oneSquareBelow);
    }

    void Update()
    {
        transform.position = pathInProgress.changePosition(transform.position, HumanController.speed);
        humanAnimationController.isMoving = pathInProgress.isMoving();
    }

    public bool isFinished()
    {
        return !pathInProgress.isMoving();
    }
}