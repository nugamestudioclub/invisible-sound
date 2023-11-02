using Godot;
using System;
using System.Collections.Generic;

public class Footsteps : Node
{
    [Export]
    private List<Resource> _variations;

    public List<Resource> Variations => _variations;

}
