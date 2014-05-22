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
				template: formTemplate
			});


			module.SubscriptionView = marionette.ItemView.extend({
				template: itemTemplate,
				tagName: 'tr'
			});

			module.SubscriptionListView = marionette.CompositeView.extend({
				template: listTemplate,
				itemView: module.SubscriptionView,
				itemViewContainer: 'tbody'
			});
		});

		return application.Scripts.Subscriptions;
	});