define(
	['app',
		'webapp/scripts/subscriptions-model',
		'webapp/scripts/subscriptions-view'],
	function (application) {

		application.module('Scripts.Subscriptions', function (module, app, backbone, marionette, $, _) {

			var api = {

				reload: function () {

					var layoutView = new module.SubscriptionsLayout();
					app.setContentView(layoutView);
					
					//var rqHandlers = app.request('load:scripts:handlers');
					var rqFormData = app.request('load:scripts:subscription-form');

					$.when(rqFormData).done(function (formData) {
						
						var formView = new module.SubscriptionsFormView({ model: formData });
						layoutView.regionForm.show(formView);
					});
				}
			};

			module.start = function () {
				api.reload();
			};

		});

		return application.Scripts.Subscriptions;
	});