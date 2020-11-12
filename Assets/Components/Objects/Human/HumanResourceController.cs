using System.Collections.Generic;

public class HumanResourceController
{
    public ResourceStorage resourceStorage;

    public HumanResourceController()
    {
        resourceStorage = new ResourceStorage(new List<ResourceAmount>());
    }
}