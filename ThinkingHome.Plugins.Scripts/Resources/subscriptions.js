define(
	['app',
		'webapp/scripts/subscriptions-model',
		'webapp/scripts/subscriptions-view'],
	function (application) {

		application.module('Scripts.Subscriptions', function (module, app, backbone, marionette, $, _) {

			var api = {

				reload: function () {

					var layoutView = new module.SubscriptionLayout();
					app.setContentView(layoutView);
					
					var rqFormData = app.request('load:scripts:subscription-form');
					var rqList = app.request('load:scripts:subscription-list');

					$.when(rqFormData, rqList).done(function (formData, list) {
						
						var formView = new module.SubscriptionFormView({ model: formData });
						layoutView.regionForm.show(formView);
						
						var listView = new module.SubscriptionListView({ collection: list });
						layoutView.regionList.show(listView);
					});
				}
			};

			module.start = function () {
				api.reload();
			};

		});

		return application.Scripts.Subscriptions;
	});