define([
	'lib',
	'text!application/settings/dashboard-list.tpl',
	'text!application/settings/dashboard-list-item.tpl'],
	function (lib, listTemplate, listItemTemplate) {

		var listItemView = lib.marionette.ItemView.extend({
			template: lib._.template(listItemTemplate),
			triggers: {
				'click .js-open-dashboard': 'dashboard:open',
				'click .js-delete': 'dashboard:delete'
			}
		});

		var listView = lib.marionette.CompositeView.extend({
			template: lib._.template(listTemplate),
			childView: listItemView,
			childViewContainer: '.js-list',
			triggers: {
				'click .js-create-dashboard': 'dashboard:create'
			}
		});

		return {
			DashboardListView: listView
		};
	});