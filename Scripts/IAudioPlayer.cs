using System;
using System.Numerics;

public interface IAudioPlayer {
	event EventHandler Finished;

	int Track { get; set; }
	float VolumeDb { get; set; }
	float MaxDistance { get; set; }
	float Attenuation { get; set; }
	bool Playing { get; set; }
	ISceneService Parent { get; set; }
	Vector3 Position { get; set; }
	void PlayLooping(string name);
	void PlayOneShot(string name);
	void Stop();
	void Play(float fromPosition = 0f);
}