using System;
using System.Linq;
using ThinkingHome.Plugins.Weather.Data;

namespace ThinkingHome.Plugins.Weather.Api
{
	internal static class ModelBuilder
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

		#region private

		private static DailyWeatherDataModel CreateDailyModel(IGrouping<DateTime, WeatherData> gr)
		{
			if (gr == null)
			{
				return null;
			}

			var items = gr.ToArray();

			if (!items.Any())
			{
				return null;
			}

			decimal minT = items.Min(d => d.Temperature);
			decimal maxT = items.Max(d => d.Temperature);

			decimal minP = items.Min(d => d.Pressure);
			decimal maxP = items.Max(d => d.Pressure);

			int minH = items.Min(d => d.Humidity);
			int maxH = items.Max(d => d.Humidity);

			return new DailyWeatherDataModel
				{
					DateTime = gr.Key,
					MinTemperature = Convert.ToInt32(minT),
					MaxTemperature = Convert.ToInt32(maxT),

					MinPressure = Convert.ToInt32(minP),
					MaxPressure = Convert.ToInt32(maxP),

					MinHumidity = Convert.ToInt32(minH),
					MaxHumidity = Convert.ToInt32(maxH)
				};
		}

		private static WeatherDataModel CreateModel(WeatherData obj)
		{
			return obj == null 
				? null
				: new WeatherDataModel
						{
							DateTime = obj.Date,
							Code = obj.WeatherCode,
							Description = obj.WeatherDescription,
							Temperature = Convert.ToInt32(obj.Temperature),
							Pressure = Convert.ToInt32(obj.Pressure),
							Humidity = obj.Humidity, 
						};
		}

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
