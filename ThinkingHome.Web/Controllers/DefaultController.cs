using System;
using System.Configuration;
using System.Web.Mvc;

namespace ThinkingHome.Web.Controllers
{
	public class DefaultController : Controller
	{
		public static string ServiceUrlFormat
		{
			get
			{
				return ConfigurationManager.AppSettings["serviceHost"] ?? "http://localhost:8000/api/{0}/{1}";
			}
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Nav()
		{
			var model = new[]
				{
					new { title = "Welcome",id = "6F189A59-CDBB-4CBA-82E5-201C67115CDD", controller="Default", action="Tiles"},
					new { title = "Page 1", id = "857AC354-A14A-4C3E-9F5C-42C09ACEA8D3", controller="Default", action="Tiles"},
					new { title = "Page 2", id = "838E152D-CFD1-4C7D-91AA-7DCD8040CEA6", controller="Default", action="Tiles"},
					new { title = "Page 3", id = "76DFD4EE-0483-4DC1-9864-4F4AD909BC24", controller="Default", action="Tiles"},
					new { title = "Page 4", id = "85E99275-3591-4444-8C9A-53145F8874FF", controller="Default", action="Tiles"}
				};

			return Json(model, JsonRequestBehavior.AllowGet);
		}

		public ActionResult GeneratedScript()
		{
			var model = new
			{
				urlFormat = ServiceUrlFormat
			};

			return View(model);
		}
	}
}
