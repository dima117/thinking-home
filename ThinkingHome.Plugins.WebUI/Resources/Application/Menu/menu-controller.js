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
				}
			};

			app.addInitializer(function () {
				controller.reloadMenu();
			});
		});

		return application.Menu;
	});