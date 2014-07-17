define(
	['app', 'json!api/webui/styles.json', 'application/common/sortable-view'],
	function (application, cssFiles) {

		application.module('Common', function (module, app, backbone, marionette, $, _) {

			module.FormView = marionette.ItemView.extend({

				onRender: function () {

					var data = this.serializeData();

					this.$('select').each(function (index, select) {

						select = $(select);

						var fieldName = select.data('items-field') || (select.attr('name') + '-options');
						var items = data[fieldName];

						if (items) {

							_.each(items, function (item) {

								$('<option />').val(item.id).text(item.name).appendTo(select);
							});
						}
					});

					Backbone.Syphon.deserialize(this, data);
				},

				updateModel: function () {

					var data = Backbone.Syphon.serialize(this);
					this.model.set(data);
				}
			});

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

			module.utils = {
				formatString: function () {

					var s = arguments[0];
					for (var i = 0; i < arguments.length - 1; i++) {
						var reg = new RegExp("\\{" + i + "\\}", "gm");
						s = s.replace(reg, arguments[i + 1]);
					}

					return s;
				},
				alert: function () {
					var msg = module.utils.formatString.apply(null, arguments);
					window.alert(msg);
				},
				confirm: function () {
					var msg = module.utils.formatString.apply(null, arguments);
					return window.confirm(msg);
				}
			};

			// css
			module.LoadCss = function () {

				for (var i = 0; i < arguments.length; i++) {

					$('<link type="text/css" rel="stylesheet" />')
						.attr('href', arguments[i])
						.appendTo("head");
				}
			};

			module.LoadCss.apply(null, cssFiles);
		});

		return application.Common;
	});