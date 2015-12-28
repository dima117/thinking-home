using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.WebUI
{
	#region resources

	// VENDOR

	// js
	[JavaScriptResource("/vendor/js/require.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.require.min.js")]
	[JavaScriptResource("/vendor/js/require-text.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.require-text.js")]
	[JavaScriptResource("/vendor/js/require-json.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.require-json.js")]
	[JavaScriptResource("/vendor/js/require-lang.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.require-lang.js")]

	[JavaScriptResource("/vendor/js/json2.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.json2.min.js")]
	[JavaScriptResource("/vendor/js/jquery.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.jquery.min.js")]

	[JavaScriptResource("/vendor/js/underscore.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.underscore.min.js")]
	[HttpEmbeddedResource("/vendor/js/underscore.min.js.map", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.underscore.min.js.map", "application/json")]

	[JavaScriptResource("/vendor/js/backbone.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.backbone.min.js")]
	[HttpEmbeddedResource("/vendor/js/backbone.min.js.map", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.backbone.min.js.map", "application/json")]

	[JavaScriptResource("/vendor/js/backbone.marionette.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.backbone.marionette.min.js")]
	[HttpEmbeddedResource("/vendor/js/backbone.marionette.min.js.map", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.backbone.marionette.min.js.map", "application/json")]

	[JavaScriptResource("/vendor/js/backbone.syphon.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.backbone.syphon.js")]
	[JavaScriptResource("/vendor/js/bootstrap.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.bootstrap.min.js")]
	[JavaScriptResource("/vendor/js/moment.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.moment.min.js")]
	[JavaScriptResource("/vendor/js/handlebars.amd.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.handlebars.amd.min.js")]

	[JavaScriptResource("/vendor/js/jquery.signalr.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.jquery.signalr.min.js")]


	[JavaScriptResource("/vendor/js/codemirror-all.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror-all.js")]
	[JavaScriptResource("/vendor/js/codemirror.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror.js")]
	[JavaScriptResource("/vendor/js/codemirror-javascript.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror-javascript.js")]
	[JavaScriptResource("/vendor/js/codemirror-closebrackets.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror-closebrackets.js")]
	[JavaScriptResource("/vendor/js/codemirror-matchbrackets.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror-matchbrackets.js")]
	[JavaScriptResource("/vendor/js/codemirror-fullscreen.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror-fullscreen.js")]

	[JavaScriptResource("/vendor/js/chart.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.chart.min.js")]
	[HttpEmbeddedResource("/vendor/js/chart.min.js.map", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.chart.min.js.map", "application/json")]
	[JavaScriptResource("/vendor/js/chart.scatter.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.chart.scatter.min.js")]
	[HttpEmbeddedResource("/vendor/js/chart.scatter.min.js.map", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.chart.scatter.min.js.map", "application/json")]
	
	// css
	[CssResource("/vendor/css/bootstrap.min.css", "ThinkingHome.Plugins.WebUI.Resources.Vendor.css.bootstrap.min.css")]
	[CssResource("/vendor/css/font-awesome.min.css", "ThinkingHome.Plugins.WebUI.Resources.Vendor.css.font-awesome.min.css")]
	[CssResource("/vendor/css/weather-icons.min.css", "ThinkingHome.Plugins.WebUI.Resources.Vendor.css.weather-icons.min.css")]
	[CssResource("/vendor/css/site.css", "ThinkingHome.Plugins.WebUI.Resources.Vendor.css.site.css")]
	[CssResource("/vendor/css/codemirror.css", "ThinkingHome.Plugins.WebUI.Resources.Vendor.css.codemirror.css", AutoLoad = true)]

	// fonts
	[HttpEmbeddedResource("/vendor/fonts/glyphicons-halflings-regular.eot", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.glyphicons-halflings-regular.eot", "application/vnd.ms-fontobject")]
	[HttpEmbeddedResource("/vendor/fonts/glyphicons-halflings-regular.svg", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.glyphicons-halflings-regular.svg", "image/svg+xml")]
	[HttpEmbeddedResource("/vendor/fonts/glyphicons-halflings-regular.ttf", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.glyphicons-halflings-regular.ttf", "application/x-font-truetype")]
	[HttpEmbeddedResource("/vendor/fonts/glyphicons-halflings-regular.woff", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.glyphicons-halflings-regular.woff", "application/font-woff")]
	[HttpEmbeddedResource("/vendor/fonts/glyphicons-halflings-regular.woff2", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.glyphicons-halflings-regular.woff2", "application/font-woff2")]

	[HttpEmbeddedResource("/vendor/fonts/fontawesome-webfont.eot", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.fontawesome-webfont.eot", "application/vnd.ms-fontobject")]
	[HttpEmbeddedResource("/vendor/fonts/fontawesome-webfont.svg", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.fontawesome-webfont.svg", "image/svg+xml")]
	[HttpEmbeddedResource("/vendor/fonts/fontawesome-webfont.ttf", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.fontawesome-webfont.ttf", "application/x-font-truetype")]
	[HttpEmbeddedResource("/vendor/fonts/fontawesome-webfont.woff", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.fontawesome-webfont.woff", "application/font-woff")]
	[HttpEmbeddedResource("/vendor/fonts/fontawesome-webfont.woff2", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.fontawesome-webfont.woff2", "application/font-woff2")]

	[HttpEmbeddedResource("/vendor/fonts/weathericons-regular-webfont.eot", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.weathericons-regular-webfont.eot", "application/vnd.ms-fontobject")]
	[HttpEmbeddedResource("/vendor/fonts/weathericons-regular-webfont.svg", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.weathericons-regular-webfont.svg", "image/svg+xml")]
	[HttpEmbeddedResource("/vendor/fonts/weathericons-regular-webfont.ttf", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.weathericons-regular-webfont.ttf", "application/x-font-truetype")]
	[HttpEmbeddedResource("/vendor/fonts/weathericons-regular-webfont.woff", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.weathericons-regular-webfont.woff", "application/font-woff")]
	[HttpEmbeddedResource("/vendor/fonts/weathericons-regular-webfont.woff2", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.weathericons-regular-webfont.woff2", "application/font-woff2")]

	//APPLICATION

	// html
	[HttpEmbeddedResource("/", "ThinkingHome.Plugins.WebUI.Resources.Application.index.html", "text/html")]
	[HttpEmbeddedResource("/favicon.ico", "ThinkingHome.Plugins.WebUI.Resources.Application.favicon.ico", "image/x-icon")]

	// webapp: main
	[JavaScriptResource("/application/index.js", "ThinkingHome.Plugins.WebUI.Resources.Application.index.js")]
	[JavaScriptResource("/application/lib.js", "ThinkingHome.Plugins.WebUI.Resources.Application.lib.js")]

	// core
	[JavaScriptResource("/application/core/app.js", "ThinkingHome.Plugins.WebUI.Resources.Application.core.app.js")]
	[JavaScriptResource("/application/core/app-router.js", "ThinkingHome.Plugins.WebUI.Resources.Application.core.app-router.js")]
	[JavaScriptResource("/application/core/app-radio.js", "ThinkingHome.Plugins.WebUI.Resources.Application.core.app-radio.js")]
	[JavaScriptResource("/application/core/app-timer.js", "ThinkingHome.Plugins.WebUI.Resources.Application.core.app-timer.js")]

	// layout
	[JavaScriptResource("/application/layout/layout-view.js", "ThinkingHome.Plugins.WebUI.Resources.Application.layout.layout-view.js")]
	[JavaScriptResource("/application/layout/layout.js", "ThinkingHome.Plugins.WebUI.Resources.Application.layout.layout.js")]
	[HttpEmbeddedResource("/application/layout/app-layout.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.layout.app-layout.tpl")]
	[HttpEmbeddedResource("/application/layout/app-menu.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.layout.app-menu.tpl")]

	// webapp: sections
	[JavaScriptResource("/application/sections/system.js", "ThinkingHome.Plugins.WebUI.Resources.Application.sections.system.js")]
	[JavaScriptResource("/application/sections/user.js", "ThinkingHome.Plugins.WebUI.Resources.Application.sections.user.js")]

	[JavaScriptResource("/application/sections/list.js", "ThinkingHome.Plugins.WebUI.Resources.Application.sections.list.js")]
	[JavaScriptResource("/application/sections/list-model.js", "ThinkingHome.Plugins.WebUI.Resources.Application.sections.list-model.js")]
	[JavaScriptResource("/application/sections/list-view.js", "ThinkingHome.Plugins.WebUI.Resources.Application.sections.list-view.js")]
	[HttpEmbeddedResource("/application/sections/list.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.sections.list.tpl")]
	[HttpEmbeddedResource("/application/sections/list-item.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.sections.list-item.tpl")]

	// webapp: libs
	[JavaScriptResource("/application/libs/i18n.js", "ThinkingHome.Plugins.WebUI.Resources.Application.libs.i18n.js")]
	[JavaScriptResource("/application/libs/dashboard-layout.js", "ThinkingHome.Plugins.WebUI.Resources.Application.libs.dashboard-layout.js")]
	[JavaScriptResource("/application/libs/common.js", "ThinkingHome.Plugins.WebUI.Resources.Application.libs.common.js")]

	#endregion

	[Plugin]
	public class WebUIPlugin : PluginBase
	{
		private readonly List<AppSectionAttribute> sections = new List<AppSectionAttribute>();
		private readonly HashSet<string> cssFiles = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
		private readonly Dictionary<string, string> widgets = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

		public override void InitPlugin()
		{
			base.InitPlugin();

			foreach (var plugin in Context.GetAllPlugins())
			{
				var type = plugin.GetType();

				// разделы
				var sectionAttributes = type.GetCustomAttributes<AppSectionAttribute>();
				sections.AddRange(sectionAttributes);

				// стили
				var cssResourceAttributes = type
					.GetCustomAttributes<CssResourceAttribute>()
					.Where(attr => attr.AutoLoad)
					.ToArray();

				var urls = cssResourceAttributes.Select(attr => attr.Url).ToArray();
				cssFiles.UnionWith(urls);

				// виджеты
				var webWidgetAttributes = type
					.GetCustomAttributes<WebWidgetAttribute>()
					.ToArray();

				foreach (var widget in webWidgetAttributes)
				{
					widgets.Add(widget.Type, widget.GetModulePath());
				}
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
				.Select(x => new
				{
					id = Guid.NewGuid(),
					name = x.GetTitle(),
					path = x.GetModulePath(),
					sortOrder = x.SortOrder
				})
				.ToArray();

			return list;
		}

		[HttpCommand("/api/webui/styles.json")]
		public object LoadStyles(HttpRequestParams request)
		{
			return cssFiles;
		}

		[HttpCommand("/api/webui/widgets.json")]
		public object LoadWidgets(HttpRequestParams request)
		{
			return widgets;
		}
	}
}
