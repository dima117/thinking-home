define(['app'], function (application) {

	application.module('Menu', function (module, app, backbone, marionette, $, _) {

		// entities
		module.MenuItem = backbone.Model.extend({
			defaults: {
				sortOrder: 0
			}
		});

		module.MenuItemCollection = backbone.Collection.extend({
			model: module.MenuItem,
			comparator: 'sortOrder'
		});

		// api
		var api = {

			loadSystemItems: function () {

				var defer = $.Deferred();

				$.getJSON('/api/webui/sections/system')
					.done(function (items) {
						var collection = new module.MenuItemCollection(items);
						defer.resolve(collection);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			},

			loadCommonItems: function () {

				var defer = $.Deferred();

				$.getJSON('/api/webui/sections/common')
					.done(function (items) {
						var collection = new module.MenuItemCollection(items);
						defer.resolve(collection);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		// requests
		app.reqres.setHandler('load:menu:system', function () {
			return api.loadSystemItems();
		});

		app.reqres.setHandler('load:menu:common', function () {
			return api.loadCommonItems();
		});
	});

	return application.Menu;
});