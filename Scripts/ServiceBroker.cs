using Godot;

public class ServiceBroker : IServiceBroker {
	public ServiceBroker(
		ISceneServiceProvider sceneServiceProvider,
		IResourceServiceProvider resourceServiceProvider,
		IGraphicsServiceProvider graphicsServiceProvider,
		IAudioServiceProvider audioServiceProvider
		) {
		Scene = sceneServiceProvider;
		Resources = resourceServiceProvider;
		Graphics = graphicsServiceProvider;
		Audio = audioServiceProvider;
	}

	public IAudioServiceProvider Audio { get; }

	public IGraphicsServiceProvider Graphics { get; }

	public IResourceServiceProvider Resources { get; }

	public ISceneServiceProvider Scene { get; }

	public IServicePackage Connect(EntityType type, int id, string resourceId) {
		var data = Resources.Default.LoadScene(resourceId);
		ISceneService sceneService = Scene.Connect(type, id, data);
		IServicePackage servicePackage = new ServicePackage(
			sceneService,
			Resources.Default,
            sceneService.Services.GraphicsService ?? Graphics.Connect(type, id),
			null ///
        );
		return servicePackage;
	}

	public void Disconnect(int id) {
		throw new System.NotImplementedException();
	}
}