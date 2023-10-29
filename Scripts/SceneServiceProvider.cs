using Collections;
using Godot;
using System;
using System.Collections.Generic;

using CollisionPair = System.Tuple<CollisionData, CollisionData>;

public class SceneServiceProvider : Node, ISceneServiceProvider {
	Game game;

	ServiceBroker serviceBroker;

	private bool started = false;

	private readonly Queue<CollisionPair> _collisions = new Queue<CollisionPair>();
	private readonly Dictionary<object, Dictionary<object, CollisionEventArgs>> _pendingCollisions = new Dictionary<object, Dictionary<object, CollisionEventArgs>>();

	public Queue<CollisionPair> Collisions => _collisions;

	public override void _EnterTree() {
		//attach service broker node to the root
		var resourceServiceProvider = new ResourceServiceProvider();
		var graphicsServiceProvider = new GraphicsServiceProvider();
		serviceBroker = new ServiceBroker(this, resourceServiceProvider, graphicsServiceProvider, null);
		game = new Game(serviceBroker);
	}

	public override void _Ready() {
		base._Ready();
		//GD.Print("Create");
		//game.Create(0, "TestNodeEntity");
		//GD.Print("Finished");
	}

	public override void _Process(float delta) {
		base._Process(delta);
		if( !started ) {
			var nodes = GetTree().GetNodesInGroup("entity");
			GD.Print($"found {nodes.Count} existing entity node(s)");
			foreach( var node in nodes ) {
				if( node is SceneServiceNode sceneService ) {
					sceneService.Collision += SceneService_Collision;
					GD.Print("scene requesting creation of entity...");
					game.Create(0, sceneService);
				}
			}

			started = true;
			//GD.Print("Create");
			//var entity = game.Create(0, "TestNodeEntity");
		}
	}

	public ISceneService Connect(EntityType type, int id, IReadOnlyBlackboard data) {
		var scene = data.GetValue<PackedScene>("scene");
		var sceneService = (SceneServiceNode)scene.Instance();
		GetTree().Root.AddChild(sceneService);
		return sceneService;
	}

	private void SceneService_Collision(object sender, CollisionEventArgs e) {
		if( _pendingCollisions.TryGetValue(e.OtherCollider, out var otherCollisions)
			&&  otherCollisions.TryGetValue(e.SenderCollider, out var collision) ) {
			_collisions.Enqueue(new CollisionPair(
				new CollisionData(collision.Sender, collision.Args),
				new CollisionData(e.Sender, e.Args)
			));
			
			otherCollisions.Remove(e.SenderCollider);


        }
        if (e.Args.TryGetValue("(x,y)", out string location))
        {
            GD.Print($"stepped on tile with location {location}");
        }
    }
}