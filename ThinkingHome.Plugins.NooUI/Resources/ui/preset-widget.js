define(['lib'], function (lib) {

	var PresetWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'{{displayName}}' +
				'<div>' +
					'<a href="#" class="btn btn-default th-pointer col-sm-9 col-xs-9 js-btn-load"> Load </a>' +
					'<a href="#" class="btn btn-default th-pointer col-sm-3 col-xs-3 js-btn-save"> Save </a>' +
				'</div>'),
		triggers: {
			"click .js-btn-load": "preset:load",
			"click .js-btn-save": "preset:save"
		}
	});

	var createSender = function (channel, cmd) {
		return function () {
			return lib.$.getJSON('/api/noolite', { ch: channel, cmd: cmd });
		}
	};

	return {
		show: function (model, region) {
			var view = new PresetWidgetView({ model: model });
			var channel = model.get('data').channel;

			view.on("preset:load", createSender(channel, 7)); // NooLite Load
			view.on("preset:save", createSender(channel, 8)); // NooLite Save

			region.show(view);
		}
	};
});
