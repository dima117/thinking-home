define(
	['lib'],
	function (lib) {

		var dashboardCollection = lib.backbone.Collection.extend({
			comparator: "sortOrder"
		});

		var api = {
			loadDashboardList: function () {

				var defer = lib.$.Deferred();

				lib.$.getJSON('/api/uniui/dashboard/list')
					.done(function (items) {

						var collection = new dashboardCollection(items);
						defer.resolve(collection);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			},

			createDashboard: function (title) {

				var rq = lib.$.post('/api/uniui/dashboard/create', { title: title });

				return rq.promise();
			},

			deleteDashboard: function (id) {

				var rq = lib.$.post('/api/uniui/dashboard/delete', { id: id });
				return rq.promise();
			}
		};

		return {
			loadDashboardList: api.loadDashboardList,
			createDashboard: api.createDashboard,
			deleteDashboard: api.deleteDashboard
		};
	});