define(
	['app'],
	function (application) {

		application.module('Common', function (module, app, backbone, marionette, $, _) {

			module.ComplexView = marionette.ItemView.extend({
				parts: [],
				onRender: function () {

					var self = this;

					_.each(this.parts, function (obj, field) {

						var data = self.model.get(field);

						if (data) {

							var params = data instanceof backbone.Collection
								? { collection: data }
								: { model: data };

							var view = new obj.view(params);
							view.render();

							self.$(obj.container).append(view.$el);
						}
					});
				}
			});
		});

		return application.Common;
	});

