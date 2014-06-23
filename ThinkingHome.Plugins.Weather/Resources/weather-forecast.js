define(
	['app',
		'webapp/weather/forecast-model',
		'webapp/weather/forecast-view'],
	function (application) {

		application.module('Weather.Forecast', function (module, app, backbone, marionette, $, _) {

			var api = {
				reload: function () {

					var rq = app.request('load:weather:forecast');

					$.when(rq).done(function (collection) {

						var view = new module.WeatherForecastView({
							collection: collection
						});
						
						app.setContentView(view);
					});
				}
			};

			module.start = function () {
				api.reload();
			};

		});

		return application.Weather.Forecast;
	});