using System;

public interface ISceneService {
	event EventHandler<CollisionEventArgs> Collision;

	Entity Entity { get; set; }
	string Name { get; }
	IServicePackage SceneServices { get; }

	void Alert(System.Numerics.Vector2 position);
}