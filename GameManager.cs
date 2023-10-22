
using Godot;

public class GameManager : Node
{
	Game game;

    ServiceBroker serviceBroker;

	public override void _EnterTree()
	{
		//attach service broker node to the root
		IResourceServiceProvider resourceServiceProvider = new ResourceServiceProvider();
		serviceBroker = new ServiceBroker(this, resourceServiceProvider);
        game = new Game(serviceBroker);
	}

    public override void _Ready()
    {
        base._Ready();
        game.Create(0);
    }
    //load in all configs

    //hook up all services
}
