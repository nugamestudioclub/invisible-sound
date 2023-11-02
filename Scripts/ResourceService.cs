using Collections;
using Godot;
using System.Collections;
using System.Collections.Generic;

public class ResourceService : IResourceService {

	private readonly IBlackboard assets = new Blackboard();

	public IReadOnlyBlackboard Assets => assets;

	public void AddRange<T>(IEnumerable<KeyValuePair<string, T>> data) {
		foreach( var pair in data )
			assets.SetValue(pair.Key, pair.Value);
	}

	public IBlackboard LoadScene(string resourceId) {
		var data = new Blackboard();
		data.SetValue("scene", GD.Load<PackedScene>($"res://Scenes/{resourceId}.tscn"));
		return data;
	}

	public IBlackboard LoadResource(string path) {
		var data = new Blackboard();
		data.SetValue("resource", GD.Load<Resource>(path));
		return data;
	}
}