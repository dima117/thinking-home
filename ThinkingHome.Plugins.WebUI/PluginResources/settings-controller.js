define(
	['app', 'webapp/webui/settings-model', 'webapp/webui/settings-view'],
	function (application) {

		application.module('WebUI.Settings', function (module, app, backbone, marionette, $, _) {

			var mainView = new module.SettingsLayout();

			var api = {

				reload: function () {

					var rq = app.request('load:settings:all-items');

					$.when(rq).done(function (items) {

						var listView = new module.NavigationItemListView({ collection: items });
						
						mainView.regionList.show(listView);
					});
				}
			};

			module.createView = function () {

				api.reload();
				return mainView;
			};
			
		});

		return application.WebUI.Settings;
	});