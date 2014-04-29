define(
	['app', 'webapp/packages/list-model', 'webapp/packages/list-view'],
	function (application) {

		application.module('Packages.List', function (module, app, backbone, marionette, $, _) {

			var mainView = new module.PackageListLayout();			

			var api = {
				
				install: function (itemView) {
					
					var packageId = itemView.model.get('id');
					var rq = app.request('update:packages:install', packageId);
					
					$.when(rq).done(api.reload);
				},

				uninstall: function (itemView) {
					
					var packageId = itemView.model.get('id');
					var rq = app.request('update:packages:uninstall', packageId);
					
					$.when(rq).done(api.reload);
				},

				reload: function() {

					var rq = app.request('load:packages:all');
					
					$.when(rq).done(function (items) {

						var listView = new module.PackageListView({ collection: items });

						listView.on('itemview:packages:install', api.install);
						listView.on('itemview:packages:uninstall', api.uninstall);

						mainView.regionList.show(listView);
					});
				}
			};

			module.createView = function () {

				api.reload();
				return mainView;
			};

		});

		return application.Packages.List;
	});