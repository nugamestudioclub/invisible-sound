using Godot;
using System.Xml.Linq;

public class SceneServiceProvider : Node, ISceneServiceProvider
{
    private Node currentScene;
    public SceneServiceProvider()
    {
        currentScene = new Node();
        //should be in a load function
        GetTree().Root.AddChild(currentScene);
    }
    public ISceneService Connect(EntityType type, int id, string resourceId)
    {
        PackedScene scene = GD.Load<PackedScene>($"res://Scenes/{resourceId}");
        currentScene.AddChild(scene.Instance());

        IServicePackage servicePackage = new ServicePackage(null, null, null, null);

        ISceneService sceneService = new SceneServiceNode();
        return sceneService;
    }
}