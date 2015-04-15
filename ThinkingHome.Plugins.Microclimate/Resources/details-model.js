define(
	['app', 'marionette', 'backbone', 'underscore'],
	function (application, marionette, backbone, _) {

		var api = {
			loadDetails: function (id) {

				var defer = $.Deferred();

				$.getJSON('/api/microclimate/sensors/details', { id: id })
					.done(function (data) {

						var model = new backbone.Model(data);
						defer.resolve(model);
					})
					.fail(function() {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		// requests
		application.reqres.setHandler('query:microclimate:details', api.loadDetails);

		return api;
	});