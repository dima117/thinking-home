define(['app'], function (application) {

	application.module('Packages.List', function (module, app, backbone, marionette, $, _) {

		// entities
		module.Package = backbone.Model.extend({
			urlRoot: 'api/packages/list'
		});

		module.PackageCollection = backbone.Collection.extend({
			url: 'api/packages/list',
			model: module.Package,
			comparator: 'id'
		});

		// api
		var api = {

			loadPackages: function () {

				var defer = $.Deferred();

				var packages = new module.PackageCollection();

				packages.fetch({
					success: function (list) {
						defer.resolve(list);
					},
					error: function () {
						defer.resolve(undefined);
					}
				});

				return defer.promise();
			}
		};

		// requests
		app.reqres.setHandler('load:packages:all', function () {
			return api.loadPackages();
		});
	});

	return application.Packages.List;
});