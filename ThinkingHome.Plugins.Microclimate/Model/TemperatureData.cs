using System;

namespace ThinkingHome.Plugins.Microclimate.Model
{
	
	public class TemperatureData
	{
		public virtual Guid Id { get; set; }

		public virtual DateTime CurrentDate { get; set; }

		public virtual TemperatureSensor Sensor { get; set; }

		public virtual int Temperature { get; set; }

		public virtual int Humidity { get; set; }
	}
}
