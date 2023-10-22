public interface IServicePackage
{ 
    IAudioService AudioService { get; }
    IGraphicsService GraphicsService { get; }

    IResourceService ResourceService { get; }

    ISceneService SceneService { get; }
}