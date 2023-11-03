using Collections;
using Godot;
using System;
using System.Collections.Generic;
using System.Security.Claims;

public class Game : IGame {
	private int currentId = 0;
	public IServiceBroker ServiceProviders { get; private set; }

	private readonly Dictionary<string, Entity> _entities = new Dictionary<string, Entity>();

	public IBlackboard GlobalData = new Blackboard();
	public Game(IServiceBroker serviceProviders) {
		ServiceProviders = serviceProviders;
		/*
		 * public bool HasVisualizer { get; private set; }
    public bool HasGas { get; private set; }

    public bool HasKey { get; private set; }

    [Export]
	private float _maxBatteryLife;
	public float MaxBatteryLife => _maxBatteryLife;

    [Export]
    private float _batteryStep;
    public float BatteryStep => _batteryStep;

    public float CurrentBatteryLife { get; private set; }
		 */
		GlobalData.SetValue("hasVisualizer", false);
		GlobalData.SetValue("hasGas", false);
		GlobalData.SetValue("hasKey", false);
		float maxBatteryLife = 100f;
		GlobalData.SetValue("maxBatteryLife", maxBatteryLife);
		GlobalData.SetValue("currentBatteryLife", maxBatteryLife/2);
		GlobalData.SetValue("batteryStep", 20f);
		GlobalData.SetValue("location", Location.Exterior);

	}

	public Entity CreateEntity(EntityType type, string resourceId) {
		int id = currentId++;
		var services = ServiceProviders.Connect(type, id, resourceId);
		string name = resourceId;
		GD.Print($"creating new entity {id}: '{name}'");
		var entity = new Entity(this, services, type, id, resourceId);
		_entities[name] = entity;
		services.SceneService.Entity = entity;
		services.SceneService.Message += SceneService_Message;

		return entity;
	}

	public Entity CreateEntity(EntityType type, ISceneService sceneService) {
		int id = currentId++;
		var services = ServiceProviders.Connect(type, id, sceneService);
		string name = sceneService.Name;
		GD.Print($"creating extant entity {id}: '{name}'");
		// GD.Print($"'{name}' audio is null? {services.AudioService == null}");
		var entity = new Entity(this, services, type, id, name);
		_entities[name] = entity;
		sceneService.Entity = entity;
		services.SceneService.Message += SceneService_Message;
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
		var player = GetEntityByName("PlayerEntity");
		var monster = GetEntityByName("MonsterEntity");
		if( player != null && monster != null ) {
			//GD.Print($"player null {player == null} monster null {monster == null}");
			float distance = System.Numerics.Vector3.Distance(
			player.Services.SceneService.ScenePosition,
			monster.Services.SceneService.ScenePosition
		);
			var data = new Blackboard();
			data.SetValue("danger_distance", distance);
			data.SetValue("location", GlobalData.GetValue<Location>("location"));
			ServiceProviders.Audio.Update(data);
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
			// GD.Print($"\tALARM @ ({x,2},{y,2})");
			return true;
		}
		return false;
	}

	private void SceneService_Message(object sender, MessageEventArgs e) {
		var blackboard= e.Blackboard;
		if( blackboard.TryGetValue("messageType", out string messageType) ) {
			if( messageType == "consume" ) {
				HandleMessageConsume(blackboard);
			}
			else if( messageType == "location" ) {
				HandleMessageLocation(blackboard);
			}
		}
	}

	private void HandleMessageConsume(IReadOnlyBlackboard blackboard) {
		if( blackboard.TryGetValue("consumableType", out ConsumableType consumableType) ) {
			var UI = GetEntityByName("UIEntity");
			if( UI.Services.SceneService is UIEntity uiScene ) {
				switch( consumableType ) {
				case ConsumableType.None:
					break;
				case ConsumableType.Key:
					GlobalData.SetValue("hasKey", true);
					uiScene.ShowKeycard = true;
					break;
				case ConsumableType.Gas:
					GlobalData.SetValue("hasGas", true);
					GD.Print("showing gas");
					uiScene.ShowGas = true;
					break;
				case ConsumableType.Visualizer:
					GlobalData.SetValue("hasVisualizer", true);
					uiScene.ShowVisualizer = true;
					break;
				case ConsumableType.Battery:
					float currentBatteryLife = Math.Min(
						GlobalData.GetValue<float>("batteryStep") + GlobalData.GetValue<float>("currentBatteryLife"),
						GlobalData.GetValue<float>("maxBatteryLife"));
					GlobalData.SetValue("currentBatteryLife", currentBatteryLife);
					float percentLife = currentBatteryLife / GlobalData.GetValue<float>("maxBatteryLife");
					uiScene.CurrentVisualizerProgress = percentLife * 100;
					break;
				}
			}
		}
	}

	private void HandleMessageLocation(IReadOnlyBlackboard blackboard) {
		GD.Print("teleporting");
		var currentLocation = GlobalData.GetValue<Location>("location");
		var location = currentLocation == Location.Exterior
			? Location.PoliceStation : Location.PoliceStation;
		var settings = new Blackboard();
		settings.SetValue("location", location);
		ServiceProviders.Audio.Update(settings);
		GlobalData.SetValue("location", location);
	}
}