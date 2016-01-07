define(
	['lib',
		'text!webapp/scripts/subscriptions-layout.tpl',
		'text!webapp/scripts/subscriptions-form.tpl',
		'text!webapp/scripts/subscriptions-list.tpl',
		'text!webapp/scripts/subscriptions-list-item.tpl',
		'lang!webapp/scripts/lang.json'],
	function (lib, layoutTemplate, formTemplate, listTemplate, itemTemplate, lang) {

		var subscriptionLayout = lib.marionette.LayoutView.extend({
			template: lib.handlebars.compile(layoutTemplate),
			regions: {
				regionForm: '#region-subscriptions-form',
				regionList: '#region-subscriptions-list'
			},
			templateHelpers: { lang: lang }
		});

		var subscriptionFormView = lib.marionette.ItemView.extend({
			template: lib.handlebars.compile(formTemplate),
			ui: {
				eventList: '.js-event-list',
				scriptList: '.js-script-list'
			},
			onRender: function () {

				// add items
				var data = this.serializeData();
				lib.utils.addListItems(this.ui.eventList, data.eventList);
				lib.utils.addListItems(this.ui.scriptList, data.scriptList);

				// set selected values
				lib.syphon.deserialize(this, data);
			},
			events: { 'click .js-btn-add-subscription': 'addSubscription' },
			addSubscription: function (e) {
				e.preventDefault();

				var data = lib.syphon.serialize(this);
				this.trigger('scripts:subscription:add', data);
			},
			templateHelpers: { lang: lang }
		});

		var subscriptionView = lib.marionette.ItemView.extend({
			template: lib.handlebars.compile(itemTemplate),
			tagName: 'tr',
			triggers: { 'click .js-delete-subscription': 'scripts:subscription:delete' },
			templateHelpers: { lang: lang }
		});

		var subscriptionListView = lib.marionette.CompositeView.extend({
			template: lib.handlebars.compile(listTemplate),
			childView: subscriptionView,
			childViewContainer: 'tbody',
			templateHelpers: { lang: lang }
		});

		return {
			SubscriptionLayout: subscriptionLayout,
			SubscriptionFormView: subscriptionFormView,
			SubscriptionView: subscriptionView,
			SubscriptionListView: subscriptionListView
		};
	});