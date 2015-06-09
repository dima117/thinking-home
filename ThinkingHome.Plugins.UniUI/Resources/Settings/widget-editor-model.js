define(
	['lib'],
	function (lib) {

		var widgetEditorModel = lib.backbone.Model.extend({
			defaults: {
				id: null,
				type: null,
				dashboardId: null,
				parameters: null,
				displayName: ''
			},
			initialize: function() {
				
				var parameters = this.get('parameters');
				if (parameters) {
					this.set('parameters', new lib.backbone.Collection(parameters));
				}
			},
			toJSON: function() {
				
				var json = lib.backbone.Model.prototype.toJSON.apply(this, arguments);

				if (json.parameters) {
					
					json.parameters = json.parameters.toJSON();
				}
			}
		});

		var api = {
			createWidget: function (dashboardId, type) {

				var defer = lib.$.Deferred();

				lib.$.getJSON('/api/uniui/widget/create', { type: type, dashboard: dashboardId })
					.done(function (data) {

						var model = new widgetEditorModel(data);

						defer.resolve(model);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			},

			editWidget: function (id) {

				var defer = lib.$.Deferred();

				lib.$.getJSON('/api/uniui/widget/edit', { id: id })
					.done(function (data) {

						var model = new widgetEditorModel(data);

						defer.resolve(model);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		return {
			createWidget: api.createWidget,
			editWidget: api.editWidget
		};
	});