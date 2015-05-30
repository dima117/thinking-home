define([
	'lib',
	'text!webapp/uniui/settings/dashboard-list.tpl',
	'text!webapp/uniui/settings/dashboard-list-item.tpl'],
	function (lib, listTemplate, listItemTemplate) {

		var listItemView = lib.marionette.ItemView.extend({
			template: lib._.template(listItemTemplate)
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