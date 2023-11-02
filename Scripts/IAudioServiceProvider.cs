public interface IAudioServiceProvider : IServiceProvider {
	IAudioService Default { get; }
	IAudioPlayer Connect(int track);
	void Disconnect(IAudioPlayer player);
	IAudioService Provide(EntityType type, ISceneService parent);
}