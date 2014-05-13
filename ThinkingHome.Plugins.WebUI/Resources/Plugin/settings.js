define(
	['app', 'webapp/webui/settings-model', 'webapp/webui/settings-view'],
	function (application) {

		application.module('WebUI.Settings', function (module, app, backbone, marionette, $, _) {

			var api = {

				reload: function () {

					var rq = app.request('load:settings:all-items');

					$.when(rq).done(function (items) {

						var view = new module.NavigationItemListView({ collection: items });
						app.setContentView(view);
					});
				}
			};

			module.start = function () {
				api.reload();
			};
			
		});

		return application.WebUI.Settings;
	});