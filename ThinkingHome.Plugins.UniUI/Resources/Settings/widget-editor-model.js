define(
	['lib'],
	function (lib) {

		var api = {

			createWidget: function (dashboardId, type) {

				var defer = lib.$.Deferred();

				lib.$.getJSON('/api/uniui/widget/create', { type: type, dashboard: dashboardId })
					.done(function (data) {

						var info = new lib.backbone.Model(data.info),
							fields = new lib.backbone.Collection(data.fields);

						defer.resolve({ info: info, fields: fields});
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			},

			editWidget: function (id) {

				var defer = lib.$.Deferred();

				lib.$.getJSON('/api/uniui/widget/edit', { id: id })
					.done(function (data) {

						var info = new lib.backbone.Model(data.info),
							fields = new lib.backbone.Collection(data.fields);

						defer.resolve({ info: info, fields: fields });
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		return {
			createWidget: api.createWidget,
			editWidget: api.editWidget
		};
	});