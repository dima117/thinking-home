define(
	['app', 'marionette', 'backbone', 'underscore',
		'webapp/microclimate/index-view',
		'webapp/microclimate/index-model'
	],
	function (application, marionette, backbone, _, views) {

		var api = {
			details: function(view) {

				var id = view.model.get('id');
				application.navigate('webapp/microclimate/details', id);
			},
			list: function() {

				var rq = application.request('query:microclimate:sensors');

				$.when(rq).done(function (collection) {

					var view = new views.SensorList({
						collection: collection
					});

					view.on('childview:show:sensor:details', api.details);
					application.setContentView(view);
				});
			}
		};

		var module = {
			start: api.list
		};
		return module;
	});