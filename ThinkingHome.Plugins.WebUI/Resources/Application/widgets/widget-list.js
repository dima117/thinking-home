define(['lib',
		'application/settings/widget-list-model.js',
		'application/settings/widget-list-view.js'
],
	function (lib, models, views) {

		var widgetList = lib.common.AppSection.extend({
			start: function (dashboardId) {
				this.loadWidgetList(dashboardId);
			},

			createPanel: function (view) {
				var title = window.prompt('Enter panel title'),
					dashboardId = view.model.get('id');

				if (title) {
					models.createPanel(dashboardId, title).done(this.bind('loadWidgetList', dashboardId));
				}
			},

			renamePanel: function (view, childView) {

				var title = childView.model.get('title'),
					id = childView.model.get('id'),
					dashboardId = view.model.get('id');

				title = window.prompt('Enter new panel title', title);

				if (title) {
					models.renamePanel(id, title).done(this.bind('loadWidgetList', dashboardId));
				}
			},

			deletePanel: function (view, childView) {

				var id = childView.model.get('id'),
					title = childView.model.get('title'),
					dashboardId = view.model.get('id');

				if (lib.utils.confirm('Do you want to delete the "{0}" panel?', title)) {
					models.deletePanel(id).done(this.bind('loadWidgetList', dashboardId));
				}
			},

			createWidget: function (childView) {

				var id = childView.model.get("id"),
					type = childView.ui.typeSelector.val();

				this.application.navigate('application/settings/widget-editor', "create", id, type);
			},

			editWidget: function (panelView, widgetView) {

				var id = widgetView.model.get("id");

				this.application.navigate('application/settings/widget-editor', "edit", id);
			},

			openDashboardList: function () {

				this.application.navigate('application/settings/dashboard-list');
			},

			loadWidgetList: function (dashboardId) {
				models.loadPanels(dashboardId).done(this.bind('displayWidgetList'));
			},

			displayWidgetList: function (data) {

				var view = new views.PanelListView({
					model: data.info,
					collection: data.panels
				});

				this.listenTo(view, 'panel:create', this.bind('createPanel', view));
				this.listenTo(view, 'childview:panel:rename', this.bind('renamePanel', view));
				this.listenTo(view, 'childview:panel:delete', this.bind('deletePanel', view));
				this.listenTo(view, 'childview:widget:create', this.bind('createWidget'));
				this.listenTo(view, 'childview:widget:edit', this.bind('editWidget'));
				this.listenTo(view, 'open:dashboard:list', this.bind('openDashboardList'));

				this.application.setContentView(view);
			}
		});

		return widgetList;
	});