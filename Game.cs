using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Game : IGame
{
    public Game(IServiceBroker serviceProviders)
    {
        ServiceProviders = serviceProviders;
    }
    private int currentId = 0;
    public IServiceBroker ServiceProviders { get; private set; }

    public Entity Create(EntityType type, string resourceId)
    {
        int thisId = currentId++;
        IServicePackage serivces = ServiceProviders.Connect(type, thisId, resourceId);
        return new Entity(this, serivces, type, thisId);
    }
}

