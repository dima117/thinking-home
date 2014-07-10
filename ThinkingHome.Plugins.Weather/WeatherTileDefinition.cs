using System;
using System.Linq;
using NHibernate.Linq;
using ThinkingHome.Plugins.Weather.Data;
using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.Weather
{
	[Tile]
	public class WeatherTileDefinition : TileDefinition
	{
		public override string Title
		{
			get { return "Weather"; }
		}

		public override string Url
		{
			get { return "webapp/weather/forecast"; }
		}

		public override bool HasParameters
		{
			get { return true; }
		}

		public override void FillModel(WebUI.Model.TileModel model)
		{
			model.content = "10:00 — 5°C\n16:00 —  6°C\n22:00 —  4°C\n04:00 —  3°C";
		}

		public override TileParameter[] GetParameters(Guid id)
		{
			using (var session = Context.OpenSession())
			{
				var locations = session.Query<Location>().ToList();

				var list = locations
					.Select(l => new TileParameterValue { Label = l.DisplayName, Value = l.Id.ToString() })
					.ToArray();

				return new[]
				   {
					   new TileParameter
					   {
						   Label = "City",
						   Name = "city",
						   List = list
					   }
				   };
			}
		}
	}
}
