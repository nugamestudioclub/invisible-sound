using Godot;
using System;

public class SFX : Node
{
    [Export]
    private Resource _switch;
    public Resource Switch => _switch;
}
