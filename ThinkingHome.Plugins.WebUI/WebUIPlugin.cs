using System.Linq;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Handlers;
using ThinkingHome.Plugins.WebUI.Data;

namespace ThinkingHome.Plugins.WebUI
{
	#region respurces

	// html
	[HttpResource("/", "ThinkingHome.Plugins.WebUI.Resources.index.html", "text/html")]
	
	// js
	[HttpResource("/js/vendor/require.min.js", "ThinkingHome.Plugins.WebUI.Resources.js.vendor.require.min.js", "text/javascript")]
	[HttpResource("/js/vendor/json2.min.js", "ThinkingHome.Plugins.WebUI.Resources.js.vendor.json2.min.js", "text/javascript")]
	[HttpResource("/js/vendor/jquery.min.js", "ThinkingHome.Plugins.WebUI.Resources.js.vendor.jquery.min.js", "text/javascript")]
	[HttpResource("/js/vendor/underscore.min.js", "ThinkingHome.Plugins.WebUI.Resources.js.vendor.underscore.min.js", "text/javascript")]
	[HttpResource("/js/vendor/backbone.min.js", "ThinkingHome.Plugins.WebUI.Resources.js.vendor.backbone.min.js", "text/javascript")]
	[HttpResource("/js/vendor/backbone.marionette.min.js", "ThinkingHome.Plugins.WebUI.Resources.js.vendor.backbone.marionette.min.js", "text/javascript")]
	[HttpResource("/js/vendor/bootstrap.min.js", "ThinkingHome.Plugins.WebUI.Resources.js.vendor.bootstrap.min.js", "text/javascript")]
	[HttpResource("/js/vendor/tpl.min.js", "ThinkingHome.Plugins.WebUI.Resources.js.vendor.tpl.min.js", "text/javascript")]

	[HttpResource("/js/application/navigation.js", "ThinkingHome.Plugins.WebUI.Resources.js.application.navigation.js", "text/javascript")]
	[HttpResource("/js/application/item.tpl", "ThinkingHome.Plugins.WebUI.Resources.js.application.item.tpl")]

	[HttpResource("/js/th-main.js", "ThinkingHome.Plugins.WebUI.Resources.js.th-main.js", "text/javascript")]
	[HttpResource("/js/app.js", "ThinkingHome.Plugins.WebUI.Resources.js.app.js", "text/javascript")]

	// css
	[HttpResource("/css/bootstrap.min.css", "ThinkingHome.Plugins.WebUI.Resources.css.bootstrap.min.css", "text/css")]
	[HttpResource("/css/site.css", "ThinkingHome.Plugins.WebUI.Resources.css.site.css", "text/css")]
	
	// fonts
	[HttpResource("/fonts/glyphicons-halflings-regular/.eot", "ThinkingHome.Plugins.WebUI.Resources.css.glyphicons-halflings-regular.eot", "application/vnd.ms-fontobject")]
	[HttpResource("/fonts/glyphicons-halflings-regular/.svg", "ThinkingHome.Plugins.WebUI.Resources.css.glyphicons-halflings-regular.svg", "image/svg+xml")]
	[HttpResource("/fonts/glyphicons-halflings-regular/.ttf", "ThinkingHome.Plugins.WebUI.Resources.css.glyphicons-halflings-regular.ttf", "application/x-font-truetype")]
	[HttpResource("/fonts/glyphicons-halflings-regular/.woff", "ThinkingHome.Plugins.WebUI.Resources.css.glyphicons-halflings-regular.woff", "application/font-woff")]

	// webapp
	[HttpResource("/webapp/webui/settings.js", "ThinkingHome.Plugins.WebUI.PluginResources.settings.js", "text/javascript")]
	[HttpResource("/webapp/webui/settings.tpl", "ThinkingHome.Plugins.WebUI.PluginResources.settings.tpl")]
	[HttpResource("/webapp/webui/table-row.tpl", "ThinkingHome.Plugins.WebUI.PluginResources.table-row.tpl")]

	#endregion

	[Plugin]
	public class WebUIPlugin : Plugin
	{
		public override void InitDbModel(ModelMapper mapper)
		{
			mapper.Class<NavigationItem>(cfg => cfg.Table("WebUI_NavigationItem"));
		}

		[HttpCommand("/api/webui/items")]
		public object GetNavigationItems(HttpRequestParams request)
		{
			using (var session = Context.OpenSession())
			{
				var list = session.Query<NavigationItem>()
					.Select(x => new { id = x.Id, name = x.Name, path = x.ModulePath, sortOrder = x.SortOrder })
					.ToArray();

				return list;
			}
		}
	}
}
