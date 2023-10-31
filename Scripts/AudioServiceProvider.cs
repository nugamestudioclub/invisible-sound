using Godot;
using System.Collections.Generic;

public class AudioServiceProvider : IAudioServiceProvider {
	private readonly Game _game;
	private readonly HashSet<IAudioPlayer> _players;
	private readonly Queue<IAudioPlayer> _queue;

	public AudioServiceProvider(Game game) {
		Exceptions.ArgumentNull.ThrowIfNull(game, nameof(game));
		_game = game;
	}

	public IAudioPlayer Connect(int track) {
		var player = (IAudioPlayer)_game.ServiceProviders.Resources.Default
			.LoadScene("AudioPlayer").GetValue<PackedScene>("scene")
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