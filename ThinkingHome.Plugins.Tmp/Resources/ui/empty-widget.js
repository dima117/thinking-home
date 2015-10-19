define(['lib'], function (lib) {

	var emptyWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'{{displayName}} <a href="#" class="btn btn-default">click me</a>'),
		triggers: {
			"click .btn": "btn:click"
		}
	});

	var clickHandler = function () {

		alert(1);
	};

	return {
		show: function (model, region) {

			var view = new emptyWidgetView({ model: model });
			view.on("btn:click", clickHandler);

			// обращаемся снаружи к DOM элементам представления - не делайте так никогда
			//application.radio.on('server:time', function (msg) {
			//	view.$('.btn').text(msg.data);
			//});

			region.show(view);
		}
	};
});