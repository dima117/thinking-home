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

	// VENDOR

	// js
	[JavaScriptResource("/vendor/js/require.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.require.min.js")]
	[JavaScriptResource("/vendor/js/json2.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.json2.min.js")]
	[JavaScriptResource("/vendor/js/jquery.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.jquery.min.js")]
	[JavaScriptResource("/vendor/js/underscore.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.underscore.min.js")]
	[JavaScriptResource("/vendor/js/backbone.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.backbone.min.js")]
	[JavaScriptResource("/vendor/js/backbone.marionette.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.backbone.marionette.min.js")]
	[JavaScriptResource("/vendor/js/bootstrap.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.bootstrap.min.js")]
	[JavaScriptResource("/vendor/js/tpl.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.tpl.min.js")]

	[JavaScriptResource("/vendor/js/codemirror-all.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror-all.js")]
	[JavaScriptResource("/vendor/js/codemirror.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror.js")]
	[JavaScriptResource("/vendor/js/codemirror-javascript.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror-javascript.js")]
	[JavaScriptResource("/vendor/js/codemirror-closebrackets.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror-closebrackets.js")]
	[JavaScriptResource("/vendor/js/codemirror-matchbrackets.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror-matchbrackets.js")]


	// css
	[HttpResource("/vendor/css/bootstrap.min.css", "ThinkingHome.Plugins.WebUI.Resources.Vendor.css.bootstrap.min.css", "text/css")]
	[HttpResource("/vendor/css/site.css", "ThinkingHome.Plugins.WebUI.Resources.Vendor.css.site.css", "text/css")]
	[HttpResource("/vendor/css/codemirror.css", "ThinkingHome.Plugins.WebUI.Resources.Vendor.css.codemirror.css", "text/css")]
	
	// fonts
	[HttpResource("/vendor/fonts/glyphicons-halflings-regular/.eot", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.glyphicons-halflings-regular.eot", "application/vnd.ms-fontobject")]
	[HttpResource("/vendor/fonts/glyphicons-halflings-regular/.svg", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.glyphicons-halflings-regular.svg", "image/svg+xml")]
	[HttpResource("/vendor/fonts/glyphicons-halflings-regular/.ttf", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.glyphicons-halflings-regular.ttf", "application/x-font-truetype")]
	[HttpResource("/vendor/fonts/glyphicons-halflings-regular/.woff", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.glyphicons-halflings-regular.woff", "application/font-woff")]

	//APPLICATION

	// html
	[HttpResource("/", "ThinkingHome.Plugins.WebUI.Resources.Application.index.html", "text/html")]

	// webapp: main
	[JavaScriptResource("/application/index.js", "ThinkingHome.Plugins.WebUI.Resources.Application.index.js")]
	[JavaScriptResource("/application/app.js", "ThinkingHome.Plugins.WebUI.Resources.Application.app.js")]

	// webapp: menu
	[JavaScriptResource("/application/menu/menu-controller.js", "ThinkingHome.Plugins.WebUI.Resources.Application.Menu.menu-controller.js")]
	[JavaScriptResource("/application/menu/menu-model.js", "ThinkingHome.Plugins.WebUI.Resources.Application.Menu.menu-model.js")]
	[JavaScriptResource("/application/menu/menu-view.js", "ThinkingHome.Plugins.WebUI.Resources.Application.Menu.menu-view.js")]
	[HttpResource("/application/menu/menu-item.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.Menu.menu-item.tpl")]

	// webapp: menu settings
	[SystemSection("Main menu items", "/webapp/webui/settings.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.settings.js")]
	[JavaScriptResource("/webapp/webui/settings-model.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.settings-model.js")]
	[JavaScriptResource("/webapp/webui/settings-view.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.settings-view.js")]
	[HttpResource("/webapp/webui/settings-layout.tpl", "ThinkingHome.Plugins.WebUI.Resources.Plugin.settings-layout.tpl")]
	[HttpResource("/webapp/webui/settings-list-item.tpl", "ThinkingHome.Plugins.WebUI.Resources.Plugin.settings-list-item.tpl")]

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
					.Select(x => new { id = Guid.NewGuid(), name = x.Title, path = x.GetModulePath(), sortOrder = x.SortOrder })
					.ToArray();

			return list;

		}
	}
}
