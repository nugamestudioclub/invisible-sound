using Godot;

public class AudioPlayerNode : AudioStreamPlayer2D, IAudioPlayer {
	private ISceneService _parent;

	float IAudioPlayer.Volume {
		get => VolumeDb;
		set => VolumeDb = value;
	}

	public int Track { get; set; }

	public ISceneService Parent {
		get => _parent;
		set {
			if( value == _parent )
				return;
			if( _parent is Node oldParent )
				oldParent.RemoveChild(this);
			if( Parent is Node newParent )
				newParent.AddChild(this);
			_parent = value;
		}
	}

	System.Numerics.Vector3 IAudioPlayer.Position {
		get => new System.Numerics.Vector3(Position.x, Position.y, 0);
		set => Position = new Vector2(value.X, value.Y);
	}

	public void PlayOneShot(string name) {
        if (_parent
			.Entity
			.Services
			.ResourceService
			.Assets
			.TryGetValue(name, out Resource result))
		{
			GD.Print($"playing {name}");
			VolumeDb = 10;
            GD.Print($"volume {VolumeDb}");
            Stream = GD.Load<AudioStream>($"res://Audio/Footsteps/Dirt/Dirt 1.wav");//(AudioStream) result;
            GD.Print($"Stream = null {Stream == null}");
            GD.Print($"MaxDistance {MaxDistance}");

            if (!Playing)
			{
                Play();
            }
			
            //result


        };
		
	}
}