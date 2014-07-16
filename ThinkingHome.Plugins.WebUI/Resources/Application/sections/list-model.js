define(['app'], function (application) {

	application.module('WebUI.Sections', function (module, app, backbone, marionette, $, _) {

		// entities
		module.Page = backbone.Model.extend({
			defaults: {
				sortOrder: 0
			}
		});

		module.PageCollection = backbone.Collection.extend({
			model: module.Page,
			comparator: 'sortOrder'
		});

		// api
		var api = {

			loadCommonSections: function () {

				var defer = $.Deferred();

				$.getJSON('/api/webui/sections/common')
					.done(function (items) {
						var collection = new module.PageCollection(items);
						defer.resolve(collection);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			},

			loadSystemSections: function () {

				var defer = $.Deferred();

				$.getJSON('/api/webui/sections/system')
					.done(function (items) {
						var collection = new module.PageCollection(items);
						defer.resolve(collection);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		// requests
		app.reqres.setHandler('load:sections:common', api.loadCommonSections);
		app.reqres.setHandler('load:sections:system', api.loadSystemSections);
	});

	return application.WebUI.Sections;
});