define(
	['app',
		'webapp/weather/settings-model',
		'webapp/weather/settings-view'],
	function (application) {

		application.module('Weather.Settings', function (module, app, backbone, marionette, $, _) {

			var layoutView;

			var api = {

				addLocation: function() {
					alert('add location');
				},

				reloadForm: function () {

					//app.request('load:scripts:subscription-form')
					//	.done(function (formData) {

					var form = new module.WeatherSettingsFormView();//({ model: formData });
					form.on('weather:location:add', api.addLocation);
					layoutView.regionForm.show(form);
					//});
				}
			};

			module.start = function () {

				// init layout
				layoutView = new module.WeatherSettingsLayout();
				app.setContentView(layoutView);

				api.reloadForm();
				// api.reloadList();
			};

		});

		return application.Weather.Settings;
	});