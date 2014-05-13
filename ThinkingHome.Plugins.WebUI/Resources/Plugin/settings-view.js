define(
	['app', 'tpl!webapp/webui/settings-list.tpl', 'tpl!webapp/webui/settings-list-item.tpl'],
	function (application, listTemplate, itemTemplate) {

		application.module('WebUI.Settings', function (module, app, backbone, marionette, $, _) {

			module.NavigationItemView = marionette.ItemView.extend({
				template: itemTemplate,
				className: 'media'
			});

			module.NavigationItemListView = marionette.CompositeView.extend({
				template: listTemplate,
				itemView: module.NavigationItemView,
				itemViewContainer: '.ph-list'
			});
		});

		return application.WebUI.Settings;
	});