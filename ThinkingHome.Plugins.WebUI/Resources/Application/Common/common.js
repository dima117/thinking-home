define(
	['app'],
	function (application) {

		application.module('Common', function (module, app, backbone, marionette, $, _) {

			module.FormView = marionette.ItemView.extend({

				onRender: function () {

					var data = this.serializeData();

					this.$('select').each(function(index, select) {

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
				
				updateModel: function() {
				
					var data = Backbone.Syphon.serialize(this);
					this.model.set(data);
				}
			});

		});

		return application.Common;
	});