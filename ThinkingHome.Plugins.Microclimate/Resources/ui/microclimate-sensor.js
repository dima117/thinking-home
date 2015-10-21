define(['lib'], function (lib) {

	var sensorWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'<a class="btn btn-primary btn-block th-btn-widget js-open-details">' +
			'<div>{{displayName}}</div>' +
			'{{#with data}}<div class="row">' +
				'<div class="col-xs-5 th-btn-widget-large"><span class="wi wi-thermometer"></span>&nbsp;&nbsp;<span class="js-t">{{t}}</span>&deg;C</div>' +
				'{{#if showHumidity}}<div class="col-xs-7 th-btn-widget-large"><span class="wi wi-humidity"></span>&nbsp;&nbsp;<span class="js-h">{{h}}</span>%</div>{{/if}}' +
			'</div>{{/with}}' +
			'</a>'),
		triggers: {
			'click .js-open-details': 'sensor:details'
		},
		setValues: function(t, h) {
			this.$('.js-t').text(t);
			this.$('.js-h').text(h);
		}
	});

	var sensorWidget = lib.common.Widget.extend({
		show: function (model) {
			var sensorId = model.get('data').id;

			this.view = new sensorWidgetView({ model: model }),

			this.listenTo(this.view, 'sensor:details', this.bind('openDetails', sensorId));
			this.listenTo(this.application.radio, 'microclimate:sensor:update', this.bind('updateSensorData', sensorId));

			this.region.show(this.view);
		},
		updateSensorData: function (sensorId, message) {
			var data = message.data;
			if (data.id === sensorId) {
				this.view.setValues(data.t, data.h);
			}
		},
		openDetails: function (sensorId) {
			this.application.navigate('webapp/microclimate/details', sensorId);
		}
	});

	return sensorWidget;
});