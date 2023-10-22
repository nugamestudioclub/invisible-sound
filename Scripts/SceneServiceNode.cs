using Godot;

public class SceneServiceNode : Node, ISceneService {
	public IServicePackage Services { get; private set; }

	public override void _EnterTree() {
		//find all of the services that are children
		//GraphicsServiceNode graphicsServiceNode = GetChildren<GraphicsServiceNode>(0);
		var graphicsService = GetNode<GraphicsServiceNode>("Sprite");
		Services = new ServicePackage(this, null, graphicsService, null);
	}
}