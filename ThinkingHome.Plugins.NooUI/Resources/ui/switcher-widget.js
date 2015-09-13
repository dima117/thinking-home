define(['lib'], function (lib) {

	var SwitcherWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'<div class="th-widget-block nooui-widget">' +
				'<div class="th-widget-block-title">{{displayName}}</div>' +
				'<div class="th-widget-block-content btn-group-justified">' +
					'<a href="#" class="btn btn-default nooui-button js-btn-on">  On</a>' +
					'<a href="#" class="btn btn-default nooui-button js-btn-off"> Off</a>' +
				'</div>' +
			'</div>'),
		className: 'th-widget-container',
		triggers: {
			"click .js-btn-on":  "switcher:on",
			"click .js-btn-off": "switcher:off"
		}
	});

	var switcherOn  = function () {
		var req = $.getJSON('/api/noolite?ch=' + this.model.get('data') + '&cmd=on');
		//console.log(req);
	};

	var switcherOff = function (args) {
		var req = $.getJSON('/api/noolite?ch=' + this.model.get('data') + '&cmd=off');
		//console.log(req);
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