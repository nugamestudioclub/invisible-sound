using Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class AudioService : IAudioService {
	private readonly IAudioServiceProvider _provider;
	private readonly ISceneService _root;
	private readonly Dictionary<string, RoundRobin<string>> _footsteps;

	public AudioService(IAudioServiceProvider provider, IReadOnlyBlackboard data, ISceneService root = null) {
		Exceptions.ArgumentNull.ThrowIfNull(provider, nameof(provider));
		_provider = provider;
		_root = root;
		_footsteps = GetFootstepRoundRobins(data);
		Godot.GD.Print(((Godot.Node)_root).Name);
		foreach( string key in _footsteps.Keys ) {
			Godot.GD.Print($"material: '{key}'");
			Godot.GD.Print(string.Join("\n", _footsteps[key]));
		}
	}

	public void PlayFootstep(string material) {
		if( _footsteps.TryGetValue(material, out var rr) ) {
			string clip = rr.Get();
			// Godot.GD.Print($"Play footstep '{clip}'");
			PlayOneShot(clip, (int)AudioTrack.Footstep, _root);
		}
	}

	public void PlayLooping(string name, int track, ISceneService parent) {
		PlayLooping(name, track, parent, Vector3.Zero);
	}

	public void PlayLooping(string name, int track, ISceneService parent, Vector3 offset) {
		var player = _provider.Connect(track);
		player.Parent = parent;
		player.Position = parent.ScenePosition + offset;
		player.PlayLooping(name);
		Godot.GD.Print($"playing '{name}' (track {track}) @ {parent.ScenePosition}");
	}

	public void PlayOneShot(string name, int track, ISceneService parent) {
		PlayOneShot(name, track, parent, Vector3.Zero);
	}

	public void PlayLooping(string name, int track, Vector3 position) {
		var player = _provider.Connect(track);
		player.Parent = _root;
		player.Position = position;
		player.PlayLooping(name);
	}


	public void PlayOneShot(string name, int track, ISceneService parent, Vector3 offset) {
		var player = _provider.Connect(track);
		player.Parent = parent;
		player.Position = parent.ScenePosition + offset;
		player.PlayOneShot(name);
		Godot.GD.Print($"playing '{name}' (track {track}) @ {parent.ScenePosition}");
	}

	public void PlayOneShot(string name, int track, Vector3 position) {
		var player = _provider.Connect(track);
		player.Parent = _root;
		player.Position = position;
		player.PlayOneShot(name);
	}

	private static string GetFootstepMaterial(string path) {
		int start = AudioServiceProvider.FootstepPath.Length + 1;
		int end = path.IndexOf('/', start);
		return path.Substring(start, end - start);
	}

	private static Dictionary<string, List<string>> GetFootstepResources(IReadOnlyBlackboard data) {
		var resources = new Dictionary<string, List<string>>();
		if( data.TryGetValue<HashSet<string>>("footsteps", out var names) ) {
			foreach( string name in names ) {
				string material = GetFootstepMaterial(name);
				if( resources.TryGetValue(material, out var items) ) {
					items.Add(name);
				}
				else {
					items = new List<string>() { name };
					resources[material] = items;
				}
			}
		}
		return resources;
	}

	private static Dictionary<string, RoundRobin<string>> GetFootstepRoundRobins(IReadOnlyBlackboard data) {
		var resources = GetFootstepResources(data);
		var roundRobins = new Dictionary<string, RoundRobin<string>>();
		foreach( var pair in resources )
			roundRobins[pair.Key] = new RoundRobin<string>(pair.Value);
		return roundRobins;
	}
}