define(['lib'], function (lib) {

	var nooUISwitcherWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'<div class="th-widget-block th-pointer btn-primary">' +
				'<div class="th-widget-block-title">{{displayName}}</div>' +
				'<div class="th-widget-block-content">' +
					'<a href="#on" class="btn btn-default">ON</a>' +
					'<a href="#off" class="btn btn-default">OFF</a>' +
				'</div>' +
			'</div>'),
		className: 'th-widget-container',
		triggers: {
			"click .btn": "btn:click"
		}
	});


	return {
		show: function (model, region) {

			var view = new nooUISwitcherWidgetView({ model: model });
			view.on("btn:click", clickHandler);

			region.show(view);
		}
	};
});