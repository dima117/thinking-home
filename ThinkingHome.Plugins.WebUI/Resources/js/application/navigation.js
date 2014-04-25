define(['app', 'tpl!js/application/item.tpl'], function (application, itemTemplate) {

	application.module('Navigation', function (module, app, backbone, marionette, $, _) {

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

		var api = {
			getItems: function () {

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
			},
			loadPage: function (path) {

				require([path], function (obj) {
					var view = obj.createView();
					app.regionMain.show(view);
				});
			}
		};

		// views
		module.NavItemView = marionette.ItemView.extend({
			template: itemTemplate,
			tagName: 'li',
			events: {
				'click a': 'itemClicked'
			},
			itemClicked: function (e) {
				e.preventDefault();

				var path = this.model.get('path');
				api.loadPage(path);
			}
		});

		module.NavMenuView = marionette.CollectionView.extend({
			tagName: 'ul',
			className: 'nav navbar-nav',
			itemView: module.NavItemView
		});

		module.Controller = {
			load: function () {

				// right menu
				var rightMenuItems = new module.NavItemCollection([
					{
						id: '78839E19-38F0-4218-A7AC-E01E2F145997',
						name: 'Settings',
						path: 'webapp/webui/settings'
					}
				]);
				
				var rightView = new module.NavMenuView({
					collection: rightMenuItems
				});

				app.regionNavigationRight.show(rightView);

				// main menu
				var rq = api.getItems();
				$.when(rq).done(function (items) {

					var view = new module.NavMenuView({
						collection: items
					});

					app.regionNavigation.show(view);
				});
			}
		};

		app.on("initialize:after", module.Controller.load);
	});

	return application.Navigation;
});