namespace ThinkingHome.Plugins.OneWire
{
	public class Ds18B20 : OneWireDevice
	{
		public decimal GetTemperature()
		{
			var buf = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };
			var data = Adapter.GetData(Address, 0x44, buf);

			return data[0];
		}

		public override string DeviceName
		{
			get { return "DS18B20 temperature sensor"; }
		}
	}
}