define(
	['lib',
		'webapp/scripts/subscriptions-model',
		'webapp/scripts/subscriptions-view'],
	function (lib, models, views) {

		var subscriptionList = lib.common.AppSection.extend({
			start: function () {
				// init layout
				var layout = this.layout = new views.SubscriptionLayout();
				this.application.setContentView(layout);

				this.reloadForm();
				this.reloadList();
			},

			addSubscription: function (data) {
				models.addSubscription(data.selectedEventAlias, data.selectedScriptId)
					.done(this.bind('reloadList'));
			},
				
			deleteSubscription: function (childView) {
				var eventAlias = childView.model.get('eventAlias');
				var scriptName = childView.model.get('scriptName');

				if (lib.utils.confirm('Delete the subscription?\n- event: "{0}"\n- script: "{1}"', eventAlias, scriptName)) {
					var subscriptionId = childView.model.get('id');
					models.deleteSubscription(subscriptionId).done(this.bind('reloadList'));
				}
			},

			displayList: function (list) {
				var view = new views.SubscriptionListView({ collection: list });
				this.listenTo(view, 'childview:scripts:subscription:delete', this.bind('deleteSubscription'));
				this.layout.regionList.show(view);
			},

			reloadList: function () {
				models.loadSubscriptions().done(this.bind('displayList'));
			},

			displayForm: function (formData) {
				var form = new views.SubscriptionFormView({ model: formData });
				this.listenTo(form, 'scripts:subscription:add', this.bind('addSubscription'))
				this.layout.regionForm.show(form);
			},

			reloadForm: function () {
				models.loadFormData().done(this.bind('displayForm'));
			}
		});

		return subscriptionList;
	});