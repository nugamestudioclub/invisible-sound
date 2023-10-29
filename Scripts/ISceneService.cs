using System;

public interface ISceneService {
	event EventHandler<CollisionEventArgs> Collision;

	string Name { get; }
	IServicePackage Services { get; }

	void Alert(System.Numerics.Vector2 position);
}