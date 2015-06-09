define(['app', 'common',
		'webapp/uniui/settings/widget-editor-model.js',
		'webapp/uniui/settings/widget-editor-view.js'
],
	function (application, common, models, views) {

		var api = {

			loadDashboardInfo: function(id) {

				models.loadDashboardInfo(id)
					.done(function(data) {

						var view = new views.DashboardInfoView({
							model: data.info,
							collection: data.widgets
						});

						api.view = view;
						application.setContentView(view);
					});
			}
		};

		return {
			start: api.loadDashboardInfo
		};
	});