define(
	['lib'],
	function (lib) {

		var dashboardListItem = lib.backbone.Model.extend({
			defaults: {
				active: false
			}
		});

		var dashboardList = lib.backbone.Collection.extend({
			model: dashboardListItem,
			comparator: 'sortOrder'
		});

		var api = {
			loadDashboardList: function () {

				var defer = lib.$.Deferred();

				lib.$.getJSON('/api/uniui/dashboard/list')
					.done(function (data) {

						var list = new dashboardList(data);

						defer.resolve(list);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			},

			loadDashboardDetails: function (id) {

				var defer = lib.$.Deferred();

				lib.$.getJSON('/api/uniui/dashboard/details', { id: id })
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