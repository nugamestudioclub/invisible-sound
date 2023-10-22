using Collections;
using Godot;

public class ResourceService : IResourceService {
	public IBlackboard LoadScene(string resourceId) {
		var data = new Blackboard();
		data.SetValue("scene", GD.Load<PackedScene>($"res://Scenes/{resourceId}.tscn"));
		return data;
	}
}