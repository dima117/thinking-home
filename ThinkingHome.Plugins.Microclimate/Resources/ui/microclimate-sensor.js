define(['lib'], function (lib) {

	var sensorWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'<a class="btn btn-primary btn-block th-btn-widget">' +
			'<div>{{displayName}}</div>' +
			'{{#with data}}<div class="row">' +
				'<div class="col-xs-5 th-btn-widget-large"><span class="wi wi-thermometer"></span>&nbsp;&nbsp;{{t}}&deg;C</div>' +
				'{{#if h}}<div class="col-xs-7 th-btn-widget-large"><span class="wi wi-humidity"></span>&nbsp;&nbsp;{{h}}%</div>{{/if}}' +
			'</div>{{/with}}' +
			'</a>')
	});

	var sensorWidget = lib.common.Widget.extend({
		show: function (model) {
			var view = new sensorWidgetView({ model: model });

			//this.listenTo(view, 'switcher:on', createSender(channel, 2));
			//this.listenTo(view, 'switcher:off', createSender(channel, 0));
			this.region.show(view);
		}
	});

	return sensorWidget;
});