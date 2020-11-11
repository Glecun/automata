public class GOTTownHall : IGridObjectType
{
    protected bool Equals(GOTTownHall other)
    {
        return true;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GOTTownHall) obj);
    }

    public override int GetHashCode()
    {
        throw new System.NotImplementedException();
    }
}