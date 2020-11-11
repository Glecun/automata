using System;
using System.Collections.Generic;
using System.Linq;

public class ResourceStorage
{
    public readonly List<ResourceAmount> resourceAmounts;

    public ResourceStorage(List<ResourceAmount> resourceAmounts)
    {
        this.resourceAmounts = initResourceAmounts(resourceAmounts);
    }

    private static List<ResourceAmount> initResourceAmounts(List<ResourceAmount> amounts)
    {
        return Enum.GetValues(typeof(ResourceEnum)).Cast<ResourceEnum>().Select(resource =>
        {
            var resourceAmountToOverride = amounts.Find(amount => amount.resourceEnum == resource);
            return resourceAmountToOverride ?? new ResourceAmount(0, resource);
        }).ToList();
    }

    public ResourceAmount get(ResourceEnum resourceEnum)
    {
        return resourceAmounts.Find(resource => resource.resourceEnum == resourceEnum);
    }
}