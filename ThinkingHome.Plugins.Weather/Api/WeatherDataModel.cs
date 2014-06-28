using System;

namespace ThinkingHome.Plugins.Weather.Api
{
	public class WeatherDataModel
	{
		public DateTime DateTime { get; set; }
		public int Temperature { get; set; }
		public int Pressure { get; set; }
		public int Humidity { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
	}
}
