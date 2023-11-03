using Collections;
using Godot;
using System;
using System.Collections.Generic;
using System.Numerics;

public class AudioServiceProvider : IAudioServiceProvider {
	public static readonly string FootstepPath = "res://Audio/Footsteps";

	public Game Game { get; set; }
	private readonly HashSet<IAudioPlayer> _players = new HashSet<IAudioPlayer>();
	private readonly Queue<IAudioPlayer> _queue = new Queue<IAudioPlayer>();
	private readonly IAudioService _default;
	private readonly IResourceService _resourceService;
	private readonly AudioBalance _audioBalance;

	private float _dangerDistance;

	public IAudioService Default => _default;

	public AudioServiceProvider(AudioBalance audioBalance, IResourceService resourceService, ISceneService root = null) {
		Exceptions.ArgumentNull.ThrowIfNull(audioBalance, nameof(audioBalance));
		Exceptions.ArgumentNull.ThrowIfNull(resourceService, nameof(resourceService));
		_audioBalance = audioBalance;
		_resourceService = resourceService;
		_default = new AudioService(this, new Blackboard(), root);
	}

	public IAudioService Provide(EntityType type, ISceneService parent) {
		var data = new Blackboard();
		if( type == EntityType.Character ) {
			var footsteps = new HashSet<string>();
			foreach( string name in _resourceService.Assets.Names ) {
				if( name.StartsWith(FootstepPath) ) {
					// GD.Print($"adding '{name}'");
					footsteps.Add(name);
				}
				else {
					// GD.Print($"skipping '{name}'");
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
		Balance(player);
		HandleDangerDistance(player, _dangerDistance);
		_players.Add(player);
		return player;
	}

	public void Disconnect(IAudioPlayer player) {
		_players.Remove(player);
		_queue.Enqueue(player);
		if( player is Node node )
			node.GetParent()?.RemoveChild(node);
	}

	public void Update(IReadOnlyBlackboard settings) {
		var location = settings.GetValueOrDefault("location", Location.None);
		if( location == Location.None )
			return;
		float distance = settings.GetValueOrDefault("danger_distance", -1f);
		var exteriorMusic = Game.GetEntityByName("ExteriorMusic")?.Services.SceneService as MusicPlayer;
		var interiorMusic1 = Game.GetEntityByName("InteriorMusic1")?.Services.SceneService as MusicPlayer;
		var interiorMusic2 = Game.GetEntityByName("InteriorMusic2")?.Services.SceneService as MusicPlayer;
		var interiorMusic3 = Game.GetEntityByName("InteriorMusic3")?.Services.SceneService as MusicPlayer;
		if( exteriorMusic != null )
			HandleExteriorMusic(exteriorMusic, location);
		if( interiorMusic1 != null && interiorMusic2 != null && interiorMusic3 != null )
			HandleInteriorMusic(interiorMusic1, interiorMusic2, interiorMusic3, distance, location);
	}

	private void Balance(IAudioPlayer player) {
		var trackBalance = GetTrackBalance(player);
		player.VolumeDb = trackBalance.VolumeDb;
		player.MaxDistance = trackBalance.MaxDistance;
		player.Attenuation = trackBalance.Attenuation;
	}

	private int DetermineDangerLevel(float distance) {
		var distances = _audioBalance.DangerDistance;
		for( int i = 1; i < distances.Count; ++i )
			if( distance <= distances[i] )
				return i;
		return 0;
	}

	private AudioTrackBalance GetTrackBalance(IAudioPlayer player) {
		switch( player.Track ) {
		case (int)AudioTrack.Music:
			return _audioBalance.TrackMusic;
		case (int)AudioTrack.Ambient:
			return _audioBalance.TrackAmbient;
		case (int)AudioTrack.Footstep:
			return _audioBalance.TrackFootstep;
		case (int)AudioTrack.Interactable:
			return _audioBalance.TrackInteractable;
		case (int)AudioTrack.Danger1:
		case (int)AudioTrack.Danger2:
			return _audioBalance.TrackDanger;
		default:
			return _audioBalance.TrackDefault;
		}
	}

	private void HandleDangerDistance(IAudioPlayer player, float distance) {
		switch( player.Track ) {
		case (int)AudioTrack.Danger2:
			distance = NormalizeDangerDistance(distance);
			player.VolumeDb = _audioBalance.MinDangerVolumeDb
				+ (1f - distance) * (_audioBalance.TrackDanger.VolumeDb - _audioBalance.MinDangerVolumeDb);
			break;
		}
	}

	private float NormalizeDangerDistance(float distance) {
		if( distance > _audioBalance.MaxDangerDistance )
			return 1;
		if( distance < _audioBalance.MinDangerDistance )
			return 0;
		return (distance - _audioBalance.MinDangerDistance)
			/ (_audioBalance.MaxDangerDistance - _audioBalance.MinDangerDistance);
	}

	private void HandleInteriorMusic(MusicPlayer interiorMusic1, MusicPlayer interiorMusic2, MusicPlayer interiorMusic3, float distance, Location location) {
		if( location == Location.PoliceStation || location == Location.Store ) {
			if( !interiorMusic1.Playing )
				interiorMusic1.Play();
			if( distance >= 0 ) {
				int dangerLevel = DetermineDangerLevel(distance);
				if( dangerLevel > 1 && !interiorMusic2.Playing )
					interiorMusic2.Play();
				else
					interiorMusic2.Stop();
				if( dangerLevel > 2 && !interiorMusic3.Playing )
					interiorMusic3.Play();
				else interiorMusic3.Stop();
			}
		}
		else {
			interiorMusic1.Stop();
			interiorMusic2.Stop();
			interiorMusic3.Stop();
		}
	}

	private void HandleExteriorMusic(MusicPlayer exteriorMusic, Location location) {
		if( location == Location.Exterior ) {
			if( !exteriorMusic.Playing )
				exteriorMusic.Play();
		}
		else {
			exteriorMusic.Stop();
		}
	}
	private void AudioPlayer_Finished(object sender, EventArgs e) {
		if( sender is IAudioPlayer player )
			Disconnect(player);
	}

}