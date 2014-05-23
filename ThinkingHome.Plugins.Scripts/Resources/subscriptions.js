define(
	['app',
		'webapp/scripts/subscriptions-model',
		'webapp/scripts/subscriptions-view'],
	function (application) {

		application.module('Scripts.Subscriptions', function (module, app, backbone, marionette, $, _) {

			var layoutView;

			var api = {

				addSubscription: function () {

					var eventAlias = this.model.get('selectedEventAlias');
					var scriptId = this.model.get('selectedScriptId');

					app.request('update:scripts:subscription-add', eventAlias, scriptId)
						.done(api.reloadList);
				},

				deleteSubscription: function (itemView) {

					var model = itemView.model.toJSON();
					var message = _.template('Delete subscription?\n- event: <%= eventAlias %>\n- script: <%= scriptName %>', model);

					if (window.confirm(message)) {
						
						var subscriptionId = itemView.model.get('id');
						app.request('update:scripts:subscription-delete', subscriptionId)
							.done(api.reloadList);
					}
				},

				reloadList: function () {

					app.request('load:scripts:subscription-list')
						.done(function (list) {

							var view = new module.SubscriptionListView({ collection: list });
							view.on('itemview:scripts:subscription:delete', api.deleteSubscription);
							layoutView.regionList.show(view);
						});
				},

				reloadForm: function () {

					app.request('load:scripts:subscription-form')
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