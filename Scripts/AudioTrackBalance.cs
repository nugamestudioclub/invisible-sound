using Godot;

public class AudioTrackBalance : Resource {
	[Export]
	private float _volumeDb = 0f;

	public float VolumeDb => _volumeDb;

	[Export(PropertyHint.Range, "1,10000000,1")]
	private float _maxDistance = 2000f;

	public float MaxDistance => _maxDistance;

	[Export(PropertyHint.Range, "0,1000000")]
	private float _attenuation = 1f;

	public float Attenuation => _attenuation;
}