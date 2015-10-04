define(['lib'], function (lib) {

	var DimmerWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'<div>{{displayName}}</div>' +
			'<div class="btn-group btn-group-justified">' +
				'<a href="#" class="btn btn-default th-pointer js-btn-set" data-brightness="0">   0   </a>' +
				'<a href="#" class="btn btn-default th-pointer js-btn-set" data-brightness="30">  30  </a>' +
				'<a href="#" class="btn btn-default th-pointer js-btn-set" data-brightness="50">  50  </a>' +
				'<a href="#" class="btn btn-default th-pointer js-btn-set" data-brightness="70">  70  </a>' +
				'<a href="#" class="btn btn-default th-pointer js-btn-set" data-brightness="100"> 100 </a>' +
			'</div>'),
		events: {
			"click .js-btn-set": "btnSetClick",
		},
		btnSetClick: function (e) {
			this.trigger('dimmer:set',lib.$(e.target).data("brightness")); // call dimmerSet function with "brightness" data attribute from pressed button
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
