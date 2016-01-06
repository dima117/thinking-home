define(['lib', 'lang!webapp/nooui/lang.json'], function (lib, lang) {

	var presetWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'<div>{{displayName}}</div>' +
			'<div class="row">' +
				'<div class="col-xs-8">' +
					'<a href="#" class="btn btn-default btn-block js-btn-load">{{lang \'Load\'}}</a>' +
				'</div>' +
				'<div class="col-xs-4">' +
					'<a href="#" class="btn btn-default btn-block js-btn-save">{{lang \'Save\'}}</a>' +
				'</div>' +
			'</div>'),
		triggers: {
			'click .js-btn-load': 'preset:load',
			'click .js-btn-save': 'preset:save'
		},
		templateHelpers: { lang: lang }
	});

	var createSender = function (channel, cmd) {
		return function () {
			return lib.$.getJSON('/api/noolite', { ch: channel, cmd: cmd });
		}
	};

	var presetWidget = lib.common.Widget.extend({
		show: function (model) {
			var view = new presetWidgetView({ model: model }),
				channel = model.get('data').channel;

			this.listenTo(view, 'preset:load', createSender(channel, 7)); // NooLite Load
			this.listenTo(view, 'preset:save', createSender(channel, 8)); // NooLite Save

			this.region.show(view);
		}
	});

	return presetWidget;
});
