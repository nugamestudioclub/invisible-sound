using System.Collections.Generic;
using Godot;

public class AudioBalance : Node {
	[Export]
	private AudioTrackBalance _trackDefault;

	[Export]
	private AudioTrackBalance _trackMusic;

	[Export]
	private AudioTrackBalance _trackAmbient;

	[Export]
	private AudioTrackBalance _trackFootstep;

	[Export]
	private AudioTrackBalance _trackInteractable;

	[Export]
	private AudioTrackBalance _trackDanger;

	[Export]
	private float _minDangerVolumeDb = -100f;

	[Export]
	private float _minDangerDistance = 10f;

	[Export]
	private float _maxDangerDistance = 1000f;

	public AudioTrackBalance TrackDefault => _trackDefault;

	public AudioTrackBalance TrackMusic => _trackMusic;

	public AudioTrackBalance TrackAmbient => _trackAmbient;

	public AudioTrackBalance TrackFootstep => _trackFootstep;

	public AudioTrackBalance TrackInteractable => _trackInteractable;

	public AudioTrackBalance TrackDanger => _trackDanger;

	[Export]
	private List<float> _dangerDistance = new List<float>(3);

	public IList<float> DangerDistance => _dangerDistance;

	public float MinDangerVolumeDb => _minDangerVolumeDb;

	public float MinDangerDistance => _minDangerDistance;

	public float MaxDangerDistance => _maxDangerDistance;
}