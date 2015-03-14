define(
	['lib', 'syphon'],
	function (lib, syphon) {

		var formView = lib.marionette.ItemView.extend({

			onRender: function () {

				var data = this.serializeData();

				this.$('select').each(function (index, select) {

					select = lib.$(select);

					var fieldName = select.data('items-field') || (select.attr('name') + '-options');
					var items = data[fieldName];

					if (items) {

						lib._.each(items, function (item) {

							lib.$('<option />').val(item.id).text(item.name).appendTo(select);
						});
					}
				});

				syphon.deserialize(this, data);
			},

			updateModel: function () {

				var data = syphon.serialize(this);
				this.model.set(data);
			}
		});

		return formView;
	});

