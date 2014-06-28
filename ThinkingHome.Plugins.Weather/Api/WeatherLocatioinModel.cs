namespace ThinkingHome.Plugins.Weather.Api
{
	public class WeatherLocatioinModel
	{
		public string LocationName { get; set; }
		public WeatherDataModel Now { get; set; }
		public WeatherDataModel[] Today { get; set; }
		public DailyWeatherDataModel[] Forecast { get; set; }
	}
}
