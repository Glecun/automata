﻿using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PathInProgress
{
    public int currentPathIndex;
    public List<Vector3> pathVectorList;
    public MonoBehaviour destinationobj;

    public static readonly PathInProgress NOT_MOVING = new PathInProgress(0, null, null);

    private PathInProgress(int currentPathIndex, List<Vector3> pathVectorList, MonoBehaviour destinationobj)
    {
        this.currentPathIndex = currentPathIndex;
        this.pathVectorList = pathVectorList;
        this.destinationobj = destinationobj;
    }

    public static PathInProgress setPath(int2 start, int2 end, Pathfinding pathfinding)
    {
        var pathVectorList = pathfinding.FindPath(start.x, start.y, end.x, end.y);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }

        return new PathInProgress(0, pathVectorList, null);
    }

    public static PathInProgress setPathToNearest(int2 start, IGridObjectType gridObjectType, Pathfinding pathfinding)
    {
        List<Vector3> pathVectorList;
        MonoBehaviour objectController;
        pathfinding.FindPathToNearest(start.x, start.y, gridObjectType, out pathVectorList, out objectController);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }

        return new PathInProgress(0, pathVectorList, objectController);
    }


    public Vector3 changePosition(Vector3 currentPosition, float speed)
    {
        var newPosition = currentPosition;
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            var movementWidth = (speed * Time.deltaTime);
            if (Vector3.Distance(currentPosition, targetPosition) > movementWidth)
            {
                Vector3 moveDir = (targetPosition - currentPosition).normalized;
                newPosition = currentPosition + moveDir * movementWidth;
            }
            else
            {
                newPosition = targetPosition;
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

    public bool isInARangeOf(int range)
    {
        return pathVectorList != null && pathVectorList.Count <= range;
    }
}