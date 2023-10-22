using Godot;

public class SceneServiceNode : Node, ISceneService
{
    [Export]
    private GraphicsServiceNode graphicsService;
    public SceneServiceNode(Node parent, IServicePackage servicePackage)
    {
        Services = servicePackage;
        parent.AddChild(this);
    }
    public IServicePackage Services { get; private set; }

    public override void _EnterTree()
    {
        //find all of the services that are children
        //GraphicsServiceNode graphicsServiceNode = GetChildren<GraphicsServiceNode>(0);
        Services = new ServicePackage(this, null, graphicsService, null);
    }
}