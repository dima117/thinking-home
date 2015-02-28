define(['lib'], function (lib) {

	// entities
	var formData = lib.backbone.Model.extend();

	var subscription = lib.backbone.Model.extend();

	var subscriptionCollection = lib.backbone.Collection.extend({
		model: subscription
	});

	// api
	var api = {

		loadSubscriptions: function () {

			var defer = lib.$.Deferred();

			lib.$.getJSON('/api/scripts/subscription/list')
				.done(function (items) {

					var collection = new subscriptionCollection(items);
					defer.resolve(collection);
				})
				.fail(function () {

					defer.resolve(undefined);
				});

			return defer.promise();
		},

		addSubscription: function (eventAlias, scriptId) {

			var rq = lib.$.post('/api/scripts/subscription/add', {
				eventAlias: eventAlias,
				scriptId: scriptId
			});

			return rq.promise();
		},

		deleteSubscription: function (subscriptionId) {

			var rq = lib.$.post('/api/scripts/subscription/delete', {
				subscriptionId: subscriptionId
			});

			return rq.promise();
		},

		loadFormData: function () {

			var defer = lib.$.Deferred();

			lib.$.getJSON('/api/scripts/subscription/form')
				.done(function (data) {

					var model = new formData(data);
					defer.resolve(model);
				})
				.fail(function () {

					defer.resolve(undefined);
				});

			return defer.promise();
		}
	};

	return {

		// entities
		FormData: formData,
		Subscription: subscription,
		SubscriptionCollection: subscriptionCollection,

		// requests
		loadSubscriptions: api.loadSubscriptions,
		loadFormData: api.loadFormData,
		addSubscription: api.addSubscription,
		deleteSubscription: api.deleteSubscription
	};
});