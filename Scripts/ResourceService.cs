using Collections;
using Godot;
using System.Collections;
using System.Collections.Generic;

public class ResourceService : IResourceService {

	private readonly IBlackboard assets = new Blackboard();
    public IBlackboard LoadScene(string resourceId) {
		var data = new Blackboard();
		data.SetValue("scene", GD.Load<PackedScene>($"res://Scenes/{resourceId}.tscn"));
		return data;
	}

	public void Add<T>(IEnumerable<KeyValuePair<string , T>> data) 
	{
		foreach (var pair in data)
		{
			assets.SetValue(pair.Key, pair.Value);
			GD.Print($"Added {pair.Key}, value type {pair.Value.GetType()}");
		}
	}

	public IReadOnlyBlackboard Assets => assets;
}