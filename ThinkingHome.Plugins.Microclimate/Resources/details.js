define(
	['app', 'marionette', 'backbone', 'underscore',
		'webapp/microclimate/details-model',
		'webapp/microclimate/details-view'
	],
	function (application, marionette, backbone, _, models, views) {

		var api = {
			details: function (id) {

				models.loadDetails(id)
					.done(function (model) {

						var view = new views.SensorDetails({
							model: model
						});

						view.on('show:sensor:list', api.goBack);
						application.setContentView(view);
					});
			},
			goBack: function () {
				application.navigate('webapp/microclimate/index');
			}
		};

		var module = {
			start: api.details
		};

		return module;
	});