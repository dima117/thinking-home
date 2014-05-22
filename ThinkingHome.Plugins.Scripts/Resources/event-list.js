define(
	['app',
		'webapp/scripts/event-list-model',
		'webapp/scripts/event-list-view'],
	function (application) {

		application.module('Scripts.EventList', function (module, app, backbone, marionette, $, _) {

			var api = {

				reload: function () {

					var model = new backbone.Model({
						'event-handlers': [
							{ id: '1', name: 'хрюката' },
							{ id: '2', name: 'парасек' },
							{ id: '3', name: 'свиняжка' }
						],
						'eeeeeeee-options': [
							{ id: '4', name: 'хрюката - 2' },
							{ id: '5', name: 'парасек - 2' },
							{ id: '6', name: 'свиняжка - 2' }
						],
					});
					
					var view = new module.XxxView({ model: model });
				
					/*
					var layoutView = new module.EventsLayout();
					app.setContentView(layoutView);
					*/
					app.setContentView(view);

					//var rqHandlers = app.request('load:scripts:handlers');
					//var rqEvents = app.request('load:scripts:events');

					//$.when(rqHandlers, rqEvents).done(function (handlers) {

					//	var view = new module.EventHandlerListView({ collection: handlers });

					//	layoutView.regionList.show(view);
					//});
				}
			};

			module.start = function () {
				api.reload();
			};

		});

		return application.Scripts.EventList;
	});