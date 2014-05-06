define(
	['app', 'webapp/scripts/script-list-model', 'webapp/scripts/script-list-view'],
	function (application) {

		application.module('Scripts.List', function (module, app, backbone, marionette, $, _) {

			var mainView = new module.ScriptListLayout();

			var api = {
				
				reload: function() {

					var rq = app.request('load:scripts:list');
					
					$.when(rq).done(function (items) {

						var listView = new module.ScriptListView({ collection: items });

						//listView.on('itemview:packages:install', api.install);
						//listView.on('itemview:packages:uninstall', api.uninstall);

						mainView.regionList.show(listView);
					});
				}
			};

			module.createView = function () {

				api.reload();
				return mainView;
			};

		});

		return application.Scripts.List;
	});