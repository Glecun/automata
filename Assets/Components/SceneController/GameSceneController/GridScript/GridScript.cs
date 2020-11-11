using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class GridScript
{
    public readonly int cellSize;
    public readonly int width;
    public readonly int height;
    private readonly List<GridSquare>[,] grid;

    public GridScript(int cellSize, int width, int height)
    {
        this.cellSize = cellSize;
        this.width = width;
        this.height = height;
        grid = new List<GridSquare>[width, height];

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                grid[x, y] = new List<GridSquare>();
            }
        }
    }

    public int GetCellSize()
    {
        return cellSize;
    }

    public int GetX()
    {
        return width;
    }

    public int GetY()
    {
        return height;
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        y = Mathf.FloorToInt(worldPosition.y / cellSize);
    }

    public PathInProgress calculatePath(int2 start, int2 end)
    {
        var pathfinding = new Pathfinding(width, height, cellSize, grid);
        return PathInProgress.setPath(start, end, pathfinding);
    }

    public PathInProgress calculatePathToNearest(int2 start, IGridObjectType gridObjectType)
    {
        var pathfinding = new Pathfinding(width, height, cellSize, grid);
        return PathInProgress.setPathToNearest(start, gridObjectType, pathfinding);
    }

    public void registerOnGrid(GridObject gridObject)
    {
        deregisterOnGrid(gridObject.referenceToObject.objectController);
        gridObject.zone.ForEach(gridSquare => grid[gridSquare.x, gridSquare.y].Add(gridSquare));
    }

    private void deregisterOnGrid(MonoBehaviour objectController)
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                grid[x, y] = grid[x, y].Where(square => square.referenceToObject.objectController != objectController)
                    .ToList();
            }
        }
    }
}