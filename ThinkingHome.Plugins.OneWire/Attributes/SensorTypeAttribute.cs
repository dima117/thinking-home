using System;

namespace ThinkingHome.Plugins.OneWire.Attributes
{
    public class SensorTypeAttribute : Attribute
    {
        private readonly byte sensorType;

        public SensorTypeAttribute(byte sensType)
        {
            sensorType = sensType;
        }

        public override bool Match(object obj)
        {
	        if (obj == null)
	        {
		        return false;
	        }

            return (byte)obj == sensorType;
        } 
    }
}
