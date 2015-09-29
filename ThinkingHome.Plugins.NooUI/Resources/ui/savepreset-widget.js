define(['lib'], function (lib) {

	var SavePresetWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'{{displayName}}' +
				'<div class="btn-group btn-group-justified">' +
					'<a href="#" class="btn btn-default th-pointer js-btn-save"> Save </a>' +
				'</div>'),
		triggers: {
			"click .js-btn-save": "savepreset:save",
		}
	});

	var createSender = function (channel, cmd) {
		return function () {
			return lib.$.getJSON('/api/noolite', { ch: channel, cmd: cmd });
		}
	};

	return {
		show: function (model, region) {
			var view = new SavePresetWidgetView({ model: model });
			var channel = model.get('data').channel;

			view.on("savepreset:save", createSender(channel, 8)); // NooLite Save

			region.show(view);
		}
	};
});
