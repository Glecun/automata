using System.Collections.Generic;

public class GridObject
{
    public bool isWalkable = false;
    public int width;
    public int height;
    public int x;
    public int y;
    public readonly List<GridSquare> zone;

    public GridObject(bool isWalkable, int x, int y) : this(isWalkable, 1, 1, x, y)
    {
    }

    public GridObject(bool isWalkable, int width, int height, int x, int y)
    {
        this.isWalkable = isWalkable;
        this.width = width;
        this.height = height;
        this.x = x;
        this.y = y;
        zone = createZone();
    }

    private List<GridSquare> createZone()
    {
        List<GridSquare> gridSquares = new List<GridSquare>();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                gridSquares.Add(new GridSquare(x + i, y + j, isWalkable));
            }
        }

        return gridSquares;
    }
}