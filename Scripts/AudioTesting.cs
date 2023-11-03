using Godot;
using System.Collections.Generic;

public class AudioTesting : Node2D {
	private IList<Resource> _resources;

	private AudioStreamPlayer2D _audioPlayer;

	public override void _Ready() {
		base._Ready();
		_resources = GetNode<ResourceList>("Resources");
		_audioPlayer = GetNode<AudioStreamPlayer2D>("AudioPlayer");
	}

	public override void _Input(InputEvent e) {
		base._Input(e);
		if( e is InputEventKey k && k.IsPressed() ) {
			int index = GetInputIndex(k.Scancode);
			if( index >= 0 && index < _resources.Count )
				Play(index);
		}
	}

	private int GetInputIndex(uint scancode) {
		switch( scancode ) {
		case (int)KeyList.Key0: return 0;
		case (int)KeyList.Key1: return 1;
		case (int)KeyList.Key2: return 2;
		case (int)KeyList.Key3: return 3;
		case (int)KeyList.Key4: return 4;
		case (int)KeyList.Key5: return 5;
		case (int)KeyList.Key6: return 6;
		case (int)KeyList.Key7: return 7;
		case (int)KeyList.Key8: return 8;
		case (int)KeyList.Key9: return 9;
		default: return -1;
		}
	}

	private void Play(int index) {
		if( _audioPlayer.Playing )
			_audioPlayer.Stop();
		_audioPlayer.Stream = (AudioStream)_resources[index];
		_audioPlayer.Play();
	}
}
