define(['app'], function (application) {

	application.module('Scripts.Subscriptions', function (module, app, backbone, marionette, $, _) {

		// entities
		module.FormData = backbone.Model.extend();
		
		module.Subscription = backbone.Model.extend();

		module.SubscriptionCollection = backbone.Collection.extend({
			model: module.Subscription
		});

		// api
		var api = {

			loadSubscriptions: function () {

				var defer = $.Deferred();

				$.getJSON('/api/scripts/subscription/list')
					.done(function (items) {
						var collection = new module.SubscriptionCollection(items);
						defer.resolve(collection);
					})
					.fail(function () {
						defer.resolve(undefined);
					});

				return defer.promise();
			},
			
			addSubscription: function (eventAlias, scriptId) {

				var rq = $.post('/api/scripts/subscription/add', {
					eventAlias: eventAlias,
					scriptId: scriptId
				});

				return rq.promise();
			},
			
			deleteSubscription: function (subscriptionId) {

				var rq = $.post('/api/scripts/subscription/delete', {
					subscriptionId: subscriptionId
				});

				return rq.promise();
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
		app.reqres.setHandler('query:scripts:subscription-list', api.loadSubscriptions);
		app.reqres.setHandler('query:scripts:subscription-form', api.loadFormData);
		app.reqres.setHandler('cmd:scripts:subscription-add', api.addSubscription);
		app.reqres.setHandler('cmd:scripts:subscription-delete', api.deleteSubscription);
	});

	return application.Scripts.Subscriptions;
});