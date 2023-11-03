using System.Numerics;

public interface IAudioService {
	IAudioPlayer PlayLooping(string name, int track, ISceneService parent);
	IAudioPlayer PlayLooping(string name, int track, ISceneService parent, Vector3 offset);
	IAudioPlayer PlayLooping(string name, int track, Vector3 offset);
	IAudioPlayer PlayOneShot(string name, int track, ISceneService parent);
	IAudioPlayer PlayOneShot(string name, int track, ISceneService parent, Vector3 offset);
	IAudioPlayer PlayOneShot(string name, int track, Vector3 position);
	IAudioPlayer PlayFootstep(string material);
}