define(['lib'], function (lib) {

	var SwitcherWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'{{displayName}}' +
				'<div class="btn-group-justified">' +
					'<a href="#" class="btn btn-default nooui-button js-btn-on" data="ololo find me">  On</a>' +
					'<a href="#" class="btn btn-default nooui-button js-btn-off"> Off</a>' +
				'</div>'),
		className: 'nooui-widget',
		events: {
			"click .js-btn-on": "omon",
		},
		omon: function (e) {
			console.log($(e));
		},
		triggers: {
			"click .js-btn-off": "switcher:off"
		}
	});

	var switcherOn  = function (attrs) {
		var req = $.getJSON('/api/noolite?ch=' + this.model.get('data') + '&cmd=on');
		console.log($(attrs.target));
	};

	var switcherOff = function (args) {
		var req = $.getJSON('/api/noolite?ch=' + this.model.get('data') + '&cmd=off');
		console.log(this);
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