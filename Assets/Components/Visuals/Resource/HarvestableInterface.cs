public class ResourceAmount
{
    public int amount;
    public ResourceEnum resourceEnum;

    public ResourceAmount(int amount, ResourceEnum resourceEnum)
    {
        this.amount = amount;
        this.resourceEnum = resourceEnum;
    }
}

public interface IHarvestable
{
    ResourceAmount RetrieveResourceAmount();
}