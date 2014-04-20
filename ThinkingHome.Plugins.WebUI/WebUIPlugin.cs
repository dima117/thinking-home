using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener.Handlers;

namespace ThinkingHome.Plugins.WebUI
{
	[Plugin]

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

	[HttpResource("/js/th-main.js", "ThinkingHome.Plugins.WebUI.Resources.js.th-main.js", "text/javascript")]
	[HttpResource("/js/app.js", "ThinkingHome.Plugins.WebUI.Resources.js.app.js", "text/javascript")]

	// css
	[HttpResource("/css/bootstrap-theme.min.css", "ThinkingHome.Plugins.WebUI.Resources.css.bootstrap-theme.min.css", "text/css")]
	[HttpResource("/css/bootstrap.min.css", "ThinkingHome.Plugins.WebUI.Resources.css.bootstrap.min.css", "text/css")]

	// fonts
	[HttpResource("/fonts/glyphicons-halflings-regular/.eot", "ThinkingHome.Plugins.WebUI.Resources.css.glyphicons-halflings-regular.eot", "application/vnd.ms-fontobject")]
	[HttpResource("/fonts/glyphicons-halflings-regular/.svg", "ThinkingHome.Plugins.WebUI.Resources.css.glyphicons-halflings-regular.svg", "image/svg+xml")]
	[HttpResource("/fonts/glyphicons-halflings-regular/.ttf", "ThinkingHome.Plugins.WebUI.Resources.css.glyphicons-halflings-regular.ttf", "application/x-font-truetype")]
	[HttpResource("/fonts/glyphicons-halflings-regular/.woff", "ThinkingHome.Plugins.WebUI.Resources.css.glyphicons-halflings-regular.woff", "application/font-woff")]


	public class WebUIPlugin : Plugin
	{
	}
}
