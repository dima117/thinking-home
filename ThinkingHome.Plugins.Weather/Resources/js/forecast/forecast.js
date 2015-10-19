define(
	['lib',
		'webapp/weather/forecast-model',
		'webapp/weather/forecast-view'],
	function (lib, models, views) {

		var forecast = lib.common.AppSection.extend({
			start: function () {
				models.loadList().done(this.bind('displayForecast'));
			},

			displayForecast: function (collection) {
				var view = new views.WeatherForecastView({
					collection: collection
				});

				this.application.setContentView(view);
			}
		});

		return forecast;
	});