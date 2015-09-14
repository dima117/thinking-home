using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.WebUI
{
	[AppSection("Dashboard list", SectionType.System, "/application/settings/dashboard-list.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.dashboard-list.js")]
	[JavaScriptResource("/application/settings/dashboard-list-view.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.dashboard-list-view.js")]
	[JavaScriptResource("/application/settings/dashboard-list-model.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.dashboard-list-model.js")]
	[HttpResource("/application/settings/dashboard-list.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.dashboard-list.tpl")]
	[HttpResource("/application/settings/dashboard-list-item.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.dashboard-list-item.tpl")]

	// widget list
	[JavaScriptResource("/application/settings/widget-list.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-list.js")]
	[JavaScriptResource("/application/settings/widget-list-view.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-list-view.js")]
	[JavaScriptResource("/application/settings/widget-list-model.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-list-model.js")]
	[HttpResource("/application/settings/panel-list.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.panel-list.tpl")]
	[HttpResource("/application/settings/widget-list-item.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-list-item.tpl")]
	[HttpResource("/application/settings/panel-list-item.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.panel-list-item.tpl")]

	// widget editor
	[JavaScriptResource("/application/settings/widget-editor.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-editor.js")]
	[JavaScriptResource("/application/settings/widget-editor-view.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-editor-view.js")]
	[JavaScriptResource("/application/settings/widget-editor-model.js", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-editor-model.js")]
	[HttpResource("/application/settings/widget-editor.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-editor.tpl")]
	[HttpResource("/application/settings/widget-editor-field.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.widgets.widget-editor-field.tpl")]

	// dashboard
	[JavaScriptResource("/application/dashboard.js", "ThinkingHome.Plugins.WebUI.Resources.Application.dashboard.dashboard.js")]
	[JavaScriptResource("/application/dashboard-view.js", "ThinkingHome.Plugins.WebUI.Resources.Application.dashboard.dashboard-view.js")]
	[JavaScriptResource("/application/dashboard-model.js", "ThinkingHome.Plugins.WebUI.Resources.Application.dashboard.dashboard-model.js")]
	[HttpResource("/application/dashboard-layout.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.dashboard.dashboard-layout.tpl")]
	[HttpResource("/application/dashboard-widget.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.dashboard.dashboard-widget.tpl")]
	[HttpResource("/application/dashboard-nothing.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.dashboard.dashboard-nothing.tpl")]

	[Plugin]
	public class WebUiWidgetPlugin : PluginBase
	{
	}
}
