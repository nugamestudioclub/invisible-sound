using Collections;
using Godot;
using Invisiblesound.Scripts;
using System;
using System.Linq;
using System.Collections.Generic;
using CollisionPair = System.Tuple<CollisionData, CollisionData>;

public class SceneServiceProvider : Node2D, ISceneServiceProvider, ISceneService {
	Game game;

	ServiceBroker serviceBroker;

	private bool started = false;

	private readonly Queue<CollisionPair> _collisions = new Queue<CollisionPair>();
	private readonly Dictionary<object, Dictionary<object, CollisionEventArgs>> _pendingCollisions = new Dictionary<object, Dictionary<object, CollisionEventArgs>>();

	public event System.EventHandler<CollisionEventArgs> Collision;

	public Queue<CollisionPair> Collisions => _collisions;
	public Entity Entity { get; set; }

	private readonly Dictionary<ISceneService, ISet<ISceneService>> _overlapping;

	public IServicePackage SceneServices { get; }
	public System.Numerics.Vector3 ScenePosition => System.Numerics.Vector3.Zero;
	public System.Numerics.Vector3 Anchor => System.Numerics.Vector3.Zero;

	private readonly Dictionary<WalkmeshMaterial, List<Walkmesh>> _walkmeshes;

	public SceneServiceProvider() {
		_walkmeshes = CreateWalkmeshes();
	}

	public override void _EnterTree() {
		//attach service broker node to the root
		var resourceServiceProvider = GetNode<ResourceServiceProvider>("Resources");
		var graphicsServiceProvider = new GraphicsServiceProvider();
		var audioServiceProvider = new AudioServiceProvider(resourceServiceProvider.Default, this);
		serviceBroker = new ServiceBroker(this, resourceServiceProvider, graphicsServiceProvider, audioServiceProvider);
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
			RegisterEntities();
			RegisterWalkmeshes();
			started = true;
			//GD.Print("Create");
			//var entity = game.Create(0, "TestNodeEntity");
		}
	}

	public static string GetName(WalkmeshMaterial material) {
		return Enum.GetNames(typeof(WalkmeshMaterial))[(int)material];
	}

	public WalkmeshMaterial GetMaterialAt(System.Numerics.Vector3 position) {
		// GD.Print($"{nameof(GetMaterialAt)}({position}");
		foreach( var material in _walkmeshes.Keys ) {
			// GD.Print($"material: {material}");
			foreach( var walkmesh in _walkmeshes[material] ) {
				// GD.Print($"\twalkmesh: '{walkmesh.Name}'");
				if( Geometry.IsPointInPolygon(new Vector2(position.X, position.Y), walkmesh.Polygon) )
					return material;
			}
		}
		return WalkmeshMaterial.None;
	}

	public override void _PhysicsProcess(float delta) {
		base._PhysicsProcess(delta);
		game.UpdatePhysics(delta);
		_pendingCollisions.Clear();
	}

	public ISceneService Connect(EntityType type, int id, IReadOnlyBlackboard data) {
		var scene = data.GetValue<PackedScene>("scene");
		var sceneService = (SceneServiceNode)scene.Instance();
		GetTree().Root.AddChild(sceneService);
		return sceneService;
	}

	private void SceneService_Collision(object sender, CollisionEventArgs e) {
		// GD.Print($"{nameof(SceneServiceProvider)}.{nameof(SceneService_Collision)}");
		if( _pendingCollisions.TryGetValue(e.OtherCollider, out var otherCollisions)
			&&  otherCollisions.TryGetValue(e.SenderCollider, out var existingCollision) )
			HandlePendingCollision(e, existingCollision);
		else
			RegisterPendingCollision(e);
	}

	private void HandlePendingCollision(CollisionEventArgs newCollision, CollisionEventArgs existingCollision) {
		// GD.Print($"found matching for {newCollision.Sender.GetHashCode()} from {existingCollision.Sender.GetHashCode()}");
		_collisions.Enqueue(new CollisionPair(
			new CollisionData(existingCollision.Sender, existingCollision.Args),
			new CollisionData(newCollision.Sender, newCollision.Args)
		));
		_pendingCollisions[existingCollision.SenderCollider].Remove(newCollision.SenderCollider);
	}

	private void RegisterPendingCollision(CollisionEventArgs e) {

		// GD.Print($"\tadding collision from {e.Sender.GetHashCode()}");
		if( _pendingCollisions.TryGetValue(e.SenderCollider, out var senderCollisions) ) {
			senderCollisions[e.OtherCollider] = e;
		}
		else {
			senderCollisions = new Dictionary<object, CollisionEventArgs>() {
				{ e.OtherCollider, e }
			};
			_pendingCollisions.Add(e.SenderCollider, senderCollisions);
		}
	}

	public void Alert(System.Numerics.Vector2 position) { }

	private static Dictionary<WalkmeshMaterial, List<Walkmesh>> CreateWalkmeshes() {
		var walkmeshes = new Dictionary<WalkmeshMaterial, List<Walkmesh>>();
		foreach( var x in Enum.GetValues(typeof(WalkmeshMaterial)) ) {
			var material = (WalkmeshMaterial)Convert.ChangeType(x, typeof(WalkmeshMaterial));
			walkmeshes.Add(material, new List<Walkmesh>());
		}
		walkmeshes.Remove(WalkmeshMaterial.None);
		return walkmeshes;
	}

	private void RegisterEntities() {
		var entityNodes = GetTree().GetNodesInGroup("entity");
		GD.Print($"found {entityNodes.Count} existing entity node(s)");
		foreach( var node in entityNodes ) {
			if( node is SceneServiceNode sceneService ) {
				sceneService.Provider = this;
				sceneService.Collision += SceneService_Collision;
				GD.Print("scene requesting creation of entity...");
				game.CreateEntity(sceneService.Type, sceneService);
			}
		}
	}

	private void RegisterWalkmeshes() {
		var walkmeshNodes = GetTree().GetNodesInGroup("walkmesh");
		foreach( var node in walkmeshNodes )
			if( node is Walkmesh walkmesh )
				if( walkmesh.MaterialType != WalkmeshMaterial.None )
					_walkmeshes[walkmesh.MaterialType].Add(walkmesh);
	}
}