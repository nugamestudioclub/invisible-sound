using Godot;
using System;

public class SceneServiceNode : Node2D, ISceneService {
	public Entity Entity { get; set; }

	public ISceneServiceProvider Provider { get; set; }

	public EntityType Type { get; protected set; }

	public IServicePackage SceneServices { get; private set; }
	public virtual System.Numerics.Vector3 ScenePosition => new System.Numerics.Vector3(GlobalPosition.x, GlobalPosition.y, 0);
	public virtual System.Numerics.Vector3 Anchor => new System.Numerics.Vector3(Position.x, Position.y, 0);

	[Export]
	public NodePath graphicsServicePath;

	public event EventHandler<CollisionEventArgs> Collision;

	public override void _EnterTree() {
		var graphicsService = (GraphicsServiceNode)GetNode(graphicsServicePath);
		SceneServices = new ServicePackage(this, null, graphicsService, null);
	}

	protected virtual void OnCollision(CollisionEventArgs e) {
		// GD.Print($"{nameof(SceneServiceNode)}.{nameof(OnCollision)}");
		// GD.Print($"\t'{Name}' detected collision");
		Collision?.Invoke(this, e);
	}

	public virtual void Alert(System.Numerics.Vector2 position) { }

	public WalkmeshMaterial GetMaterialAt(System.Numerics.Vector3 position) {
		return Provider is ISceneService scene
			? scene.GetMaterialAt(position)
			: WalkmeshMaterial.None;
	}

	public virtual void Start() { }
}