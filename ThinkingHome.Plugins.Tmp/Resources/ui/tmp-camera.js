define(['lib'], function (lib) {

	var cameraWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile('<img src="" style="width: 100%;" />'),
		setImageUrl: function (url) {
			this.$('img').attr('src', url);
		}
	});

	var dimmerWidget = lib.common.Widget.extend({
		show: function (model) {
			var view = new cameraWidgetView({ model: model });
			this.updateImage(view);
			this.region.show(view);

			this.interval = window.setInterval(this.bind('updateImage', view), 2500);
		},
		updateImage: function(view) {
			lib.$.getJSON('/api/tmp/camera/image').done(function (data) {
				view.setImageUrl(data.url);
			});
		},
		onBeforeDestroy: function() {
			window.clearInterval(this.interval);
		}
	});

	return dimmerWidget;
});
