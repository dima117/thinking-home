define(
	['app', 'tpl!webapp/webui/settings-layout.tpl', 'tpl!webapp/webui/settings-list-item.tpl'],
	function (application, layoutTemplate, itemTemplate) {

		application.module('WebUI.Settings', function (module, app, backbone, marionette, $, _) {

			module.NavigationItemView = marionette.ItemView.extend({
				template: itemTemplate,
				className: 'media'
			});

			module.NavigationItemListView = marionette.CollectionView.extend({
				itemView: module.NavigationItemView
			});

			module.SettingsLayout = marionette.Layout.extend({
				template: layoutTemplate,
				regions: {
					regionList: '.ph-list'
				}
			});
		});

		return application.WebUI.Settings;
	});