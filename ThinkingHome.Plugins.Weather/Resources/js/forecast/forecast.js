define(
	['app',
		'webapp/weather/forecast-model',
		'webapp/weather/forecast-view'],
	function (application, models, views) {

		var api = {
			addWeatherTile: function (view) {

				var locationId = view.model.get('id');
				application.addTile('ThinkingHome.Plugins.Weather.WeatherTileDefinition', { cityId: locationId });
			},

			reload: function () {

				models.loadList()
					.done(function (collection) {

						var view = new views.WeatherForecastView({
							collection: collection
						});

						view.on('childview:weather:add-tile', api.addWeatherTile);

						application.setContentView(view);
					});
			}
		};

		return {
			start: function () {

				api.reload();
			}
		};
	});