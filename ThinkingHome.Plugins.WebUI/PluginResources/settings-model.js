define(['app'], function (application) {

	application.module('WebUI.Settings', function (module, app, backbone, marionette, $, _) {

		// entities
		module.NavigationItem = backbone.Model.extend({
			urlRoot: 'api/webui/items',
			defaults: {
				sortOrder: 0
			}
		});

		module.NavigationItemCollection = backbone.Collection.extend({
			url: 'api/webui/items',
			model: module.NavigationItem,
			comparator: 'sortOrder'
		});

		// api
		var api = {

			loadNavigationItems: function () {

				var defer = $.Deferred();

				var items = new module.NavigationItemCollection();

				items.fetch({
					success: function (list) {
						defer.resolve(list);
					},
					error: function () {
						defer.resolve(undefined);
					}
				});

				return defer.promise();
			}
		};

		// requests
		app.reqres.setHandler('load:settings:all-items', function () {
			return api.loadNavigationItems();
		});
	});

	return application.WebUI.Settings;
});