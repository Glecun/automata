public class ResourceAmount
{
    public int amount;
    public ResourceEnum resourceEnum;

    public ResourceAmount(int amount, ResourceEnum resourceEnum)
    {
        this.amount = amount;
        this.resourceEnum = resourceEnum;
    }

    public ResourceAmount copy()
    {
        return new ResourceAmount(this.amount, this.resourceEnum);
    }
}

public interface IHarvestable
{
    ResourceAmount RetrieveResourceAmount();
}