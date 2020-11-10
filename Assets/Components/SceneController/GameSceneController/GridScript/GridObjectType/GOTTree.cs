public class GOTTree : IGridObjectType
{
    private readonly TreeStateEnum STATE;

    public GOTTree(TreeStateEnum state)
    {
        STATE = state;
    }

    protected bool Equals(GOTTree other)
    {
        return STATE == other.STATE;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GOTTree) obj);
    }

    public override int GetHashCode()
    {
        return (int) STATE;
    }
}