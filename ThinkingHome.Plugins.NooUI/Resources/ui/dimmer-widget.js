define(['lib'], function (lib) {

	var DimmerWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'<div>{{displayName}}</div>' +
			'<div class="btn-group btn-group-justified">' +
				'{{#range 0 100 25}}' +
					'<a href="#" class="btn btn-default js-btn-set" data-brightness="{{this}}">{{this}}</a>' +
				'{{/range}}' +
			'</div>'),
		events: {
			"click .js-btn-set": "btnSetClick",
		},
		btnSetClick: function (e) {
			e.preventDefault();
			e.stopPropagation();

			var channel = this.model.get('data').channel,
				brightness = lib.$(e.target).data("brightness");
			this.trigger('dimmer:set', channel, brightness);
		}
	});

	var dimmerWidget = lib.common.Widget.extend({
		show: function (model, region) {
			var view = new DimmerWidgetView({ model: model });

			this.listenTo(view, 'dimmer:set', function (channel, brightness) {
				lib.$.getJSON('/api/noolite', { ch: channel, cmd: 6, br: brightness });
			});

			this.region.show(view);
		}	
	});

	return dimmerWidget;
});
