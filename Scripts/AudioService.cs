using System.Numerics;

public class AudioService : IAudioService {
	private readonly IAudioServiceProvider _provider;
	private readonly ISceneService _root;

	public AudioService(IAudioServiceProvider provider, ISceneService root = null) {
		Exceptions.ArgumentNull.ThrowIfNull(provider, nameof(provider));
		_provider = provider;
		_root = root;
	}

	public void PlayOneShot(string name, int track, ISceneService parent) {
		PlayOneShot(name, track, parent, Vector3.Zero);
	}

	public void PlayOneShot(string name, int track, ISceneService parent, Vector3 offset) {
		var player = _provider.Connect(track);
		player.Parent = parent;
		player.Position = Vector3.Zero;
		player.PlayOneShot(name);
	}

	public void PlayOneShot(string name, int track, Vector3 position) {
		var player = _provider.Connect(track);
		player.Parent = _root;
		player.Position = position;
		player.PlayOneShot(name);
	}
}