define(['lib'], function (lib) {

	var presetLoaderWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'<a href="#" class="btn btn-default btn-block js-btn-load">{{displayName}}</a>'),
		triggers: {
			'click .js-btn-load': 'preset-loader:load'
		}
	});

	var createSender = function (channel, cmd) {
		return function () {
			return lib.$.getJSON('/api/noolite', { ch: channel, cmd: cmd });
		}
	};

	var presetLoaderWidget = lib.common.Widget.extend({
		show: function (model) {
			var view = new presetLoaderWidgetView({ model: model }),
				channel = model.get('data').channel;

			this.listenTo(view, 'preset-loader:load', createSender(channel, 7)); // NooLite Load
			this.region.show(view);
		}
	});

	return presetLoaderWidget;
});
