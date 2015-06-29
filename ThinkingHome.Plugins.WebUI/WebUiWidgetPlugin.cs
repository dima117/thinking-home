using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.WebUI
{
	[AppSection("Dashboard list", SectionType.System, "/webapp/uniui/settings/dashboard-list.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.dashboard-list.js")]
	[JavaScriptResource("/webapp/uniui/settings/dashboard-list-view.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.dashboard-list-view.js")]
	[JavaScriptResource("/webapp/uniui/settings/dashboard-list-model.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.dashboard-list-model.js")]
	[HttpResource("/webapp/uniui/settings/dashboard-list.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.dashboard-list.tpl")]
	[HttpResource("/webapp/uniui/settings/dashboard-list-item.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.dashboard-list-item.tpl")]

	// widget list
	[JavaScriptResource("/webapp/uniui/settings/widget-list.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-list.js")]
	[JavaScriptResource("/webapp/uniui/settings/widget-list-view.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-list-view.js")]
	[JavaScriptResource("/webapp/uniui/settings/widget-list-model.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-list-model.js")]
	[HttpResource("/webapp/uniui/settings/widget-list.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-list.tpl")]
	[HttpResource("/webapp/uniui/settings/widget-list-item.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-list-item.tpl")]

	// widget editor
	[JavaScriptResource("/webapp/uniui/settings/widget-editor.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-editor.js")]
	[JavaScriptResource("/webapp/uniui/settings/widget-editor-view.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-editor-view.js")]
	[JavaScriptResource("/webapp/uniui/settings/widget-editor-model.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-editor-model.js")]
	[HttpResource("/webapp/uniui/settings/widget-editor.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-editor.tpl")]
	[HttpResource("/webapp/uniui/settings/widget-editor-field.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-editor-field.tpl")]

	// dashboard
	[JavaScriptResource("/webapp/uniui/ui/dashboard.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.dashboard.js")]
	[JavaScriptResource("/webapp/uniui/ui/dashboard-view.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.dashboard-view.js")]
	[JavaScriptResource("/webapp/uniui/ui/dashboard-model.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.dashboard-model.js")]
	[HttpResource("/webapp/uniui/ui/dashboard.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.dashboard.tpl")]

	[Plugin]
	public class WebUiWidgetPlugin : PluginBase
	{
	}
}
