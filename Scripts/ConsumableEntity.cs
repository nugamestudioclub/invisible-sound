using Collections;
using Godot;

public class ConsumableEntity : SceneServiceNode
{
    [Export]
    private ConsumableType _consumableType;

    public ConsumableType ConsumableType => _consumableType;
    public void _Consume()
    {
        GD.Print("consimung from entity");
        //consume the type
        IBlackboard blackboard = new Blackboard();
        blackboard.SetValue("messageType", "consume");
        blackboard.SetValue("consumableType", ConsumableType);
        OnMessage(new MessageEventArgs(blackboard));
    }


}