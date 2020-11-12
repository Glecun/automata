using Unity.Mathematics;
using UnityEngine;

public class HumanMovementController : MonoBehaviour
{
    private GameSceneController gameSceneController;
    private const int RANGE = 1;
    public const int height = 1;

    private void Start()
    {
        gameSceneController = GameObject.Find("GameSceneController").GetComponent<GameSceneController>();
    }

    public int2 getPosition()
    {
        var position = gameObject.transform.position;
        return new int2((int) position.x, (int) position.y);
    }

    public Vector3 getTopPosition(float offset)
    {
        return Utils.getTopPosition(transform, height, offset);
    }

    public PathInProgress goTo(int2 end)
    {
        var start = getPosition();
        return gameSceneController.grid.calculatePath(start, end);
    }

    public PathInProgress goToNearest(IGridObjectType gridObjectType)
    {
        var start = getPosition();
        return gameSceneController.grid.calculatePathToNearest(start, gridObjectType);
    }

    public MonoBehaviour getIfInRange(IGridObjectType gridObject)
    {
        var pathInProgress = goToNearest(gridObject);
        return isInRange(pathInProgress) ? pathInProgress.destinationobj : null;
    }

    private static bool isInRange(PathInProgress pathInProgress)
    {
        return pathInProgress.isInARangeOf(RANGE);
    }
}