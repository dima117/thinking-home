define([
	'lib',
	'text!webapp/uniui/settings/dashboard-list.tpl',
	'text!webapp/uniui/settings/dashboard-list-item.tpl'],
	function (lib, listTemplate, listItemTemplate) {

		var listItemView = lib.marionette.ItemView.extend({
			template: lib._.template(listItemTemplate),
			triggers: {
				"click .js-move-up": "dashboard:move:up",
				"click .js-move-down": "dashboard:move:down",
				"click .js-rename": "dashboard:rename",
				"click .js-delete": "dashboard:delete"
			}
		});

		var listView = lib.marionette.CompositeView.extend({
			template: lib._.template(listTemplate),
			childView: listItemView,
			childViewContainer: '.js-list',
		});

		return {
			DashboardListView: listView
		};
	});