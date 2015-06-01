define(['app', 'common',
		'webapp/uniui/settings/dashboard-list-model.js',
		'webapp/uniui/settings/dashboard-list-view.js'
],
	function (application, common, models, views) {

		var api = {

			renameDashboard: function (childView) {

				var id = childView.model.get("id"),
					title = childView.model.get("title"),
					newTitle = window.prompt("Enter new title", title);

				if (newTitle) {

					models.renameDashboard(id, newTitle).done(api.loadDashboardList);
				}
			},
			deleteDashboard: function (childView) {

				var id = childView.model.get("id"),
					title = childView.model.get("title");

				if (common.utils.confirm('Do you want to delete the dashboard "{0}"?', title)) {

					models.deleteDashboard(id).done(api.loadDashboardList);
				}
			},

			loadDashboardList: function () {

				models.loadDashboardList()
					.done(function (list) {

						var view = new views.DashboardListView({
							collection: list
						});

						view.on("childview:dashboard:rename", api.renameDashboard);
						view.on("childview:dashboard:delete", api.deleteDashboard);

						application.setContentView(view);
					});
			}
		};

		return {
			start: api.loadDashboardList
		};
	});