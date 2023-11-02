using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

public class Entity {
	public EntityType Type { get; }
	public int Id { get; }
	public string Name { get; }
	protected IGame Game { get; }
	public IServicePackage Services { get; }

	public Entity(IGame game, IServicePackage serivces, EntityType type, int id, string name) {
		Game = game;
		Services = serivces;
		Type = type;
		Id = id;
		Name = name;
	}

	public List<Action> Actions { get; }
}