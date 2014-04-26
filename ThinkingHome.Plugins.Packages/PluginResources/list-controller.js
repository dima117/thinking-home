define(
	['app', 'webapp/packages/list-model', 'webapp/packages/list-view'],
	function (application) {

		application.module('Packages.List', function (module, app, backbone, marionette, $, _) {

			module.createView = function () {

				var defer = $.Deferred();

				var rq = app.request('load:packages:all');
				$.when(rq).done(function (items) {
						
					var view = new module.PackageListView({ collection: items });
					defer.resolve(view);
				});

				return defer.promise();
			};
		});

		return application.Packages.List;
	});