using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IAudioServiceProvider : IServiceProvider {
	IAudioPlayer Connect(int track);
	void Disconnect(IAudioPlayer player);
}