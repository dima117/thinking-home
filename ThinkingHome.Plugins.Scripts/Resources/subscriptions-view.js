define(
	['lib', 'common',
		'text!webapp/scripts/subscriptions-layout.tpl',
		'text!webapp/scripts/subscriptions-form.tpl',
		'text!webapp/scripts/subscriptions-list.tpl',
		'text!webapp/scripts/subscriptions-list-item.tpl'],
	function (lib, common, layoutTemplate, formTemplate, listTemplate, itemTemplate) {

		var subscriptionLayout = lib.marionette.LayoutView.extend({
			template: lib._.template(layoutTemplate),
			regions: {
				regionForm: '#region-subscriptions-form',
				regionList: '#region-subscriptions-list'
			}
		});

		var subscriptionFormView = lib.marionette.ItemView.extend({
			template: lib._.template(formTemplate),
			ui: {
				eventList: '.js-event-list',
				scriptList: '.js-script-list'
			},
			onRender: function () {

				// add items
				var data = this.serializeData();
				common.utils.addListItems(this.ui.eventList, data.eventList);
				common.utils.addListItems(this.ui.scriptList, data.scriptList);

				// set selected values
				lib.syphon.deserialize(this, data);
			},
			events: {
				'click .js-btn-add-subscription': 'addSubscription'
			},
			addSubscription: function (e) {
				e.preventDefault();

				var data = lib.syphon.serialize(this);
				this.trigger('scripts:subscription:add', data);
			}
		});


		var subscriptionView = lib.marionette.ItemView.extend({
			template: lib._.template(itemTemplate),
			tagName: 'tr',
			triggers: {
				'click .js-delete-subscription': 'scripts:subscription:delete'
			}
		});

		var subscriptionListView = lib.marionette.CompositeView.extend({
			template: lib._.template(listTemplate),
			childView: subscriptionView,
			childViewContainer: 'tbody'
		});

		return {
			SubscriptionLayout: subscriptionLayout,
			SubscriptionFormView: subscriptionFormView,
			SubscriptionView: subscriptionView,
			SubscriptionListView: subscriptionListView
		};
	});