using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Handlers;
using ThinkingHome.Plugins.WebUI.Attributes;

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
	[JavaScriptResource("/vendor/js/backbone.syphon.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.backbone.syphon.js")]
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

	// webapp: common
	[JavaScriptResource("/application/common/common.js", "ThinkingHome.Plugins.WebUI.Resources.Application.Common.common.js")]

	// webapp: menu settings
	[JavaScriptResource("/webapp/webui/section-list-common.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.section-list-common.js")]
	[JavaScriptResource("/webapp/webui/section-list-system.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.section-list-system.js")]
	[JavaScriptResource("/webapp/webui/section-model.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.section-model.js")]
	[JavaScriptResource("/webapp/webui/section-view.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.section-view.js")]
	[HttpResource("/webapp/webui/section-list.tpl", "ThinkingHome.Plugins.WebUI.Resources.Plugin.section-list.tpl")]
	[HttpResource("/webapp/webui/section-list-item.tpl", "ThinkingHome.Plugins.WebUI.Resources.Plugin.section-list-item.tpl")]

	#endregion

	[Plugin]
	public class WebUIPlugin : Plugin
	{
		private readonly List<AppSectionAttribute> sections = new List<AppSectionAttribute>();

		public override void Init()
		{
			base.Init();

			foreach (var plugin in Context.GetAllPlugins())
			{
				var attributes = plugin.GetType().GetCustomAttributes<AppSectionAttribute>();
				sections.AddRange(attributes);
			}
		}

		[HttpCommand("/api/webui/sections/common")]
		public object GetSections(HttpRequestParams request)
		{
			return GetSections(SectionType.Common);
		}

		[HttpCommand("/api/webui/sections/system")]
		public object GetSystemSections(HttpRequestParams request)
		{
			return GetSections(SectionType.System);
		}

		private object GetSections(SectionType sectionType)
		{
			var list = sections
				.Where(section => section.Type == sectionType)
				.Select(x => new {id = Guid.NewGuid(), name = x.Title, path = x.GetModulePath(), sortOrder = x.SortOrder})
				.ToArray();

			return list;
		}
	}
}
