namespace ThinkingHome.Plugins.Weather.Api
{
	public class WeatherDataModel
	{
		// ReSharper disable InconsistentNaming

		public string date { get; set; }
		public string time { get; set; }
		public int t { get; set; }
		public int p { get; set; }
		public int h { get; set; }
		public string code { get; set; }
		public string description { get; set; }

		// ReSharper restore InconsistentNaming
	}
}
