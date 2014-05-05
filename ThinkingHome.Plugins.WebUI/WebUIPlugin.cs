using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Handlers;
using ThinkingHome.Plugins.WebUI.Attributes;
using ThinkingHome.Plugins.WebUI.Data;

namespace ThinkingHome.Plugins.WebUI
{
	#region respurces

	// html
	[HttpResource("/", "ThinkingHome.Plugins.WebUI.Resources.index.html", "text/html")]

	// js
	[JavaScriptResource("/js/vendor/require.min.js", "ThinkingHome.Plugins.WebUI.Resources.js.vendor.require.min.js")]
	[JavaScriptResource("/js/vendor/json2.min.js", "ThinkingHome.Plugins.WebUI.Resources.js.vendor.json2.min.js")]
	[JavaScriptResource("/js/vendor/jquery.min.js", "ThinkingHome.Plugins.WebUI.Resources.js.vendor.jquery.min.js")]
	[JavaScriptResource("/js/vendor/underscore.min.js", "ThinkingHome.Plugins.WebUI.Resources.js.vendor.underscore.min.js")]
	[JavaScriptResource("/js/vendor/backbone.min.js", "ThinkingHome.Plugins.WebUI.Resources.js.vendor.backbone.min.js")]
	[JavaScriptResource("/js/vendor/backbone.marionette.min.js", "ThinkingHome.Plugins.WebUI.Resources.js.vendor.backbone.marionette.min.js")]
	[JavaScriptResource("/js/vendor/bootstrap.min.js", "ThinkingHome.Plugins.WebUI.Resources.js.vendor.bootstrap.min.js")]
	[JavaScriptResource("/js/vendor/tpl.min.js", "ThinkingHome.Plugins.WebUI.Resources.js.vendor.tpl.min.js")]

	// css
	[HttpResource("/css/bootstrap.min.css", "ThinkingHome.Plugins.WebUI.Resources.css.bootstrap.min.css", "text/css")]
	[HttpResource("/css/site.css", "ThinkingHome.Plugins.WebUI.Resources.css.site.css", "text/css")]

	// fonts
	[HttpResource("/fonts/glyphicons-halflings-regular/.eot", "ThinkingHome.Plugins.WebUI.Resources.css.glyphicons-halflings-regular.eot", "application/vnd.ms-fontobject")]
	[HttpResource("/fonts/glyphicons-halflings-regular/.svg", "ThinkingHome.Plugins.WebUI.Resources.css.glyphicons-halflings-regular.svg", "image/svg+xml")]
	[HttpResource("/fonts/glyphicons-halflings-regular/.ttf", "ThinkingHome.Plugins.WebUI.Resources.css.glyphicons-halflings-regular.ttf", "application/x-font-truetype")]
	[HttpResource("/fonts/glyphicons-halflings-regular/.woff", "ThinkingHome.Plugins.WebUI.Resources.css.glyphicons-halflings-regular.woff", "application/font-woff")]

	// webapp: main
	[JavaScriptResource("/js/th-main.js", "ThinkingHome.Plugins.WebUI.Resources.js.th-main.js")]
	[JavaScriptResource("/js/app.js", "ThinkingHome.Plugins.WebUI.Resources.js.app.js")]

	// webapp: menu
	[JavaScriptResource("/js/application/menu-controller.js", "ThinkingHome.Plugins.WebUI.Resources.js.application.menu-controller.js")]
	[JavaScriptResource("/js/application/menu-model.js", "ThinkingHome.Plugins.WebUI.Resources.js.application.menu-model.js")]
	[JavaScriptResource("/js/application/menu-view.js", "ThinkingHome.Plugins.WebUI.Resources.js.application.menu-view.js")]
	[HttpResource("/js/application/menu-item.tpl", "ThinkingHome.Plugins.WebUI.Resources.js.application.menu-item.tpl")]

	// webapp: menu settings
	[SystemSection("Main menu settings", "/webapp/webui/settings-controller.js", "ThinkingHome.Plugins.WebUI.PluginResources.settings-controller.js")]
	[JavaScriptResource("/webapp/webui/settings-model.js", "ThinkingHome.Plugins.WebUI.PluginResources.settings-model.js")]
	[JavaScriptResource("/webapp/webui/settings-view.js", "ThinkingHome.Plugins.WebUI.PluginResources.settings-view.js")]
	[HttpResource("/webapp/webui/layout.tpl", "ThinkingHome.Plugins.WebUI.PluginResources.layout.tpl")]
	[HttpResource("/webapp/webui/list-item.tpl", "ThinkingHome.Plugins.WebUI.PluginResources.list-item.tpl")]

	#endregion

	[Plugin]
	public class WebUIPlugin : Plugin
	{
		private readonly List<SystemSectionAttribute> systemSections = new List<SystemSectionAttribute>();

		public override void InitDbModel(ModelMapper mapper)
		{
			mapper.Class<NavigationItem>(cfg => cfg.Table("WebUI_NavigationItem"));
		}

		public override void Init()
		{
			base.Init();

			foreach (var plugin in Context.GetAllPlugins())
			{
				var attributes = plugin.GetType().GetCustomAttributes<SystemSectionAttribute>();
				systemSections.AddRange(attributes);
			}
		}

		[HttpCommand("/api/webui/sections/common")]
		public object GetSections(HttpRequestParams request)
		{
			using (var session = Context.OpenSession())
			{
				var list = session.Query<NavigationItem>()
					.Select(x => new { id = x.Id, name = x.Name, path = x.ModulePath, sortOrder = x.SortOrder })
					.ToArray();

				return list;
			}
		}
		[HttpCommand("/api/webui/sections/system")]
		public object GetSystemSections(HttpRequestParams request)
		{
			var list = systemSections
					.Select(x => new { id = Guid.Empty, name = x.Title, path = x.Url.Trim('/'), sortOrder = x.SortOrder })
					.ToArray();

			return list;

		}
	}
}
