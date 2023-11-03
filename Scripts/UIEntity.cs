using Godot;

public class UIEntity : SceneServiceNode
{
    private CanvasItem gasContainer;
    private CanvasItem keycardContainer;
    private TextureProgress visualizerProgress;
    public override void _EnterTree()
    {
        base._EnterTree();
        gasContainer = GetNode<CanvasItem>("FullUI/HBoxContainer/GasContainer");
        
        keycardContainer = GetNode<CanvasItem>("FullUI/HBoxContainer/CardContainer");
        visualizerProgress = GetNode<TextureProgress>("FullUI/VisualizerProgress");
        ShowGas = false;
        ShowKeycard = false;
        ShowVisualizer = false;
        CurrentVisualizerProgress = 50;
    }
    public bool ShowGas { 
        get => gasContainer.Visible;
        set => gasContainer.Visible = value;
    }

    public bool ShowKeycard
    {
        get => keycardContainer.Visible;
        set => keycardContainer.Visible = value;
    }

    public bool ShowVisualizer
    {
        get => visualizerProgress.Visible;
        set => visualizerProgress.Visible = value;
    }

    public float CurrentVisualizerProgress
    {
        get => (float) visualizerProgress.Value;
        set => visualizerProgress.Value = value;
    }
}