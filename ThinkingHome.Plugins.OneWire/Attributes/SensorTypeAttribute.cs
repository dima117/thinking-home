using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkingHome.Plugins.OneWire.Attributes
{
    public class SensorTypeAttribute : Attribute
    {
        private byte sensorType;

        public SensorTypeAttribute(byte sensType)
        {
            sensorType = sensType;
        }

        public override bool Match(object obj)
        {
            if (obj == null)
                return false;

            return (byte)obj == sensorType;
        } 
    }
}
