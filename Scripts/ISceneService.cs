using System;

public interface ISceneService {
	event EventHandler<CollisionEventArgs> Collision;

	Entity Entity { get; set; }
	string Name { get; }
	System.Numerics.Vector3 ScenePosition { get; }
	System.Numerics.Vector3 Anchor { get; }
	IServicePackage SceneServices { get; }

	void Alert(System.Numerics.Vector2 position);
	WalkmeshMaterial GetMaterialAt(System.Numerics.Vector3 position);
}