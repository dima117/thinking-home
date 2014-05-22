define(
	['app',
		'webapp/scripts/event-list-model',
		'webapp/scripts/event-list-view'],
	function (application) {

		application.module('Scripts.EventList', function (module, app, backbone, marionette, $, _) {

			var api = {

				reload: function () {

					
				
					/*
					var layoutView = new module.EventsLayout();
					app.setContentView(layoutView);
					*/
					//app.setContentView(view);

					//var rqHandlers = app.request('load:scripts:handlers');
					var rqFormData = app.request('load:scripts:subscription-form');

					$.when(rqFormData).done(function (formData) {
						
						var view = new module.SubscriptionFormView({ model: formData });
						app.setContentView(view);
					//	layoutView.regionList.show(view);
					});
				}
			};

			module.start = function () {
				api.reload();
			};

		});

		return application.Scripts.EventList;
	});