define(
	['app', 'webapp/packages/list-model', 'webapp/packages/list-view'],
	function (application) {

		application.module('Packages.List', function (module, app, backbone, marionette, $, _) {

			var api = {
				
				install: function (childView) {
					
					var packageId = childView.model.get('id');
					var rq = app.request('cmd:packages:install', packageId);
					
					$.when(rq).done(api.reload);
				},

				uninstall: function (childView) {
					
					var packageId = childView.model.get('id');
					var rq = app.request('cmd:packages:uninstall', packageId);
					
					$.when(rq).done(api.reload);
				},

				reload: function() {

					var rq = app.request('query:packages:all');
					
					$.when(rq).done(function (items) {

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