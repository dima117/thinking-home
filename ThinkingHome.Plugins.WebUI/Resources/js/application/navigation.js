define(['app'], function (application) {

	application.module('Navigation', function (module, app, bb, bbm, $, _) {
		
		// entities
		module.NavItem = Backbone.Model.extend({
			urlRoot: 'api/webui/items'
		});

		module.NavItemCollection = Backbone.Collection.extend({
			url: 'api/webui/items',
			model: module.NavItem,
			comparator: 'sortOrder'
		});

		var api = {
			getItems: function() {

				var defer = $.Deferred();

				var items = new module.NavItemCollection();

				items.fetch({
					success: function(collection) {
						defer.resolve(collection);
					},
					error: function() {
						defer.resolve(undefined);
					}
				});

				return defer.promise();
			}
		};
		
		app.reqres.setHandler('webui:items', function () {
			return api.getItems();
		});
		
		// views
		module.NavItemView = marionette.ItemView.extend();

	});

	return application.Navigation;
});