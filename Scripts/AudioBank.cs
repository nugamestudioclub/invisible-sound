using Godot;
using System;

public class AudioBank : Node
{
    public ResourceList DirtFootsteps { get; private set; }
    public ResourceList ConcreteFootsteps { get; private set; }
    public ResourceList GrassFootsteps { get; private set; }

    public override void _EnterTree()
    {
        DirtFootsteps = GetNode<ResourceList>("Footsteps/Dirt");
        //ConcreteFootsteps = GetNode<Footsteps>("Footsteps/Concrete");
        //GrassFootsteps = GetNode<Footsteps>("Footsteps/Grass");
    }
}
