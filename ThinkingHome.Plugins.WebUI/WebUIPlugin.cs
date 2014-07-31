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

	[JavaScriptResource("/vendor/js/json2.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.json2.min.js")]
	[JavaScriptResource("/vendor/js/jquery.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.jquery.min.js")]
	[JavaScriptResource("/vendor/js/underscore.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.underscore.min.js")]
	[JavaScriptResource("/vendor/js/backbone.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.backbone.min.js")]
	[JavaScriptResource("/vendor/js/backbone.marionette.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.backbone.marionette.min.js")]
	[JavaScriptResource("/vendor/js/backbone.syphon.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.backbone.syphon.js")]
	[JavaScriptResource("/vendor/js/bootstrap.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.bootstrap.min.js")]

	[JavaScriptResource("/vendor/js/codemirror-all.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror-all.js")]
	[JavaScriptResource("/vendor/js/codemirror.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror.js")]
	[JavaScriptResource("/vendor/js/codemirror-javascript.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror-javascript.js")]
	[JavaScriptResource("/vendor/js/codemirror-closebrackets.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror-closebrackets.js")]
	[JavaScriptResource("/vendor/js/codemirror-matchbrackets.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror-matchbrackets.js")]


	// css
	[CssResource("/vendor/css/bootstrap.min.css", "ThinkingHome.Plugins.WebUI.Resources.Vendor.css.bootstrap.min.css")]
	[CssResource("/vendor/css/font-awesome.min.css", "ThinkingHome.Plugins.WebUI.Resources.Vendor.css.font-awesome.min.css")]
	[CssResource("/vendor/css/site.css", "ThinkingHome.Plugins.WebUI.Resources.Vendor.css.site.css")]
	[CssResource("/vendor/css/framework.css", "ThinkingHome.Plugins.WebUI.Resources.Vendor.css.framework.css")]

	// fonts
	[HttpResource("/vendor/fonts/glyphicons-halflings-regular.eot", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.glyphicons-halflings-regular.eot", "application/vnd.ms-fontobject")]
	[HttpResource("/vendor/fonts/glyphicons-halflings-regular.svg", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.glyphicons-halflings-regular.svg", "image/svg+xml")]
	[HttpResource("/vendor/fonts/glyphicons-halflings-regular.ttf", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.glyphicons-halflings-regular.ttf", "application/x-font-truetype")]
	[HttpResource("/vendor/fonts/glyphicons-halflings-regular.woff", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.glyphicons-halflings-regular.woff", "application/font-woff")]

	[HttpResource("/vendor/fonts/fontawesome-webfont.eot", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.fontawesome-webfont.eot", "application/vnd.ms-fontobject")]
	[HttpResource("/vendor/fonts/fontawesome-webfont.svg", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.fontawesome-webfont.svg", "image/svg+xml")]
	[HttpResource("/vendor/fonts/fontawesome-webfont.ttf", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.fontawesome-webfont.ttf", "application/x-font-truetype")]
	[HttpResource("/vendor/fonts/fontawesome-webfont.woff", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.fontawesome-webfont.woff", "application/font-woff")]

	//APPLICATION

	// html
	[HttpResource("/", "ThinkingHome.Plugins.WebUI.Resources.Application.index.html", "text/html")]

	// webapp: main
	[JavaScriptResource("/application/index.js", "ThinkingHome.Plugins.WebUI.Resources.Application.index.js")]
	[JavaScriptResource("/application/app.js", "ThinkingHome.Plugins.WebUI.Resources.Application.app.js")]
	[JavaScriptResource("/application/common.js", "ThinkingHome.Plugins.WebUI.Resources.Application.common.js")]
	[JavaScriptResource("/application/common/sortable-view.js", "ThinkingHome.Plugins.WebUI.Resources.Application.common.sortable-view.js")]
	[JavaScriptResource("/application/common/complex-view.js", "ThinkingHome.Plugins.WebUI.Resources.Application.common.complex-view.js")]
	[JavaScriptResource("/application/common/form-view.js", "ThinkingHome.Plugins.WebUI.Resources.Application.common.form-view.js")]
	[JavaScriptResource("/application/common/utils.js", "ThinkingHome.Plugins.WebUI.Resources.Application.common.utils.js")]

	// webapp: sections
	[JavaScriptResource("/application/sections/system.js", "ThinkingHome.Plugins.WebUI.Resources.Application.sections.system.js")]
	[JavaScriptResource("/application/sections/user.js", "ThinkingHome.Plugins.WebUI.Resources.Application.sections.user.js")]

	[JavaScriptResource("/application/sections/list.js", "ThinkingHome.Plugins.WebUI.Resources.Application.sections.list.js")]
	[JavaScriptResource("/application/sections/list-model.js", "ThinkingHome.Plugins.WebUI.Resources.Application.sections.list-model.js")]
	[JavaScriptResource("/application/sections/list-view.js", "ThinkingHome.Plugins.WebUI.Resources.Application.sections.list-view.js")]
	[HttpResource("/application/sections/list.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.sections.list.tpl")]
	[HttpResource("/application/sections/list-item.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.sections.list-item.tpl")]

	#endregion

	[Plugin]
	public class WebUIPlugin : Plugin
	{
		private readonly List<AppSectionAttribute> sections = new List<AppSectionAttribute>();
		private readonly HashSet<string> cssFiles = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

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
					name = x.Title,
					path = x.GetModulePath(),
					sortOrder = x.SortOrder,
					tileDefKey = x.TileDefinitionKey
				})
				.ToArray();

			return list;
		}

		[HttpCommand("/api/webui/styles.json")]
		public object LoadStylesBundle(HttpRequestParams request)
		{
			return cssFiles;
		}
	}
}
