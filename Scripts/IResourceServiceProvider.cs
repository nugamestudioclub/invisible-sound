using Godot;
using System.Web;

public interface IResourceServiceProvider : IServiceProvider {
	IResourceService Default { get; }
}