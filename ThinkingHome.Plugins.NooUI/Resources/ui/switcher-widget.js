define(['lib'], function (lib) {

	var SwitcherWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'{{displayName}}' +
				'<div class="btn-group-justified nooui-btngroup">' +
					'<a href="#" class="btn btn-default nooui-button js-btn-on">  On   </a>' +
					'<a href="#" class="btn btn-default nooui-button js-btn-off"> Off  </a>' +
				'</div>'),
		className: 'nooui-widget',
		triggers: {
			"click .js-btn-on":  "switcher:on",
			"click .js-btn-off": "switcher:off",
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