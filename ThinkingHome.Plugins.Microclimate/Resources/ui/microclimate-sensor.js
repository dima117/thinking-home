define(['lib'], function(lib) {

	var widgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile('<div class="th-dashboard-widget-block-title">{{displayName}}</div>{{#with data}}<p>t: {{t}}&deg;C {{#if h}}, h: {{h}}%{{/if}}<p>{{/with}}'),
		className: 'th-dashboard-widget-block'
	});

	return {
		show: function(model, region) {

			var view = new widgetView({ model: model });
			
			region.show(view);
		}
	};
});