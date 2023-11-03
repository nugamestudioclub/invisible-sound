using Godot;

public class MusicPlayer : SceneServiceNode {
	[Export]
	private Resource _resource;

	public override void Start() {
		Entity.Services.AudioService.PlayLooping(_resource.ResourcePath, (int)AudioTrack.Music, this);
	}
}