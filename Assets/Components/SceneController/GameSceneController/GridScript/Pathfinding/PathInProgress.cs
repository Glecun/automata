using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PathInProgress
{
    public int currentPathIndex;
    public List<Vector3> pathVectorList;

    public static readonly PathInProgress NOT_MOVING = new PathInProgress(0, null);

    private PathInProgress(int currentPathIndex, List<Vector3> pathVectorList)
    {
        this.currentPathIndex = currentPathIndex;
        this.pathVectorList = pathVectorList;
    }

    public static PathInProgress setPath(int2 start, int2 end, Pathfinding pathfinding)
    {
        var pathVectorList = pathfinding.FindPath(start.x, start.y, end.x, end.y);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }

        return new PathInProgress(0, pathVectorList);
    }

    public static PathInProgress setPathToNearest(int2 start, IGridObjectType gridObjectType, Pathfinding pathfinding)
    {
        var pathVectorList = pathfinding.FindPathToNearest(start.x, start.y, gridObjectType);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }

        return new PathInProgress(0, pathVectorList);
    }


    public Vector3 changePosition(Vector3 currentPosition, float speed)
    {
        var newPosition = currentPosition;
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(currentPosition, targetPosition) > 0.01f)
            {
                Vector3 moveDir = (targetPosition - currentPosition).normalized;
                newPosition = currentPosition + moveDir * (speed * Time.deltaTime);
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }

        return newPosition;
    }

    private void StopMoving()
    {
        pathVectorList = null;
    }

    public Boolean isMoving()
    {
        return pathVectorList != null;
    }
}