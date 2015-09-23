define(['lib'], function (lib) {

	var DimmerWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'{{displayName}}' +
				'<div class="btn-group-justified nooui-btngroup">' +
					'<a href="#" class="btn btn-default nooui-button js-btn-set" data-brightness="0">   0   </a>' +
					'<a href="#" class="btn btn-default nooui-button js-btn-set" data-brightness="30">  25  </a>' +
					'<a href="#" class="btn btn-default nooui-button js-btn-set" data-brightness="50">  50  </a>' +
					'<a href="#" class="btn btn-default nooui-button js-btn-set" data-brightness="75">  75  </a>' +
					'<a href="#" class="btn btn-default nooui-button js-btn-set" data-brightness="100"> 100 </a>' +
				'</div>'),
		className: 'nooui-widget',
		events: {
			"click .js-btn-set": "btnSetClick",
		},
		btnSetClick: function (e) {
			this.trigger('dimmer:set',lib.$(e.target).data("brightness"));
		}
	});

	var dimmerSet = function (brightness) {
		var apiCommand = 6; // NooLite Set
		var apiAddr = '/api/noolite';
		var apiChannel = this.model.get('data').channel;
		return lib.$.getJSON(apiAddr, { ch: apiChannel, cmd: apiCommand, br: brightness });
	};

	return {
		show: function (model, region) {
			var view = new DimmerWidgetView({ model: model });

			view.on("dimmer:set",  dimmerSet);

			region.show(view);
		}
	};
});