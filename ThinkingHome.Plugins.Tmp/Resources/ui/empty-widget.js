define(['lib'], function (lib) {

	var emptyWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'<div class="th-widget-block th-pointer btn-primary">' +
				'<div class="th-widget-block-title">{{displayName}}</div>' +
				'<div class="th-widget-block-content">' +
					'<a href="#" class="btn btn-default">click me</a>' +
				'</div>'+
			'</div>'),
		className: 'th-widget-container',
		triggers: {
			"click .btn": "btn:click"
		}
	});

	var clickHandler = function () {

		alert(1);
	};

	return {
		show: function (model, region) {

			var view = new emptyWidgetView({ model: model });
			view.on("btn:click", clickHandler);

			region.show(view);
		}
	};
});