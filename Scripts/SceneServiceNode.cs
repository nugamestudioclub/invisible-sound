using Godot;

public class SceneServiceNode : Node, ISceneService {
	public IServicePackage Services { get; private set; }

	[Export]
	public NodePath graphicsServicePath;

	public override void _EnterTree()
    {
        var graphicsService = (GraphicsServiceNode)GetNode(graphicsServicePath);
        Services = new ServicePackage(this, null, graphicsService, null);
    }
}