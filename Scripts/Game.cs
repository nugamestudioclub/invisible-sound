using Godot;
using System;
using System.Collections.Generic;

public class Game : IGame {
	private int currentId = 0;

	public IServiceBroker ServiceProviders { get; private set; }

	private readonly Dictionary<string, Entity> _entities = new Dictionary<string, Entity>();

	public Game(IServiceBroker serviceProviders) {
		ServiceProviders = serviceProviders;
	}

	public Entity CreateEntity(EntityType type, string resourceId) {
		int id = currentId++;
		var services = ServiceProviders.Connect(type, id, resourceId);
		string name = resourceId;
		GD.Print($"creating new entity {id}: '{name}'");
		var entity = new Entity(this, services, type, id, resourceId);
		_entities[name] = entity;
		services.SceneService.Entity = entity;
		return entity;
	}

	public Entity CreateEntity(EntityType type, ISceneService sceneService) {
		int id = currentId++;
		var services = ServiceProviders.Connect(type, id, sceneService);
		string name = sceneService.Name;
		GD.Print($"creating extant entity {id}: '{name}'");
		GD.Print($"'{name}' audio is null? {services.AudioService == null}");
		var entity = new Entity(this, services, type, id, name);
		_entities[name] = entity;
		sceneService.Entity = entity;
		return entity;
	}

	public Entity GetEntityByName(string name) {
		return _entities.TryGetValue(name, out var entity) ? entity : null;
	}

	public void UpdatePhysics(float delta) {
		var sceneCollisions = ServiceProviders.Scene.Collisions;
		while( sceneCollisions.Count > 0 ) {
			var collision = sceneCollisions.Dequeue();
			HandleAlarmCollision(collision.Item1);
			HandleAlarmCollision(collision.Item2);
		}
	}

	private bool HandleAlarmCollision(CollisionData collisionData) {
		// GD.Print($"{nameof(Game)}.{nameof(HandleAlarmCollision)}");
		if( collisionData.Args.TryGetValue<string>("source", out var name)
			&& name.Equals("alarm", StringComparison.OrdinalIgnoreCase) ) {
			var alarm = GetEntityByName("Alarm");
			// GD.Print("\tfound alarm entity");
			var x = collisionData.Args.GetValue<float>("x");
			var y = collisionData.Args.GetValue<float>("y");
			alarm.Services.SceneService.Alert(new System.Numerics.Vector2(x, y));
			GD.Print($"\tALARM @ ({x,2},{y,2})");
			return true;
		}
		return false;
	}
}