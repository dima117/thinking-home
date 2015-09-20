define(['lib'], function (lib) {

	var widgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'<p>{{displayName}}</p>' +
			'{{#with data}}' +
				'<p>{{t}}&deg;C</p>' +
				'{{#if h}}<p>{{h}}%</p>{{/if}}' +
			'{{/with}}')
	});

	return {
		show: function (model, region) {

			var view = new widgetView({ model: model });

			region.show(view);
		}
	};
});