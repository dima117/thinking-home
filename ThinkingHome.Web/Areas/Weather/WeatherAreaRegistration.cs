using System.Web.Mvc;

namespace ThinkingHome.Widgets.Weather
{
	public class WeatherAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Weather";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"Weather",
				"Weather/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
