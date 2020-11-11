using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    public bool isWalkable = false;
    public int width;
    public int height;
    public int x;
    public int y;
    public ReferenceToObject referenceToObject;
    public readonly List<GridSquare> zone;

    public GridObject(bool isWalkable, int x, int y, ReferenceToObject referenceToObject) : this(isWalkable, 1, 1, x, y,
        referenceToObject)
    {
    }

    public GridObject(bool isWalkable, int width, int height, int x, int y, ReferenceToObject referenceToObject)
    {
        this.isWalkable = isWalkable;
        this.width = width;
        this.height = height;
        this.x = x;
        this.y = y;
        this.referenceToObject = referenceToObject;
        zone = createZone();
    }

    private List<GridSquare> createZone()
    {
        List<GridSquare> gridSquares = new List<GridSquare>();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                gridSquares.Add(new GridSquare(x + i, y + j, isWalkable, referenceToObject));
            }
        }

        return gridSquares;
    }
}

public class ReferenceToObject
{
    public IGridObjectType gridObjectType;
    public MonoBehaviour objectController;

    public ReferenceToObject(IGridObjectType gridObjectType, MonoBehaviour objectController)
    {
        this.gridObjectType = gridObjectType;
        this.objectController = objectController;
    }
}