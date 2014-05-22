define(
	['app',
		'tpl!webapp/scripts/subscriptions-layout.tpl',
		'tpl!webapp/scripts/subscriptions-form.tpl',
		'tpl!webapp/scripts/subscriptions-list.tpl',
		'tpl!webapp/scripts/subscriptions-list-item.tpl'],
	function (application, layoutTemplate, formTemplate, listTemplate, itemTemplate) {

		application.module('Scripts.Subscriptions', function (module, app, backbone, marionette, $, _) {

			module.SubscriptionsLayout = marionette.Layout.extend({
				template: layoutTemplate,
				regions: {
					regionForm: '#region-subscriptions-form',
					regionList: '#region-subscriptions-list'
				}
			});

			module.SubscriptionsFormView = app.Common.FormView.extend({
				template: formTemplate
			});


			module.EventHandlerView = marionette.ItemView.extend({
				template: itemTemplate,
				tagName: 'tr'
			});

			module.EventHandlerListView = marionette.CompositeView.extend({
				template: listTemplate,
				itemView: module.EventHandlerView,
				itemViewContainer: 'tbody'
			});
		});

		return application.Scripts.Subscriptions;
	});