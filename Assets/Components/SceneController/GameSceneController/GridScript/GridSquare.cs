// Represent one square of an GridObject

public class GridSquare
{
    public int x;
    public int y;
    public bool isWalkable;

    public GridSquare(int x, int y, bool isWalkable)
    {
        this.x = x;
        this.y = y;
        this.isWalkable = isWalkable;
    }
}