public interface ISceneServiceProvider : IServiceProvider
{
    ISceneService Connect(EntityType type, int id, string resourceId);
}