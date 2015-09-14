define(['app', 'lib',
		'application/settings/widget-list-model.js',
		'application/settings/widget-list-view.js'
],
	function (application, lib, models, views) {

		var api = {

			//createWidget: function () {

			//	var id = this.model.get("id"),
			//		type = this.ui.typeSelector.val();

			//	application.navigate('application/settings/widget-editor', "create", id, type);
			//},

			//editWidget: function (view) {

			//	var id = view.model.get("id");

			//	application.navigate('application/settings/widget-editor', "edit", id);
			//},

			openDashboardList: function () {

				application.navigate('application/settings/dashboard-list');
			},

			createPanel: function () {

				var title = window.prompt('Enter panel title'),
					dashboardId = this.model.get('id');

				if (title) {

					models.createPanel(dashboardId, title).done(function () {
						api.loadWidgetList(dashboardId);
					});
				}
			},

			renamePanel: function (childView) {

				var title = childView.model.get('title'),
					id = childView.model.get('id'),
					dashboardId = this.model.get('id');

				title = window.prompt('Enter new panel title', title);

				if (title) {

					models.renamePanel(id, title).done(function () {
						api.loadWidgetList(dashboardId);
					});
				}
			},

			deletePanel: function (childView) {

				var id = childView.model.get('id'),
					title = childView.model.get('title'),
					dashboardId = this.model.get('id');

				if (lib.utils.confirm('Do you want to delete the "{0}" panel?', title)) {

					models.deletePanel(id).done(function () {
						api.loadWidgetList(dashboardId);
					});
				}
			},

			loadWidgetList: function (id) {

				models.loadPanels(id)
					.done(function (data) {

						var view = new views.PanelListView({
							model: data.info,
							collection: data.panels
						});

						view.on("panel:create", api.createPanel);
						view.on("childview:panel:rename", api.renamePanel);
						view.on("childview:panel:delete", api.deletePanel);
						view.on("open:dashboard:list", api.openDashboardList);

						application.setContentView(view);
					});
			}
		};

		return {
			start: api.loadWidgetList
		};
	});