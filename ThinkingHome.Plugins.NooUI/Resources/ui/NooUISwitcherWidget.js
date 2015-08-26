define(['lib'], function (lib) {

	var emptyWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'<div class="th-widget-block th-pointer btn-primary">On</div>' +
			'<div class="th-widget-block th-pointer btn-primary">Off</div>'),
		className: 'th-widget-container'
	});

	return {
		show: function (model, region) {

			var view = new emptyWidgetView({ model: model });

			region.show(view);
		}
	};
});