using Collections;
using Godot;

public class ConsumableEntity : SceneServiceNode
{
    [Export]
    private ConsumableType _consumableType;

    public ConsumableType ConsumableType => _consumableType;
    public void _Consume(KinematicBody2D player)
    {
        //consume the type
    }
}