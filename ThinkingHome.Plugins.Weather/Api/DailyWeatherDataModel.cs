using System;

namespace ThinkingHome.Plugins.Weather.Api
{
	public class DailyWeatherDataModel
	{
		public DateTime DateTime { get; set; }

		public int MinTemperature { get; set; }
		public int MinPressure { get; set; }
		public int MinHumidity { get; set; }

		public int MaxTemperature { get; set; }
		public int MaxPressure { get; set; }
		public int MaxHumidity { get; set; }

		public string Code { get; set; }
		public string Description { get; set; }
	}
}
