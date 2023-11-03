using System;
using System.Numerics;

public interface IAudioPlayer {
	event EventHandler Finished;

	float Volume { get; set; }
	int Track { get; set; }
	ISceneService Parent { get; set; }
	Vector3 Position { get; set; }
	void PlayOneShot(string name);
}