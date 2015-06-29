define(
	['app',
		'webapp/weather/forecast-model',
		'webapp/weather/forecast-view'],
	function (application, models, views) {

		var api = {

			reload: function () {

				models.loadList()
					.done(function (collection) {

						var view = new views.WeatherForecastView({
							collection: collection
						});
						
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