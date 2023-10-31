using Godot;
using System.Collections.Generic;
using System.ComponentModel.Design;

public class AudioServiceProvider : IAudioServiceProvider {
	private readonly HashSet<IAudioPlayer> _players = new HashSet<IAudioPlayer>();
	private readonly Queue<IAudioPlayer> _queue = new Queue<IAudioPlayer>();
	private readonly IAudioService _default;
	private readonly IResourceService _resourceService;

	public IAudioService Default => _default;

	public AudioServiceProvider(IResourceService resourceService, ISceneService root = null) {
		Exceptions.ArgumentNull.ThrowIfNull(resourceService, nameof(resourceService));
		_resourceService = resourceService;
		_default = new AudioService(this, root);
	}

	public IAudioPlayer Connect(int track) {
		var player = (IAudioPlayer)_resourceService
			.LoadScene("Audio/AudioPlayer").GetValue<PackedScene>("scene")
			.Instance();
		player.Track = track;
		// set volume
		_players.Add(player);
		return player;
	}

	public void Disconnect(IAudioPlayer player) {
		_players.Remove(player);
		_queue.Enqueue(player);
		if( player is Node node )
			node.GetParent()?.RemoveChild(node);
	}
}