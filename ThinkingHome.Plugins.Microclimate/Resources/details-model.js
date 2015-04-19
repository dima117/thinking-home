define(['lib'],
	function (lib) {

		var api = {
			loadDetails: function (id) {

				var defer = lib.$.Deferred();

				lib.$.getJSON('/api/microclimate/sensors/details', { id: id })
					.done(function (data) {

						var model = new lib.backbone.Model(data);
						defer.resolve(model);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		return api;
	});