using DalSemi.OneWire;
using DalSemi.OneWire.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkingHome.Plugins.OneWire
{
	public class OneWireAdapter : IDisposable
	{        
		readonly object lockObject = new object();
        readonly PortAdapter adapter;

        OneWireAdapterConfiguration Config { get; set; }

        public OneWireAdapter(OneWireAdapterConfiguration config)
        {
            Config = config;
            adapter = AccessProvider.GetAdapter(Config.AdapterName, Config.PortName);
        }

		public void DataBlock(byte[] buf)
		{
			lock (lockObject)
			{
                adapter.DataBlock(buf, 0, buf.Length);
			}
		}

		public void Dispose()
		{			
            adapter.Dispose();
		}

        public bool SelectDevice(byte[] address, int p = 0)
        {
            return adapter.SelectDevice(address, p);
        }

        public void PutByte(int p)
        {
            adapter.PutByte(p);
        }

        public void SetPowerNormal()
        {
            adapter.SetPowerNormal();
        }

        public void SetPowerDuration(int powerTime)
        {
            adapter.SetPowerDuration((OWPowerTime)powerTime);
        }

        public bool StartPowerDelivery(int powerStart)
        {
            return adapter.StartPowerDelivery((OWPowerStart)powerStart);
        }

        public int GetByte()
        {
            return adapter.GetByte();
        }

        /// <summary>
        /// Get family sensors
        /// </summary>
        /// <param name="family">family code, if family = 0 then get all sensors</param>
        /// <returns></returns>
        public byte[][] FindAddress(int family = 0)
        {            
            var _listAddress = new List<byte[]>();

            try
            {
                // get exclusive use of resource
                adapter.BeginExclusive(true);

                // clear any previous search restrictions
                adapter.SetSearchAllDevices();

                if (family == 0) 
                    adapter.TargetAllFamilies();
                else
                    adapter.TargetFamily(family);

                adapter.Speed = OWSpeed.SPEED_REGULAR;
                
                // get 1-Wire Addresses
                // get the first 1-Wire device's address
                // keep in mind the first device is not necessarily the first 
                // device physically located on the network.
                byte[] address = new byte[8];

                if (adapter.GetFirstDevice(address, 0))
                {
                    do
                    {
                        _listAddress.Add(address);
                        address = new byte[8];
                    }
                    while (adapter.GetNextDevice(address, 0));
                }
            }
            finally
            {                
                adapter.EndExclusive();
            }

            return _listAddress.ToArray();
        }
    }
}
