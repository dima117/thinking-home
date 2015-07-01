define(['app', 'lib',
		'application/settings/dashboard-list-model.js',
		'application/settings/dashboard-list-view.js'
],
	function (application, lib, models, views) {

		var api = {

			openDashboard: function (childView) {
				
				var id = childView.model.get("id");
				application.navigate('application/settings/widget-list', id);
			},

			createDashboard: function () {
				
				var title = window.prompt('Enter dashboard title');

				if (title) {

					models.createDashboard(title).done(api.loadDashboardList);
				}
			},

			deleteDashboard: function (childView) {

				var id = childView.model.get('id'),
					title = childView.model.get('title');

				if (lib.utils.confirm('Do you want to delete the dashboard "{0}"?', title)) {

					models.deleteDashboard(id).done(api.loadDashboardList);
				}
			},

			loadDashboardList: function () {

				models.loadDashboardList()
					.done(function (list) {

						var view = new views.DashboardListView({
							collection: list
						});

						view.on('dashboard:create', api.createDashboard);
						view.on('childview:dashboard:open', api.openDashboard);
						view.on('childview:dashboard:delete', api.deleteDashboard);

						application.setContentView(view);
					});
			}
		};

		return {
			start: api.loadDashboardList
		};
	});