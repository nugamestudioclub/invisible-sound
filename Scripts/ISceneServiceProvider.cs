using Collections;

public interface ISceneServiceProvider : IServiceProvider {
	ISceneService Connect(EntityType type, int id, IReadOnlyBlackboard data);
}