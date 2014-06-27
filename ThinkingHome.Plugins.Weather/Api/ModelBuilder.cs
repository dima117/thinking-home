using System;
using System.Collections.Generic;
using System.Linq;
using ThinkingHome.Plugins.Weather.Data;

namespace ThinkingHome.Plugins.Weather.Api
{
	public static class ModelBuilder
	{
		public static WeatherDataModel CreateModel(DateTime date, WeatherData[] weatherData)
		{
			var data = weatherData.FirstOrDefault(d => d.Date == date);
			return CreateModel(data);
		}

		public static WeatherDataModel CreateModel(WeatherData data)
		{
			if (data == null)
			{
				return null;
			}

			var model = new WeatherDataModel
			{
				date = data.Date.ToString("M"),
				time = data.Date.ToShortTimeString(),
				t = Convert.ToInt32(data.Temperature),
				p = Convert.ToInt32(data.Pressure),
				h = data.Humidity,
				code = data.WeatherCode,
				description = data.WeatherDescription
			};

			return model;
		}

		public static WeatherLocatioinModel BuildLocatioinModel(DateTime now, Location location, WeatherData[] data)
		{
			var locationData = data
				.Where(d => d.Location.Id == location.Id)
				.Where(d => d.Date > now.AddMinutes(-90))
				.ToArray();

			var date = locationData
				.Select(d => new DateTime?(d.Date))
				.FirstOrDefault(d => d <= now.AddMinutes(90)) ?? now;

			var today = new List<WeatherDataModel>();
			for (var i = 1; i <= 3; i++)
			{
				var d = CreateModel(date.AddHours(3 + i * 6), locationData);
				if (d != null)
				{
					today.Add(d);
				}
			}


			var model = new WeatherLocatioinModel
			{
				displayName = location.DisplayName,
				now = CreateModel(date, locationData),
				today = today.ToArray()
			};

			return model;
		}
	}
}
