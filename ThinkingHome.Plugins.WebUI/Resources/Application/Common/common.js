define(
	['app', 'json!api/webui/styles.json'],
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

			module.LoadCss = function () {

				for (var i = 0; i < arguments.length; i++) {

					$('<link type="text/css" rel="stylesheet" />')
						.attr('href', arguments[i])
						.appendTo("head");
				}
			};

			module.LoadCss(cssFiles);
		});

		return application.Common;
	});