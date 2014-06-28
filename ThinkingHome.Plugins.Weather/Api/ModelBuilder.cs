using System;
using System.Linq;
using ThinkingHome.Plugins.Weather.Data;

namespace ThinkingHome.Plugins.Weather.Api
{
	public static class ModelBuilder
	{
		public static WeatherLocatioinModel LoadLocationWeatherData(
			DateTime now, Location location, WeatherData[] locationData)
		{
			var minDate = now.AddMinutes(-90);
			var maxDate = now.AddMinutes(90);

			// погода на текущее время
			var current = locationData.FirstOrDefault(d => d.Date > minDate && d.Date <= maxDate);

			// погода на ближайшие сутки
			var xxx = current != null ? current.Date : now;
			var day = locationData
						.Where(d => d.Date > xxx && d.Date <= xxx.AddDays(1))
						.Where(FilterByHours)
						.OrderBy(d => d.Date)
						.Take(3)
						.ToArray();

			// прогноз на несколько дней
			var forecast = locationData
						.Where(d => d.Date.Date > now.Date)
						.GroupBy(d => d.Date.Date)
						.Take(3);

			var model = new WeatherLocatioinModel
			{
				LocationName = location.DisplayName,
				Now = CreateModel(current),
				Today = day.Select(CreateModel).ToArray(),
				Forecast = forecast.Select(CreateDailyModel).ToArray()
			};

			return model;
		}

		private static DailyWeatherDataModel CreateDailyModel(IGrouping<DateTime, WeatherData> obj)
		{
			return null;
		}

		private static WeatherDataModel CreateModel(WeatherData obj)
		{
			return new WeatherDataModel
			{
				DateTime = obj.Date,
				Code = obj.WeatherCode,
				Description = obj.WeatherDescription,
				Temperature = Convert.ToInt32(obj.Temperature),
				Pressure = Convert.ToInt32(obj.Pressure),
				Humidity = obj.Humidity, 
			};
		}

		#region private

		private static bool FilterByHours(WeatherData data)
		{
			return data != null && (
				data.Date.Hour == 3 ||
				data.Date.Hour == 9 ||
				data.Date.Hour == 15 ||
				data.Date.Hour == 21);
		}

		#endregion
	}
}
