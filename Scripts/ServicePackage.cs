public class ServicePackage : IServicePackage
{
    public ServicePackage(ISceneService sceneService,
        IResourceService resourceService,
        IGraphicsService graphicsService,
        IAudioService audioService)
    {
        SceneService = sceneService;
        ResourceService = resourceService;
        GraphicsService = graphicsService;
        AudioService = audioService;
    }
    public IAudioService AudioService { get; }

    public IGraphicsService GraphicsService { get; }

    public IResourceService ResourceService { get; }

    public ISceneService SceneService { get; }
}