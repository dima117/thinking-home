define(
	['lib'],
	function (lib) {

		var widgetCollection = lib.backbone.Collection.extend({
			comparator: "sortOrder"
		});

		var api = {
			loadDashboardInfo: function (id) {

				var defer = lib.$.Deferred();

				lib.$.getJSON('/api/uniui/dashboard/info', { id: id })
					.done(function (data) {

						var info = new lib.backbone.Model(data.info),
							widgets = new widgetCollection(data.widgets);

						defer.resolve({ info: info, widgets: widgets });
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		return {
			loadDashboardInfo: api.loadDashboardInfo
		};
	});