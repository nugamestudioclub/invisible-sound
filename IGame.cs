public interface IGame
{
    IServiceBroker ServiceProviders { get; }
    Entity Create(EntityType type, string resourceId);
}