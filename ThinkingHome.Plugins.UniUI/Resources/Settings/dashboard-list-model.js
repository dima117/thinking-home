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

			addDashboard: function (tite) {

				var rq = lib.$.post('/api/uniui/dashboard/add', {tite: tite});

				return rq.promise();
			},

			renameDashboard: function (id, title) {

				var rq = lib.$.post('/api/uniui/dashboard/rename', { id: id, title: title });

				return rq.promise();
			},

			moveDashboard: function (id, up) {

				var rq = lib.$.post('/api/uniui/dashboard/move', { id: id, up: up });

				return rq.promise();
			},

			deleteDashboard: function (id) {

				var rq = lib.$.post('/api/uniui/dashboard/delete', { id: id });
				return rq.promise();
			}
		};

		return {
			loadDashboardList: api.loadDashboardList,
			addDashboard: api.addDashboard,
			renameDashboard: api.renameDashboard,
			moveDashboard: api.moveDashboard,
			deleteDashboard: api.deleteDashboard
		};
	});