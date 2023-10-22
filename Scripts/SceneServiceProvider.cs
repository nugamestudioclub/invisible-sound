using Collections;
using Godot;
using System.Xml.Linq;

public class SceneServiceProvider : ISceneServiceProvider {
	private Node currentScene;

	public SceneServiceProvider(Node parent) {
		currentScene = parent;
	}

	public ISceneService Connect(EntityType type, int id, IReadOnlyBlackboard data) {
		var scene = data.GetValue<PackedScene>("scene");
		var sceneService = (SceneServiceNode)scene.Instance();
		currentScene.AddChild(sceneService);
		return sceneService;
	}
}