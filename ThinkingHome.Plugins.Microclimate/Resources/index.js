define(['app',
		'webapp/microclimate/index-model',
		'webapp/microclimate/index-view'],

	function (application, models, views) {

		var api = {
			details: function (view) {

				var id = view.model.get('id');
				application.navigate('webapp/microclimate/details', id);
			},
			list: function () {

				models.loadSensors()
					.done(function (collection) {

						var view = new views.SensorList({
							collection: collection
						});

						view.on('childview:show:sensor:details', api.details);
						application.setContentView(view);
					});
			}
		};

		return {
			start: api.list
		};
	});