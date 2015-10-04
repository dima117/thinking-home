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
			var brightness = lib.$(e.target).data("brightness");
			this.trigger('dimmer:set', brightness); // call dimmerSet function with "brightness" data attribute from pressed button
		}
	});

	var dimmerSet = function (brightness) {
		var apiChannel = this.model.get('data').channel;
		return lib.$.getJSON('/api/noolite', { ch: apiChannel, cmd: 6, br: brightness }); // NooLite Set
	};

	return {
		show: function (model, region) {
			var view = new DimmerWidgetView({ model: model });

			view.on("dimmer:set",  dimmerSet);

			region.show(view);
		}
	};
});
