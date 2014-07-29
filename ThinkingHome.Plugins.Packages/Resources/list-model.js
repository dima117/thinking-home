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
			},
			
			installPackage: function (packageId) {
			
				var rq = $.post('/api/packages/install', { packageId: packageId });

				return rq.promise();
			},
			
			uninstallPackage: function (packageId) {
			
				var rq = $.post('/api/packages/uninstall', { packageId: packageId });

				return rq.promise();
			}
		};

		// requests
		app.reqres.setHandler('query:packages:all', api.loadPackages);
		app.reqres.setHandler('cmd:packages:install', api.installPackage);
		app.reqres.setHandler('cmd:packages:uninstall', api.uninstallPackage);
	});

	return application.Packages.List;
});