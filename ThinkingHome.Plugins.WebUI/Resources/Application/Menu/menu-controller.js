define(
	['app', 'menu-model', 'menu-view'],

	function (application) {

		application.module('Menu', function (module, app, backbone, marionette, $, _) {
		
			var controller = {

				reloadMenu: function () {
					
					var rqCommon = app.request('load:menu:common');	// common items
					var rqSystem = app.request('load:menu:system');	// system items
					
					$.when(rqCommon, rqSystem).done(function (itemsCommon, itemsSystem) {
						
						var commonView = new module.CommonMenuView({ collection: itemsCommon });
						app.regionMenu.show(commonView);
						
						var systemView = new module.SystemMenuView({ collection: itemsSystem });
						app.regionMenuRight.show(systemView);
					});
				},

				loadPage: function (path) {

					if (path) {

						require([path], function (obj) {
							
							var view = obj.createView();
							app.regionContent.show(view);
							app.navigate(path);
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

		return application.Menu;
	});