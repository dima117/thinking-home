using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkingHome.Plugins.OneWire
{
	public class OneWireAdapter : IDisposable
	{
		private readonly object lockObject = new object();

		public void SendCommand(string address, byte cmd, byte[] buf = null)
		{
			lock (lockObject)
			{
				
			}

			throw new NotImplementedException();
		}

		public byte[] GetData(string address, byte cmd, byte[] buf = null)
		{
			lock (lockObject)
			{

			}

			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
