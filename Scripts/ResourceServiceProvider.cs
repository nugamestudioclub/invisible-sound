public class ResourceServiceProvider : IResourceServiceProvider {
	public IResourceService Default { get; } = new ResourceService();
}