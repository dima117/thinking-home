define(['app'], function(application) {

	application.module('Navigation', function(module, app, backbone, marionette, $, _) {

		// entities
		module.NavItem = backbone.Model.extend({
			urlRoot: 'api/webui/items',
			defaults: { 
				sortOrder: 0
			}
		});

		module.NavItemCollection = backbone.Collection.extend({
			url: 'api/webui/items',
			model: module.NavItem,
			comparator: 'sortOrder'
		});
		
		// api
		var api = {

			getSystemItems: function () {

				var items = new module.NavItemCollection([
						{
							id: '78839E19-38F0-4218-A7AC-E01E2F145997',
							name: 'Settings',
							path: 'webapp/webui/settings-controller'
						}
				]);

				return items;
			},

			loadCommonItems: function () {

				var defer = $.Deferred();

				var items = new module.NavItemCollection();

				items.fetch({
					success: function (collection) {
						defer.resolve(collection);
					},
					error: function () {
						defer.resolve(undefined);
					}
				});

				return defer.promise();
			}
		};
		
		// requests
		app.reqres.setHandler('get:navigation:system', function () {
			return api.getSystemItems();
		});

		app.reqres.setHandler('load:navigation:common', function () {
			return api.loadCommonItems();
		});
	});
	
	return application.Navigation;
});