using Collections;

public class CollisionData {
	private readonly ISceneService _scene;
	private readonly IReadOnlyBlackboard _args;

	public ISceneService Scene => _scene;
	public IReadOnlyBlackboard Args => _args;

	public CollisionData(ISceneService scene, IReadOnlyBlackboard args) {
		Exceptions.ArgumentNull.ThrowIfNull(scene, nameof(scene));
		Exceptions.ArgumentNull.ThrowIfNull(args, nameof(args));
		_scene = scene;
		_args = args;
	}
}