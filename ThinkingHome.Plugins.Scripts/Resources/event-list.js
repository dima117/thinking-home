define(
	['app',
		'webapp/scripts/event-list-model',
		'webapp/scripts/event-list-view'],
	function (application) {

		application.module('Scripts.EventList', function (module, app, backbone, marionette, $, _) {

			var api = {

				reload: function () {

					var layoutView = new module.EventsLayout();
					app.setContentView(layoutView);

					var rq = app.request('load:scripts:handlers');

					$.when(rq).done(function (handlers) {

						var view = new module.EventHandlerListView({ collection: handlers });

						layoutView.regionList.show(view);
					});
				}
			};

			module.start = function () {
				api.reload();
			};

		});

		return application.Scripts.EventList;
	});