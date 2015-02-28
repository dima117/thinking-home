define(
	['app', 'common',
		'webapp/scripts/subscriptions-model',
		'webapp/scripts/subscriptions-view'],
	function (application, commonModule, models) {

		application.module('Scripts.Subscriptions', function (module, app, backbone, marionette, $, _) {

			var layoutView;

			var api = {

				addSubscription: function () {

					var eventAlias = this.model.get('selectedEventAlias');
					var scriptId = this.model.get('selectedScriptId');

					models.addSubscription(eventAlias, scriptId).done(api.reloadList);
				},

				deleteSubscription: function (childView) {

					var eventAlias = childView.model.get('eventAlias');
					var scriptName = childView.model.get('scriptName');

					if (commonModule.utils.confirm('Delete the subscription?\n- event: "{0}"\n- script: "{1}"', eventAlias, scriptName)) {

						var subscriptionId = childView.model.get('id');
						models.deleteSubscription(subscriptionId).done(api.reloadList);
					}
				},

				reloadList: function () {

					models.loadSubscriptions()
						.done(function (list) {

							var view = new module.SubscriptionListView({ collection: list });
							view.on('childview:scripts:subscription:delete', api.deleteSubscription);
							layoutView.regionList.show(view);
						});
				},

				reloadForm: function () {

					models.loadFormData()
						.done(function (formData) {

							var form = new module.SubscriptionFormView({ model: formData });
							form.on('scripts:subscription:add', api.addSubscription);
							layoutView.regionForm.show(form);
						});
				}
			};

			module.start = function () {

				// init layout
				layoutView = new module.SubscriptionLayout();
				app.setContentView(layoutView);

				api.reloadForm();
				api.reloadList();
			};

		});

		return application.Scripts.Subscriptions;
	});