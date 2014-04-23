define(['app', 'tpl!application/item.tpl'], function (application, itemTemplate) {

	application.module('Navigation', function (module, app, backbone, marionette, $, _) {
		
		// entities
		module.NavItem = backbone.Model.extend({
			urlRoot: 'api/webui/items'
		});

		module.NavItemCollection = backbone.Collection.extend({
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
		
		// views
		module.NavItemView = marionette.ItemView.extend({
			template: itemTemplate,
			tagName: "li"
		});

		module.Controller = {
			load: function () {
				
				var rq = api.getItems();
				$.when(rq).done(function (items) {
					
					var view = new marionette.CollectionView({
						collection: items,
						tagName: 'ul',
						className: 'nav navbar-nav',
						itemView: module.NavItemView
					});
					
					app.navRegion.show(view);
				});
			}
		};
		
		app.on("initialize:after", module.Controller.load);
	});

	return application.Navigation;
});