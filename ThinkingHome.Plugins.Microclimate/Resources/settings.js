define(['lib',
		'webapp/microclimate/settings-model',
		'webapp/microclimate/settings-view',
		'lang!webapp/microclimate/lang.json'],
	function (lib, models, views, lang) {

		var microclimateSettings = lib.common.AppSection.extend({
			start: function () {
				this.loadSettings();
			},

			addSensor: function (view) {

				var displayName = view.ui.displayName.val();
				var channel = view.ui.channel.val();
				var showHumidity = view.ui.showHumidity.prop('checked');

				if (displayName) {
					models.addSensor(displayName, channel, showHumidity)
						.done(this.bind('loadSettings'));
				}
			},

			showSensorDetails: function (view) {
				var sensorId = view.model.get('id');
				this.application.navigate('webapp/microclimate/details', sensorId);
			},

			deleteSensor: function (childView) {
				var displayName = childView.model.get('displayName');

				if (lib.utils.confirm(lang.get('Delete_the_sensor_0_and_all_related_data'), displayName)) {
					var id = childView.model.get('id');

					models.deleteSensor(id).done(this.bind('loadSettings'));
				}
			},

			displaySettings: function (collection) {
				var view = new views.SensorTable({
					collection: collection
				});

				this.listenTo(view, 'add:sensor', this.bind('addSensor', view));
				this.listenTo(view, 'childview:delete:sensor', this.bind('deleteSensor'));
				this.listenTo(view, 'childview:show:sensor:details', this.bind('showSensorDetails'));

				this.application.setContentView(view);
			},

			loadSettings: function () {
				models.loadSensorTable().done(this.bind('displaySettings'));
			}
		});

		return microclimateSettings;
	});