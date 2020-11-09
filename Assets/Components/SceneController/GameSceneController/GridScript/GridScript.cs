using Unity.Mathematics;
using UnityEngine;

public class GridScript
{
    public readonly int cellSize;
    public readonly int x;
    public readonly int y;

    public GridScript(int cellSize, int x, int y)
    {
        this.cellSize = cellSize;
        this.x = x;
        this.y = y;
    }

    public int GetCellSize()
    {
        return cellSize;
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
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
        var pathfinding = new Pathfinding(x, y, cellSize);
        return PathInProgress.setPath(start, end, pathfinding);
    }
}