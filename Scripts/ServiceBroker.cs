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
		/*
		GD.Print("NULL");
		GD.Print($"sceneService: {sceneService == null}");
		GD.Print($"Resources: {Resources == null}");
		GD.Print($"Services: {sceneService.Services == null}");
		GD.Print($"GraphicsService: {sceneService.Services.GraphicsService == null}");
		GD.Print($"Graphics: {Graphics == null}");
		*/
		return Connect(type, id, sceneService);
	}

	public IServicePackage Connect(EntityType type, int id, ISceneService sceneService) {
		IServicePackage servicePackage = new ServicePackage(
			sceneService,
			Resources.Default,
			sceneService.SceneServices.GraphicsService ?? Graphics.Connect(type, id),
			Audio.Default
        );
		return servicePackage;
	}

	public void Disconnect(int id) {
		throw new System.NotImplementedException();
	}
}