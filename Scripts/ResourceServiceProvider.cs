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
        var player = audioBank.GetNode<AudioStreamPlayer2D>(nameof(AudioStreamPlayer2D));
        player.Stream = GD.Load<AudioStream>($"res://Audio/Footsteps/Dirt/Dirt 1.wav");


            //(AudioStream) Default.Assets.GetValue<Resource>("res://Audio/Footsteps/Dirt/Dirt 1.wav");
        //player.Play();
        //Default.Add(audioBank.ConcreteFootsteps.Variations.Select(v =>
       // new KeyValuePair<string, Resource>(v.ResourceName, v)));
       // Default.Add(audioBank.GrassFootsteps.Variations.Select(v =>
       // new KeyValuePair<string, Resource>(v.ResourceName, v)));
    }
}