define(['lib'], function (lib) {

	var widgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'<div class="th-widget-block th-pointer btn-primary">' +
				'<div class="th-widget-block-title">{{displayName}}</div>' +
				'<div class="th-widget-block-content">' +
					'{{#with data}}' +
					'<div class="row">' +
						'<div class="col-xs-6">{{t}}&deg;C</div>' +
						'{{#if h}}<div class="col-xs-6">{{h}}%</div>{{/if}}' +
					'</div>' +
					'{{/with}}' +
				'</div>' +
			'</div>'),
		className: 'th-widget-container'
	});

	return {
		show: function (model, region) {

			var view = new widgetView({ model: model });

			region.show(view);
		}
	};
});