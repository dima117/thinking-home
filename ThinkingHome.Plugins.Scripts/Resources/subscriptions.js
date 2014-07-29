define(
	['app', 'common',
		'webapp/scripts/subscriptions-model',
		'webapp/scripts/subscriptions-view'],
	function (application, commonModule) {

		application.module('Scripts.Subscriptions', function (module, app, backbone, marionette, $, _) {

			var layoutView;

			var api = {

				addSubscription: function () {

					var eventAlias = this.model.get('selectedEventAlias');
					var scriptId = this.model.get('selectedScriptId');

					app.request('cmd:scripts:subscription-add', eventAlias, scriptId)
						.done(api.reloadList);
				},

				deleteSubscription: function (itemView) {

					var eventAlias = itemView.model.get('eventAlias');
					var scriptName = itemView.model.get('scriptName');

					if (commonModule.utils.confirm('Delete the subscription?\n- event: "{0}"\n- script: "{1}"', eventAlias, scriptName)) {

						var subscriptionId = itemView.model.get('id');
						app.request('cmd:scripts:subscription-delete', subscriptionId)
							.done(api.reloadList);
					}
				},

				reloadList: function () {

					app.request('query:scripts:subscription-list')
						.done(function (list) {

							var view = new module.SubscriptionListView({ collection: list });
							view.on('itemview:scripts:subscription:delete', api.deleteSubscription);
							layoutView.regionList.show(view);
						});
				},

				reloadForm: function () {

					app.request('query:scripts:subscription-form')
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