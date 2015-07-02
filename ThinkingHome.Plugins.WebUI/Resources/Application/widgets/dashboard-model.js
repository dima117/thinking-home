define(
	['lib'],
	function (lib) {

		var api = {
			loadDashboardList: function() {
				
				var defer = lib.$.Deferred();

				lib.$.getJSON('/api/uniui/dashboard/list')
					.done(function (data) {

						var list = new lib.backbone.Collection(data);

						defer.resolve(list);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		return {
			loadDashboardList: api.loadDashboardList
		};
	});