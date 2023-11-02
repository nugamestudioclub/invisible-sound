using Godot;
using System;

public class AudioBank : Node
{
    public Footsteps DirtFootsteps { get; private set; }
    public Footsteps ConcreteFootsteps { get; private set; }
    public Footsteps GrassFootsteps { get; private set; }

    public override void _EnterTree()
    {
        DirtFootsteps = GetNode<Footsteps>("Footsteps/Dirt");
        //ConcreteFootsteps = GetNode<Footsteps>("Footsteps/Concrete");
        //GrassFootsteps = GetNode<Footsteps>("Footsteps/Grass");
    }
}
