public interface IGame {
	IServiceBroker ServiceProviders { get; }
	Entity CreateEntity(EntityType type, string resourceId);
	Entity GetEntityByName(string name);
	void UpdatePhysics(float delta);
}