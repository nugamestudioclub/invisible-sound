using Godot;
using System.Collections.Generic;
using System.Linq;

public class ResourceServiceProvider : Node, IResourceServiceProvider
{
    public IResourceService Default { get; } = new ResourceService();

    public override void _Ready()
    {
        //get audio bank
        var audioBank = GetNode<AudioBank>(nameof(AudioBank));
        Default.Add(audioBank.DirtFootsteps.Variations.Select(v =>
        new KeyValuePair<string, Resource>(v.ResourcePath, v)));

        //Default.Add(audioBank.ConcreteFootsteps.Variations.Select(v =>
       // new KeyValuePair<string, Resource>(v.ResourceName, v)));
       // Default.Add(audioBank.GrassFootsteps.Variations.Select(v =>
       // new KeyValuePair<string, Resource>(v.ResourceName, v)));
    }
}