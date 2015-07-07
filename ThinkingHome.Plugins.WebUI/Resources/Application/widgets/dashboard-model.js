define(
	['lib'],
	function (lib) {

		var api = {

			loadDashboardDetails: function (id) {

				var defer = lib.$.Deferred();

				lib.$.getJSON('/api/uniui/dashboard/details', { id: id })
					.done(function (data) {

						if (data) {

							var dashboards = new lib.backbone.Collection(data.dashboards);
							var widgets = new lib.backbone.Collection(data.widgets);

							defer.resolve({
								dashboards: dashboards,
								widgets: widgets
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
			loadDashboardDetails: api.loadDashboardDetails
		};
	});