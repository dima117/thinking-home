define(['lib',
		'application/settings/dashboard-list-model.js',
		'application/settings/dashboard-list-view.js'
],
	function (lib, models, views) {

		var dashboardList = lib.common.AppSection.extend({
			start: function () {
				this.loadDashboardList();
			},

			openDashboard: function (childView) {
				var id = childView.model.get("id");
				this.application.navigate('application/settings/widget-list', id);
			},

			createDashboard: function () {
				var title = window.prompt('Enter dashboard title');

				if (title) {
					models.createDashboard(title).done(this.bindFnContext('loadDashboardList'));
				}
			},

			deleteDashboard: function (childView) {
				var id = childView.model.get('id'),
					title = childView.model.get('title');

				if (lib.utils.confirm('Do you want to delete the "{0}" dashboard?', title)) {
					models.deleteDashboard(id).done(this.bindFnContext('loadDashboardList'));
				}
			},

			loadDashboardList: function () {
				models.loadDashboardList().done(this.bindFnContext('displayDashboardList'));
			},

			displayDashboardList: function (list) {

				var view = new views.DashboardListView({
					collection: list
				});

				this.listenTo(view, 'dashboard:create', this.bindFnContext('createDashboard'))
				this.listenTo(view, 'childview:dashboard:open', this.bindFnContext('openDashboard'))
				this.listenTo(view, 'childview:dashboard:delete', this.bindFnContext('deleteDashboard'))

				this.application.setContentView(view);
			}
		});

		return dashboardList;
	});