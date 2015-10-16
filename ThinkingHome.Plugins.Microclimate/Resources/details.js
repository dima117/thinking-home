define(
	['lib', 'webapp/microclimate/details-model', 'webapp/microclimate/details-view'],
	function (lib, models, views) {

		var sensorDetails = lib.common.AppSection.extend({
			start: function (id) {
				this.loadDetails(id);
				this.listenTo(
					this.application.radio,
					'microclimate:sensor:update',
					this.bind('onSensorUpdate', id));
			},

			onSensorUpdate: function (sensorId, message) {
				if (message.data.id == sensorId) {
					this.loadDetails(sensorId);
				}
			},

			loadDetails: function (id) {
				models.loadDetails(id).done(this.bind('displayDetails'));
			},

			displayDetails: function (model) {
				var view = new views.SensorDetails({
					model: model
				});

				this.application.setContentView(view);
			}
		});

		return sensorDetails;
	});