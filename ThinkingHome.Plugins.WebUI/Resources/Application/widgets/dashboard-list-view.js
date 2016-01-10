define([
	'lib',
	'text!application/settings/dashboard-list.tpl',
	'text!application/settings/dashboard-list-item.tpl',
	'lang!application/lang.json'],
	function (lib, listTemplate, listItemTemplate, lang) {

		var listItemView = lib.marionette.ItemView.extend({
			template: lib.handlebars.compile(listItemTemplate),
			triggers: {
				'click .js-open-dashboard': 'dashboard:open',
				'click .js-delete': 'dashboard:delete'
			}
		});

		var listView = lib.marionette.CompositeView.extend({
			template: lib.handlebars.compile(listTemplate),
			childView: listItemView,
			childViewContainer: '.js-list',
			triggers: { 'click .js-create-dashboard': 'dashboard:create' },
			templateHelpers: { lang: lang }
		});

		return {
			DashboardListView: listView
		};
	});