using Godot;
using System;

public class SceneServiceNode : Node2D, ISceneService {
	public IServicePackage Services { get; private set; }

	[Export]
	public NodePath graphicsServicePath;

	public event EventHandler<CollisionEventArgs> Collision;

	public override void _EnterTree() {
		var graphicsService = (GraphicsServiceNode)GetNode(graphicsServicePath);
		Services = new ServicePackage(this, null, graphicsService, null);
	}

	protected virtual void OnCollision(CollisionEventArgs e) {
		Collision?.Invoke(this, e);
	}
}