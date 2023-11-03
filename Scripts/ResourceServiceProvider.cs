using Godot;
using System.Collections.Generic;
using System.Linq;

public class ResourceServiceProvider : Node, IResourceServiceProvider {
	public IResourceService Default { get; } = new ResourceService();

	public override void _Ready() {
		var audioBank = GetNode<AudioBank>(nameof(AudioBank));

		Default.AddRange(audioBank.ConcreteFootsteps
			.Select(v =>new KeyValuePair<string, Resource>(v.ResourcePath, v))
		);

		Default.AddRange(audioBank.DirtFootsteps
			.Select(v => new KeyValuePair<string, Resource>(v.ResourcePath, v))
		);

		Default.AddRange(audioBank.GrassFootsteps
			.Select(v => new KeyValuePair<string, Resource>(v.ResourcePath, v))
		);
	}
}