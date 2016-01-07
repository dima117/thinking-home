namespace ThinkingHome.Plugins.Weather.Api
{
	public static class WeatherUtils
	{
		public static string FormatTemperature(int t)
		{
			string format = t > 0 ? "+{0}" : "{0}";
			return string.Format(format, t);
		}

		public static string FormatTemperatureRange(int t1, int t2)
		{
			return t1 == t2
				? FormatTemperature(t1)
				: string.Format("{0} .. {1}", FormatTemperature(t1), FormatTemperature(t2));
		}

		public static string GetIconClass(string code)
		{
			switch (code)
			{
				// DAY
				case "01d":	// sky is clear 
					return "wi-day-sunny";
				case "02d": // few clouds
					return "wi-day-cloudy";
				case "03d": // scattered clouds
					return "wi-cloudy";
				case "04d": // broken clouds
					return "wi-cloudy";
				case "09d": // shower rain
					return "wi-showers";
				case "10d": // rain
					return "wi-day-rain-mix";
				case "11d": // thunderstorm
					return "wi-storm-showers";
				case "13d": // snow
					return "wi-snow";
				case "50d": // mist
					return "wi-fog";

				// NIGHT
				case "01n":	// sky is clear 
					return "wi-stars";
				case "02n": // few clouds
					return "wi-night-cloudy";
				case "03n": // scattered clouds
					return "wi-cloudy";
				case "04n": // broken clouds
					return "wi-cloudy";
				case "09n": // shower rain
					return "wi-showers";
				case "10n": // rain
					return "wi-night-rain-mix";
				case "11n": // thunderstorm
					return "wi-storm-showers";
				case "13n": // snow
					return "wi-snow";
				case "50n": // mist
					return "wi-fog";
				default:
					return "wi-cloud-refresh";
			}
		}
	}
}
