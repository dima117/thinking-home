namespace ThinkingHome.Plugins.Weather.Api
{
	public class WeatherLocatioinModel
	{
		// ReSharper disable InconsistentNaming

		public string displayName { get; set; }
		public WeatherDataModel now { get; set; }
		public WeatherDataModel[] today { get; set; }

		// ReSharper restore InconsistentNaming
	}
}
