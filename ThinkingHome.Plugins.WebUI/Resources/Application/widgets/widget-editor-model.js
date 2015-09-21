define(
	['lib'],
	function (lib) {

		var api = {

			createWidget: function (panelId, type) {

				var defer = lib.$.Deferred();

				lib.$.getJSON('/api/uniui/widget/create', { type: type, panel: panelId })
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
			},

			saveWidget: function (data) {

				var rq = lib.$.post('/api/uniui/widget/save', data);

				return rq.promise();
			},

			deleteWidget: function (id) {

				var rq = lib.$.post('/api/uniui/widget/delete', { id: id });

				return rq.promise();
			}
		};

		return {
			createWidget: api.createWidget,
			editWidget: api.editWidget,
			saveWidget: api.saveWidget,
			deleteWidget: api.deleteWidget
		};
	});