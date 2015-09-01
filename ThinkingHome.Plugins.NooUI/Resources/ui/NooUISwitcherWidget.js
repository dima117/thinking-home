define(['lib'], function (lib) {

	var nooUIWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'<div class="th-widget-block th-pointer btn-primary">' +
				'<div class="th-widget-block-title">{{displayName}}</div>' +
				'<div class="th-widget-block-content">' +
					'<a href="#" class="th-widget-block th-pointer btn btn-default nooBtn" style="width: 135px; height: 140px;">On</a>' +
					'<a href="#" class="th-widget-block th-pointer btn btn-default nooBtn" style="width: 50%;   height: 140px;">Off</a>' +
				'</div>' +
			'</div>'),
		className: 'th-widget-container',
		triggers: {
			"click .nooBtn": "btn:click"
		}
	});

	var clickHandler = function () {
		alert('Хри!');
	};

	return {
		show: function (model, region) {

			var view = new nooUIWidgetView({ model: model });
			view.on("btn:click", clickHandler);

			region.show(view);
		}
	};
});