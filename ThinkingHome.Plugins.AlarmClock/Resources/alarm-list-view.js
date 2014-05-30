define(
	['app', 'tpl!webapp/alarm-clock/list.tpl', 'tpl!webapp/alarm-clock/list-item.tpl'],
	function (application, listTemplate, itemTemplate) {

		application.module('AlarmClock.List', function (module, app, backbone, marionette, $, _) {

			module.AlarmView = marionette.ItemView.extend({
				template: itemTemplate,
				onRender: function () {

					var enabled = this.model.get('enabled');

					this.$el.toggleClass('bg-info', enabled);
					this.$('.js-btn-enable').toggleClass('hidden', enabled);
					this.$('.js-btn-disable').toggleClass('hidden', !enabled);
				},
				events: {
					'click .js-btn-enable': 'btnEnableClick',
					'click .js-btn-disable': 'btnDisableClick'
				},
				btnEnableClick: function (e) {
					e.preventDefault();
					this.trigger('alarm-clock:set-state', true);
				},
				btnDisableClick: function (e) {
					e.preventDefault();
					this.trigger('alarm-clock:set-state', false);
				}
			});

			module.AlarmListView = marionette.CompositeView.extend({
				template: listTemplate,
				itemView: module.AlarmView,
				itemViewContainer: '.js-list'
			});
		});

		return application.AlarmClock.List;
	});