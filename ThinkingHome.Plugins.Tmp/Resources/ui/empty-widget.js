define(['lib'], function(lib) {

	var emptyWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile('<div class="th-dashboard-widget-block-title">{{displayName}}</div><p>click me<p>'),
		className: 'th-dashboard-widget-block',
		triggers: {
			"click p": "p:click"
		}
	});

	var clickHandler = function() {

		alert(1);
	};

	return {
		show: function(model, region) {

			var view = new emptyWidgetView({ model: model });
			view.on("p:click", clickHandler);

			region.show(view);
		}
	};
});