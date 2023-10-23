using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IServiceBroker {
	IAudioServiceProvider Audio { get; }
	IGraphicsServiceProvider Graphics { get; }
	IResourceServiceProvider Resources { get; }

	IServicePackage Connect(EntityType type, int id, ISceneService sceneService);
	IServicePackage Connect(EntityType type, int id, string resourceId);
	void Disconnect(int id);

}

