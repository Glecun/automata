// Represent one square of an GridObject

public class GridSquare
{
    public int x;
    public int y;
    public bool isWalkable;
    public ReferenceToObject referenceToObject;


    public GridSquare(int x, int y, bool isWalkable, ReferenceToObject referenceToObject)
    {
        this.x = x;
        this.y = y;
        this.isWalkable = isWalkable;
        this.referenceToObject = referenceToObject;
    }
}