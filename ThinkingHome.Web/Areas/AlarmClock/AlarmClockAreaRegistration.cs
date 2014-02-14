using System.Web.Mvc;

namespace ThinkingHome.Widgets.AlarmClock
{
	public class AlarmClockAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "AlarmClock";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"AlarmClock",
				"AlarmClock/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
