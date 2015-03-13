define(['lib'], function (lib) {

	var complexView = lib.marionette.ItemView.extend({
		parts: [],
		onRender: function () {

			var self = this;

			lib._.each(this.parts, function (obj, field) {

				var data = self.model.get(field);

				if (data) {

					var params = data instanceof lib.backbone.Collection
						? { collection: data }
						: { model: data };

					var view = new obj.view(params);
					view.render();

					self.$(obj.container).append(view.$el);
				}
			});
		}
	});

	return complexView;
});

