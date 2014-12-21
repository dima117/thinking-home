namespace ThinkingHome.Plugins.OneWire
{
	public class OneWireDevice
	{
		public string Address { get; internal set; }

		public OneWireAdapter Adapter { get; internal set; }

		public byte DeviceType
		{
			get { return 0; }
		}

		public virtual string DeviceName
		{
			get { return "Unknown 1-wire device"; }
		}
	}
}