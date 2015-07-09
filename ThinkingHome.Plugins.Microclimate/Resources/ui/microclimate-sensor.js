define(['lib'], function(lib) {

	var widgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile('<h3>{{displayName}}</h3>{{#with data}}<p>t: {{t}}&deg;C {{#if h}}, h: {{h}}%{{/if}}<p>{{/with}}')
	});

	return {
		show: function(model, region) {

			var view = new widgetView({ model: model });
			
			region.show(view);
		}
	};
});