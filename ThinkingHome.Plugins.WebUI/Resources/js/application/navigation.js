define(
	['app', 'navigation-eitities', 'navigation-views'],

	function (application) {

		application.module('Navigation', function (module, app, backbone, marionette, $, _) {

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

				getSystemItems: function () {

					var items = new module.NavItemCollection([
						{
							id: '78839E19-38F0-4218-A7AC-E01E2F145997',
							name: 'Settings',
							path: 'webapp/webui/settings'
						}
					]);

					return items;
				},

				loadPage: function (path) {

					if (path) {

						app.navigate(path);

						require([path], function (obj) {
							var view = obj.createView();
							app.regionMain.show(view);
						});
					}
				}
			};

			app.addInitializer(function () {

				// routes
				app.router = new marionette.AppRouter({
					appRoutes: { '*path': 'loadPage' },
					controller: api
				});

				app.on('page:load', function (path) {

					api.loadPage(path);
				});

				// right menu
				var rightItems = api.getSystemItems();

				var rightView = new module.NavMenuView({
					collection: rightItems
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
			});
		});

		return application.Navigation;
	});