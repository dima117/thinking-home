define(
	['app',
		'tpl!webapp/scripts/subscriptions-layout.tpl',
		'tpl!webapp/scripts/subscriptions-form.tpl',
		'tpl!webapp/scripts/subscriptions-list.tpl',
		'tpl!webapp/scripts/subscriptions-list-item.tpl'],
	function (application, layoutTemplate, formTemplate, listTemplate, itemTemplate) {

		application.module('Scripts.Subscriptions', function (module, app, backbone, marionette, $, _) {

			module.SubscriptionLayout = marionette.Layout.extend({
				template: layoutTemplate,
				regions: {
					regionForm: '#region-subscriptions-form',
					regionList: '#region-subscriptions-list'
				}
			});

			module.SubscriptionFormView = app.Common.FormView.extend({
				template: formTemplate,
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
				template: itemTemplate,
				tagName: 'tr',
				events: {
					'click .js-delete-subscription': 'deleteSubscription'
				},
				deleteSubscription: function (e) {
					
					e.preventDefault();
					this.trigger('scripts:subscription:delete');
				}
			});

			module.SubscriptionListView = marionette.CompositeView.extend({
				template: listTemplate,
				itemView: module.SubscriptionView,
				itemViewContainer: 'tbody'
			});
		});

		return application.Scripts.Subscriptions;
	});