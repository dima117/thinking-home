define(
	['app', 'navigation-eitities', 'navigation-views'],

	function (application) {

		application.module('Navigation', function (module, app, backbone, marionette, $, _) {
		
			var controller = {

				reloadMenu: function () {

					// system items

					var rightItems = app.request('get:navigation:system');
					var rightView = new module.NavMenuView({ collection: rightItems });
					app.regionNavigationRight.show(rightView);

					// common items
					var rq = app.request('load:navigation:common');
					$.when(rq).done(function (items) {
						
						var leftView = new module.NavMenuView({ collection: items });
						app.regionNavigation.show(leftView);
					});
				},

				loadPage: function (path) {

					if (path) {

						require([path], function (obj) {
							
							var rq = obj.createView();
							
							$.when(rq).done(function (view) {

								app.regionMain.show(view);
								app.navigate(path);
							});
						});
					}
				}
			};

			app.addInitializer(function () {

				// routes
				app.router = new marionette.AppRouter({
					appRoutes: { '*path': 'loadPage' },
					controller: controller
				});

				app.on('page:open', controller.loadPage);
				app.on('menu:reload', controller.reloadMenu);

				controller.reloadMenu();
			});
		});

		return application.Navigation;
	});