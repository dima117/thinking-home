using System.Web.Mvc;

namespace ThinkingHome.Widgets.Scripts
{
	public class ScriptsAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Scripts";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"Scripts",
				"Scripts/{action}/{id}",
				new { controller = "DefaultScripts", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
