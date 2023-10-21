using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IServiceBroker
{
    IAudioServiceProvider Audio { get; }
    IGraphicsServiceProvider Graphics { get; }

    IServicePackage Connect(EntityType type, int id);

    void Disconnect(int id);
    
}

