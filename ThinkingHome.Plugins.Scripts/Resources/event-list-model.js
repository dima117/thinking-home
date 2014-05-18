define(['app'], function (application) {

	application.module('Scripts.EventList', function (module, app, backbone, marionette, $, _) {

		// entities
		module.Event = backbone.Model.extend();

		module.EventCollection = backbone.Collection.extend({
			model: module.Event
		});
		
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
			loadEvents: function () {

				var defer = $.Deferred();

				$.getJSON('/api/scripts/events')
					.done(function (items) {
						var collection = new module.EventCollection(items);
						defer.resolve(collection);
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
		
		app.reqres.setHandler('load:scripts:events', function () {
			return api.loadEvents();
		});
	});

	return application.Scripts.EventList;
});