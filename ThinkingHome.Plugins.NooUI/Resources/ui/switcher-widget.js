define(['lib'], function (lib) {

	var SwitcherWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'{{displayName}}' +
				'<div class="btn-group-justified">' +
					'<a href="#" class="btn btn-default th-pointer js-btn-on">  On   </a>' +
					'<a href="#" class="btn btn-default th-pointer js-btn-off"> Off  </a>' +
				'</div>'),
		triggers: {
			"click .js-btn-on":  "switcher:on",
			"click .js-btn-off": "switcher:off"
		}
	});

	var createSender = function (channel, cmd) {
		return function () {
			return lib.$.getJSON('/api/noolite', { ch: channel, cmd: cmd });
		}
	};

	return {
		show: function (model, region) {
			var view = new SwitcherWidgetView({ model: model });
			var channel = model.get('data').channel;

			view.on("switcher:on",  createSender(channel, 2)); // NooLite On
			view.on("switcher:off", createSender(channel, 0)); // NooLite Off

			region.show(view);
		}
	};
});
