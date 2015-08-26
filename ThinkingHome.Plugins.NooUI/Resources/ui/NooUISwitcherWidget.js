define(['lib'], function (lib) {

<<<<<<< HEAD
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
=======
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
>>>>>>> NooUI-plugin-(2nd-copy)
});