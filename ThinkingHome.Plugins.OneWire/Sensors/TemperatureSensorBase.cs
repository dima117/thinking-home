namespace ThinkingHome.Plugins.OneWire.Sensors
{
    public abstract class TemperatureSensorBase : OneWireDevice
    {
	    protected TemperatureSensorBase(byte[] address,  OneWireAdapter adapter)
            :base(address, adapter)
        {
        }

        public abstract double GetTemperature();
    }
}
