define(
	['app', 'webapp/packages/list-model', 'webapp/packages/list-view'],
	function (application, models) {

		application.module('Packages.List', function (module, app, backbone, marionette, $, _) {

			var api = {

				install: function (childView) {

					var packageId = childView.model.get('id');
					models.installPackage(packageId).done(api.reload);
				},

				uninstall: function (childView) {

					var packageId = childView.model.get('id');
					models.uninstallPackage(packageId).done(api.reload);
				},

				reload: function () {

					models.loadPackages()
						.done(function (items) {

							var view = new module.PackageListView({ collection: items });

							view.on('childview:packages:install', api.install);
							view.on('childview:packages:uninstall', api.uninstall);

							app.setContentView(view);
						});
				}
			};

			module.start = function () {
				api.reload();
			};

		});

		return application.Packages.List;
	});