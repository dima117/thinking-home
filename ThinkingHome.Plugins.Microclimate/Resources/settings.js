define(['app', 'common',
		'webapp/microclimate/settings-model',
		'webapp/microclimate/settings-view'],
	function (application, common, models, views) {

		var api = {
			addSensor: function () {

				var displayName = this.ui.displayName.val();
				var channel = this.ui.channel.val();
				var showHumidity = this.ui.showHumidity.prop('checked');

				if (displayName) {

					models.addSensor(displayName, channel, showHumidity)
						.done(api.loadSettings);
				}
			},
			showSensorDetails: function(view) {
				
				var sensorId = view.model.get('id');
				application.navigate('webapp/microclimate/details', sensorId);
			},
			deleteSensor: function (childView) {

				var displayName = childView.model.get('displayName');

				if (common.utils.confirm('Delete the sensor "{0}" and all related data?', displayName)) {

					var id = childView.model.get('id');

					models.deleteSensor(id).done(api.loadSettings);
				}
			},
			loadSettings: function () {

				models.loadSensorTable()
					.done(function (collection) {

						var view = new views.SensorTable({
							collection: collection
						});

						view.on('add:sensor', api.addSensor);
						view.on('childview:delete:sensor', api.deleteSensor);
						view.on('childview:show:sensor:details', api.showSensorDetails);
						application.setContentView(view);
					});
			}
		};

		return {
			start: api.loadSettings
		};
	});