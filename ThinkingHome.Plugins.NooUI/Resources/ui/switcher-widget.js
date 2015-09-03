define(['lib'], function (lib) {

	var SwitcherWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'<div class="th-widget-block btn-primary">' +
				'<div class="th-widget-block-title">{{displayName}}</div>' +
				'<div class="th-widget-block-content btn-group-justified">' +
					'<a href="#" class="th-widget-block th-pointer btn btn-default js-btn-on"  style="height: 140px;">On</a>' +
					'<a href="#" class="th-widget-block th-pointer btn btn-default js-btn-off" style="height: 140px;">Off</a>' +
				'</div>' +
			'</div>'),
		className: 'th-widget-container',
		triggers: {
			"click .js-btn-on":  "switcher:on",
			"click .js-btn-off": "switcher:off"
		}
	});

	var switcherOn  = function () {
		alert('On');
		console.log(this);
	};

	var switcherOff = function () {
		alert('Off');
	};

	return {
		show: function (model, region) {
			var view = new SwitcherWidgetView({ model: model });

			view.on("switcher:on",  switcherOn);
			view.on("switcher:off", switcherOff);

			region.show(view);
		}
	};
});