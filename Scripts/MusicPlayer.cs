using Godot;

public class MusicPlayer : SceneServiceNode {
	[Export]
	private Resource _resource;

	private IAudioPlayer _audioPlayer;

	public bool Playing => _audioPlayer?.Playing ?? false;

	public override void Start() {
		_audioPlayer = Entity.Services.AudioService.PlayLooping(_resource.ResourcePath, (int)AudioTrack.Music, this);
	}

	public void Play() {
		_audioPlayer?.Play();
	}

	public void Stop() {
		_audioPlayer?.Stop();
	}
}