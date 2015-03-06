define(['lib'], function (lib) {

	// entities
	var pageModel = lib.backbone.Model.extend({
		defaults: {
			sortOrder: 0
		}
	});

	var pageCollection = lib.backbone.Collection.extend({
		model: pageModel,
		comparator: 'sortOrder'
	});

	// api
	var api = {

		loadCommonSections: function () {

			var defer = lib.$.Deferred();

			lib.$.getJSON('/api/webui/sections/common')
				.done(function (items) {

					var collection = new pageCollection(items);
					defer.resolve(collection);
				})
				.fail(function () {

					defer.resolve(undefined);
				});

			return defer.promise();
		},

		loadSystemSections: function () {

			var defer = lib.$.Deferred();

			lib.$.getJSON('/api/webui/sections/system')
				.done(function (items) {

					var collection = new pageCollection(items);
					defer.resolve(collection);
				})
				.fail(function () {

					defer.resolve(undefined);
				});

			return defer.promise();
		}
	};

	return {

		// entities
		Page: pageModel,
		PageCollection: pageCollection,

		// requests
		loadCommonSections: api.loadCommonSections,
		loadSystemSections: api.loadSystemSections
	};
});