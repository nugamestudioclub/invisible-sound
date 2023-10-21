using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

public class Entity
{
    public EntityType Type { get; private set; }
    public int Id { get; private set; }

    protected IGame Game { get; private set; }

    protected IServicePackage Services { get; private set; }

    public Entity(IGame game, IServicePackage serivces, EntityType type, int id)
    {
        Game = game;
        Services = serivces;
        Type = type;
        Id = id;
    }

    public List<Action> Actions { get; }
}