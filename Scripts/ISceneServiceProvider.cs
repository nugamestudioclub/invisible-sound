using Collections;
using System;
using System.Collections.Generic;

public interface ISceneServiceProvider : IServiceProvider {
	Queue<Tuple<CollisionData, CollisionData>> Collisions { get; }
	ISceneService Connect(EntityType type, int id, IReadOnlyBlackboard data);
}