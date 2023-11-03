using System.Numerics;

public interface IAudioService {
	void PlayLooping(string name, int track, ISceneService parent);
	void PlayLooping(string name, int track, ISceneService parent, Vector3 offset);
	void PlayLooping(string name, int track, Vector3 offset);
	void PlayOneShot(string name, int track, ISceneService parent);
	void PlayOneShot(string name, int track, ISceneService parent, Vector3 offset);
	void PlayOneShot(string name, int track, Vector3 position);
	void PlayFootstep(string material);
}