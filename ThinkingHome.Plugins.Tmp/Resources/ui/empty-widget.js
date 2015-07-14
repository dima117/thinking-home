define(['lib'], function(lib) {

	var emptyWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile('<h3>{{displayName}}</h3><p>click me<p>'),
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