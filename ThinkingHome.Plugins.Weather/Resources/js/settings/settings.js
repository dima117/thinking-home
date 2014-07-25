define(
	['app',
		'webapp/weather/settings-model',
		'webapp/weather/settings-view'],
	function (application) {

		application.module('Weather.Settings', function (module, app, backbone, marionette, $, _) {

			var layoutView;

			var api = {

			};

			module.start = function () {

				// init layout
				layoutView = new module.WeatherSettingsLayout();
				app.setContentView(layoutView);

				// api.reloadForm();
				// api.reloadList();
			};

		});

		return application.Weather.Settings;
	});