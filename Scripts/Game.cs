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
        int id = currentId++;
        var serivces = ServiceProviders.Connect(type, id, resourceId);
		GD.Print($"creating new entity {id}");
		return new Entity(this, serivces, type, id);
    }

    public Entity Create(EntityType type, ISceneService sceneService) {
        int id = currentId++;
        var services = ServiceProviders.Connect(type, id, sceneService);
		GD.Print($"creating extant entity {id}");
		return new Entity(this, services, type, id);
	}
}

