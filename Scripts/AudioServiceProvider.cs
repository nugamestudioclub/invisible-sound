using Collections;
using Godot;
using System;
using System.Collections.Generic;

public class AudioServiceProvider : IAudioServiceProvider {
	private readonly HashSet<IAudioPlayer> _players = new HashSet<IAudioPlayer>();
	private readonly Queue<IAudioPlayer> _queue = new Queue<IAudioPlayer>();
	private readonly IAudioService _default;
	private readonly IResourceService _resourceService;

	public static readonly string FootstepPath = "res://Audio/Footsteps";

	public IAudioService Default => _default;

	public AudioServiceProvider(IResourceService resourceService, ISceneService root = null) {
		Exceptions.ArgumentNull.ThrowIfNull(resourceService, nameof(resourceService));
		_resourceService = resourceService;
		_default = new AudioService(this, new Blackboard(), root);
	}

	public IAudioService Provide(EntityType type, ISceneService parent) {
		var data = new Blackboard();
		if( type == EntityType.Character ) {
			var footsteps = new HashSet<string>();
			foreach( string name in _resourceService.Assets.Names ) {
				if( name.StartsWith(FootstepPath) ) {
					GD.Print($"adding '{name}'");
					footsteps.Add(name);
				}
				else {
					GD.Print($"skipping '{name}'");
				}
			}
			data.SetValue("footsteps", footsteps);
		}
		return new AudioService(this, data, parent);
	}

	public IAudioPlayer Connect(int track) {
		var player = (IAudioPlayer)_resourceService
			.LoadScene("Audio/AudioPlayer").GetValue<PackedScene>("scene")
			.Instance();
		player.Finished += AudioPlayer_Finished;
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

	private void AudioPlayer_Finished(object sender, EventArgs e) {
		if( sender is IAudioPlayer player )
			Disconnect(player);
	}
}