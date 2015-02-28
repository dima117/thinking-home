define(['lib'], function (lib) {

	// entities
	var packageModel = lib.backbone.Model.extend();

	var packageCollection = lib.backbone.Collection.extend({
		model: packageModel,
		comparator: 'id'
	});

	// api
	var api = {

		loadPackages: function () {

			var defer = lib.$.Deferred();

			lib.$.getJSON('/api/packages/list')
				.done(function (items) {
					var collection = new packageCollection(items);
					defer.resolve(collection);
				})
				.fail(function () {

					defer.resolve(undefined);
				});

			return defer.promise();
		},

		installPackage: function (packageId) {

			var rq = lib.$.post('/api/packages/install', { packageId: packageId });

			return rq.promise();
		},

		uninstallPackage: function (packageId) {

			var rq = lib.$.post('/api/packages/uninstall', { packageId: packageId });

			return rq.promise();
		}
	};

	return {
		// entities
		Package: packageModel,
		PackageCollection: packageCollection,

		// requests
		loadPackages: api.loadPackages,
		installPackage: api.installPackage,
		uninstallPackage: api.uninstallPackage
	};
});