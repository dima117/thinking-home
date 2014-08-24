define(
	['app', 'common',
		'text!webapp/scripts/subscriptions-layout.tpl',
		'text!webapp/scripts/subscriptions-form.tpl',
		'text!webapp/scripts/subscriptions-list.tpl',
		'text!webapp/scripts/subscriptions-list-item.tpl'],
	function (application, commonModule, layoutTemplate, formTemplate, listTemplate, itemTemplate) {

		application.module('Scripts.Subscriptions', function (module, app, backbone, marionette, $, _) {

			module.SubscriptionLayout = marionette.LayoutView.extend({
				template: _.template(layoutTemplate),
				regions: {
					regionForm: '#region-subscriptions-form',
					regionList: '#region-subscriptions-list'
				}
			});

			module.SubscriptionFormView = commonModule.FormView.extend({
				template: _.template(formTemplate),
				events: {
					'click .js-btn-add-subscription': 'addSubscription'
				},
				addSubscription: function (e) {
					e.preventDefault();

					this.updateModel();
					this.trigger('scripts:subscription:add');
				}
			});


			module.SubscriptionView = marionette.ItemView.extend({
				template: _.template(itemTemplate),
				tagName: 'tr',
				triggers: {
					'click .js-delete-subscription': 'scripts:subscription:delete'
				}
			});

			module.SubscriptionListView = marionette.CompositeView.extend({
				template: _.template(listTemplate),
				childView: module.SubscriptionView,
				childViewContainer: 'tbody'
			});
		});

		return application.Scripts.Subscriptions;
	});