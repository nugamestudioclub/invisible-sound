using System;

public interface ISceneService {
	event EventHandler<CollisionEventArgs> Collision;

	IServicePackage Services { get; }
}