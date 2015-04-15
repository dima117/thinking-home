define(
	['app', 'marionette', 'backbone', 'underscore',
		'webapp/microclimate/details-view',
		'webapp/microclimate/details-model'
	],
	function (application, marionette, backbone, _, views) {

		var api = {
			details: function(id) {

				var rq = application.request('query:microclimate:details', id);

				$.when(rq).done(function (model) {

					var view = new views.SensorDetails({
						model: model
					});

					view.on('show:sensor:list', api.goBack);
					application.setContentView(view);
				});
			},
			goBack: function() {
				application.navigate('webapp/microclimate/index');
			}
		};

		var module = {
			start: api.details
		};

		return module;
	});