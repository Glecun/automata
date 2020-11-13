using UnityEngine;

public class GOTTree : IGridObjectType
{
    private readonly TreeStateEnum STATE;
    private readonly MonoBehaviour whoIsCurrentlyCutting;

    public GOTTree(TreeStateEnum state, MonoBehaviour whoIsCurrentlyCutting)
    {
        STATE = state;
        this.whoIsCurrentlyCutting = whoIsCurrentlyCutting;
    }

    protected bool Equals(GOTTree other)
    {
        return STATE == other.STATE && Equals(whoIsCurrentlyCutting, other.whoIsCurrentlyCutting);
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
        unchecked
        {
            return ((int) STATE * 397) ^ (whoIsCurrentlyCutting != null ? whoIsCurrentlyCutting.GetHashCode() : 0);
        }
    }
}