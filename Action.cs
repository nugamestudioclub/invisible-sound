using System;
using System.Xml;

public class Action
{
    protected IGame Game { get; private set; }

    protected IServicePackage Services { get; private set; }
    public ActionType Type { get; }

    //do we need this?
    public bool IsCompleted { get; }

    public Action(IGame game, IServicePackage services, ActionType type)
    {
        Game = game;
        Services = services;
        Type = type;
    }

    public void Execute(Entity actor, TimeSpan dt)
    {

    }

    public void Complete()
    {

    }

    
}