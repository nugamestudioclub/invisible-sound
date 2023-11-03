using Godot;
using System;

public class AudioPlayerNode : AudioStreamPlayer2D, IAudioPlayer {
	public event EventHandler Finished;

	private ISceneService _parent;

	public int Track { get; set; }

	float IAudioPlayer.VolumeDb {
		get => VolumeDb;
		set => VolumeDb = value;
	}

	float IAudioPlayer.MaxDistance {
		get => MaxDistance;
		set => MaxDistance = value;
	}

	float IAudioPlayer.Attenuation {
		get => Attenuation;
		set => Attenuation = value;
	}

	public ISceneService Parent {
		get => _parent;
		set {
			// GD.Print($"{Name} trying setting new parent {value}");
			if( value == _parent )
				return;
			if( _parent is Node oldParent )
				oldParent.RemoveChild(this);
			if( value is Node newParent )
				newParent.AddChild(this);
			_parent = value;
		}
	}


	System.Numerics.Vector3 IAudioPlayer.Position {
		get => new System.Numerics.Vector3(Position.x, Position.y, 0);
		set => Position = new Vector2(value.X, value.Y);
	}

	public void PlayOneShot(string name) {
		var resourceService = _parent.Entity.Services.ResourceService;
		if( (resourceService.Assets.TryGetValue(name, out Resource result)
		|| resourceService.LoadResource(name).TryGetValue(name, out result)) ) {
			if( result is AudioStream stream ) {
				Stream = stream;
				Play();
			}
			else {
				GD.Print($"Resource '{name}' is not an audio stream.");
			}
		}
		else {
			GD.Print($"Failed to load resource '{name}'");
		}
	}

	private void _AudioPlayer_Finished() {
		OnFinished();
	}

	protected virtual void OnFinished() {
		Finished?.Invoke(this, EventArgs.Empty);
	}
}