define(['lib'], function (lib) {

	var emptyWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'<div class="btn-group pull-right">' +
				'<a href="#" class="btn btn-default">On</a>' +
				'<a href="#" class="btn btn-default">Off</a>' +
			'</div>{{displayName}}'),
		className: 'clearfix'
	});

	return {
		show: function (model, region) {

			var view = new emptyWidgetView({ model: model });

			region.show(view);
		}
	};
});