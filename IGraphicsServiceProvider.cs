using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IGraphicsServiceProvider : IServiceProvider
{
    IGraphicsService Connect(EntityType type, int id);
}

