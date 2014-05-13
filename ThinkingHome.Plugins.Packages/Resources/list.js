define(
	['app', 'webapp/packages/list-model', 'webapp/packages/list-view'],
	function (application) {

		application.module('Packages.List', function (module, app, backbone, marionette, $, _) {

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

						var view = new module.PackageListView({ collection: items });

						view.on('itemview:packages:install', api.install);
						view.on('itemview:packages:uninstall', api.uninstall);

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