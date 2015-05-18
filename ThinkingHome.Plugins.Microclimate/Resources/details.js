define(['app',
		'webapp/microclimate/details-model',
		'webapp/microclimate/details-view'],
	function (application, models, views) {

		var api = {
			details: function (id) {

				models.loadDetails(id)
					.done(function (model) {

						var view = new views.SensorDetails({
							model: model
						});

						application.setContentView(view);
					});
			}
		};

		return {
			start: api.details
		};
	});