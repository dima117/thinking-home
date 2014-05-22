define(['app'], function (application) {

	application.module('Scripts.EventList', function (module, app, backbone, marionette, $, _) {

		// entities
		module.FormData = backbone.Model.extend();
		
		module.EventHandler = backbone.Model.extend();

		module.EventHandlerCollection = backbone.Collection.extend({
			model: module.EventHandler
		});

		// api
		var api = {

			loadHandlers: function () {

				var defer = $.Deferred();

				$.getJSON('/api/scripts/subscription/list')
					.done(function (items) {
						var collection = new module.EventHandlerCollection(items);
						defer.resolve(collection);
					})
					.fail(function () {
						defer.resolve(undefined);
					});

				return defer.promise();
			},
			loadFormData: function () {

				var defer = $.Deferred();

				$.getJSON('/api/scripts/subscription/form')
					.done(function (data) {
						var model = new module.FormData(data);
						defer.resolve(model);
					})
					.fail(function () {
						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		// requests
		app.reqres.setHandler('load:scripts:handlers', function () {
			return api.loadHandlers();
		});
		
		app.reqres.setHandler('load:scripts:subscription-form', function () {
			return api.loadFormData();
		});
	});

	return application.Scripts.EventList;
});