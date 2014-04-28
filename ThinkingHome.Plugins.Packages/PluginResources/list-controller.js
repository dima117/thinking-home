define(
	['app', 'webapp/packages/list-model', 'webapp/packages/list-view'],
	function (application) {

		application.module('Packages.List', function (module, app, backbone, marionette, $, _) {

			module.createView = function () {

				var defer = $.Deferred();

				var rq = app.request('load:packages:all');
				$.when(rq).done(function (items) {

					var view = new module.PackageListView({ collection: items });

					view.on('itemview:packages:install', function (itemView) {

						var packageId = itemView.model.get('id');
						var rqInstall = app.request('update:packages:install', packageId);
						$.when(rqInstall).done(function (obj) {
							console.log(obj);
						});
					});

					view.on('itemview:packages:uninstall', function (itemView) {
						
						var packageId = itemView.model.get('id');
						var rqUninstall = app.request('update:packages:uninstall', packageId);
						$.when(rqUninstall).done(function (obj) {
							console.log(obj);
						});
					});

					defer.resolve(view);
				});

				return defer.promise();
			};
		});

		return application.Packages.List;
	});