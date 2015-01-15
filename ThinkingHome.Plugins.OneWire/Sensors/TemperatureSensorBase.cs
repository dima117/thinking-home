using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkingHome.Plugins.OneWire.Sensors
{
    public abstract class TemperatureSensorBase : OneWireDevice
    {
        public TemperatureSensorBase(byte[] address,  OneWireAdapter adapter)
            :base(address, adapter)
        {
        }

        public abstract double GetTemperature();
    }
}
