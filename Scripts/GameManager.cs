
using Godot;
using System;
using System.Diagnostics;

public class GameManager : Node {
	Game game;

	ServiceBroker serviceBroker;

	public override void _EnterTree() {
		//attach service broker node to the root
		var resourceServiceProvider = new ResourceServiceProvider();
		var sceneServiceProvider = new SceneServiceProvider(GetTree().Root);
		var graphicsServiceProvider = new GraphicsServiceProvider();
		serviceBroker = new ServiceBroker(sceneServiceProvider, resourceServiceProvider, graphicsServiceProvider, null);
		game = new Game(serviceBroker);
	}

	public override void _Ready() {
		base._Ready();
		GD.Print("Create");
		game.Create(0, "TestNodeEntity");
		GD.Print("Finished");
	}
	//load in all configs

	//hook up all services
}
