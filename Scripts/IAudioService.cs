using System.Numerics;

public interface IAudioService {
	void PlayOneShot(string name, int track, ISceneService parent);
	void PlayOneShot(string name, int track, ISceneService parent, Vector3 offset);
	void PlayOneShot(string name, int track, Vector3 position);
}