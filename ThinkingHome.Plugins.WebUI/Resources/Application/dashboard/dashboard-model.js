define(
	['lib'],
	function (lib) {

		// dima117@todo: дублирование кода
		var widgetCollection = lib.backbone.Collection.extend({
			comparator: 'sortOrder'
		});

		var panelModel = lib.backbone.Model.extend({
			initialize: function () {
				var widgets = this.get('widgets');
				this.set({
					'widgets': new widgetCollection(widgets)
				});
			},

			// dima117@todo: проверить: нужно ли в настройках виджетов переопределять toJSON
			toJSON: function () {
				var json = lib.backbone.Model.prototype.toJSON.call(this);
				json.widgets = json.widgets.toJSON();

				return json;
			}
		});

		var panelCollection = lib.backbone.Collection.extend({
			model: panelModel,
			comparator: 'sortOrder'
		});

		var api = {

			loadDetails: function (id) {

				var defer = lib.$.Deferred();

				lib.$.getJSON('/api/uniui/dashboard/details', { id: id })
					.done(function (data) {

						if (data) {

							var dashboards = new lib.backbone.Collection(data.dashboards);
							var panels = new panelCollection(data.panels);

							defer.resolve({
								dashboards: dashboards,
								panels: panels
							});
						}

						defer.resolve(undefined);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		return {
			loadDetails: api.loadDetails
		};
	});