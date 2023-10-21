using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Game : IGame
{
    private int currentId = 0;
    public IServiceBroker ServiceProviders { get; }

    public Entity Create(EntityType type)
    {
        int thisId = currentId++;
        IServicePackage serivces = ServiceProviders.Connect(type, thisId);
        return new Entity(this, serivces, type, thisId);
    }
}

