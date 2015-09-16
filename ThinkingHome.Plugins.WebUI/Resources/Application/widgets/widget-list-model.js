define(
	['lib'],
	function (lib) {
		
		var widgetCollection = lib.backbone.Collection.extend({
			comparator: "sortOrder"
		});

		var panelModel = lib.backbone.Model.extend({
			initialize: function () {
				var widgets = this.get('widgets');
				this.set({
					'widgets': new widgetCollection(widgets)
				});
			}
		});

		var panelCollection = lib.backbone.Collection.extend({
			model: panelModel,
			comparator: "sortOrder"
		});

		var api = {
			createPanel: function (dashboardId, title) {

				var rq = lib.$.post('/api/uniui/panel/create', {
					dashboard: dashboardId,
					title: title
				});

				return rq.promise();
			},

			renamePanel: function (id, title) {

				var rq = lib.$.post('/api/uniui/panel/rename', { id: id, title: title });
				return rq.promise();
			},

			deletePanel: function (id) {

				var rq = lib.$.post('/api/uniui/panel/delete', { id: id });
				return rq.promise();
			},

			loadPanels: function (id) {

				var defer = lib.$.Deferred();

				lib.$.getJSON('/api/uniui/panel/list', { id: id })
					.done(function (data) {

						var info = new lib.backbone.Model(data.info),
							panels = new panelCollection(data.panels);

						defer.resolve({ info: info, panels: panels });
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		return {
			loadPanels: api.loadPanels,
			createPanel: api.createPanel,
			renamePanel: api.renamePanel,
			deletePanel: api.deletePanel
		};
	});