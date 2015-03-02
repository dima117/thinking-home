define(
	['app',
		'webapp/weather/forecast-model',
		'webapp/weather/forecast-view'],
	function (application, models, views) {

		application.module('Weather.Forecast', function (module, app, backbone, marionette, $, _) {

			var api = {

				addWeatherTile: function (view) {

					var locationId = view.model.get('id');
					app.addTile('ThinkingHome.Plugins.Weather.WeatherTileDefinition', { cityId: locationId });
				},

				reload: function () {

					models.loadList()
						.done(function (collection) {

							var view = new views.WeatherForecastView({
								collection: collection
							});

							view.on('childview:weather:add-tile', api.addWeatherTile);

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