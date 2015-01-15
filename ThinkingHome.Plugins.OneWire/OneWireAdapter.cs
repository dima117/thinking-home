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
        PortAdapter _adapter;

        OneWireAdapterConfiguration Config { get; set; }

        public OneWireAdapter(OneWireAdapterConfiguration config)
        {
            Config = config;
            _adapter = AccessProvider.GetAdapter(Config.AdapterName, Config.PortName);
        }

		public void DataBlock(byte[] buf)
		{
			lock (lockObject)
			{
                _adapter.DataBlock(buf, 0, buf.Length);
			}
		}

		public void Dispose()
		{			
            _adapter.Dispose();
		}

        public bool SelectDevice(byte[] address, int p = 0)
        {
            return _adapter.SelectDevice(address, p);
        }

        public void PutByte(int p)
        {
            _adapter.PutByte(p);
        }

        public void SetPowerNormal()
        {
            _adapter.SetPowerNormal();
        }

        public void SetPowerDuration(int powerTime)
        {
            _adapter.SetPowerDuration((OWPowerTime)powerTime);
        }

        public bool StartPowerDelivery(int powerStart)
        {
            return _adapter.StartPowerDelivery((OWPowerStart)powerStart);
        }

        public int GetByte()
        {
            return _adapter.GetByte();
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
                _adapter.BeginExclusive(true);

                // clear any previous search restrictions
                _adapter.SetSearchAllDevices();

                if (family == 0) 
                    _adapter.TargetAllFamilies();
                else
                    _adapter.TargetFamily(family);

                _adapter.Speed = OWSpeed.SPEED_REGULAR;
                
                // get 1-Wire Addresses
                // get the first 1-Wire device's address
                // keep in mind the first device is not necessarily the first 
                // device physically located on the network.
                byte[] address = new byte[8];

                if (_adapter.GetFirstDevice(address, 0))
                {
                    do
                    {
                        _listAddress.Add(address);
                        address = new byte[8];
                    }
                    while (_adapter.GetNextDevice(address, 0));
                }
            }
            finally
            {                
                _adapter.EndExclusive();
            }

            return _listAddress.ToArray();
        }
    }
}
