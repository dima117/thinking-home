define(
	[
		'app',
		'tpl!webapp/alarm-clock/list.tpl',
		'tpl!webapp/alarm-clock/list-item.tpl'],
	function (application, listTemplate, itemTemplate) {

		application.module('AlarmClock.List', function (module, app, backbone, marionette, $, _) {

			module.AlarmView = marionette.ItemView.extend({
				template: itemTemplate,
				onRender: function () {

					var enabled = this.model.get('enabled');
					var scriptId = this.model.get('scriptId');

					this.$el.toggleClass('bg-info', enabled);
					this.$('.js-btn-enable').toggleClass('hidden', enabled);
					this.$('.js-btn-disable').toggleClass('hidden', !enabled);
					
					this.$('.js-play-sound').toggleClass('hidden', !!scriptId);
					this.$('.js-run-script').toggleClass('hidden', !scriptId);
					

				},
				triggers: {
					'click .js-btn-enable': 'alarm-clock:enable',
					'click .js-btn-disable': 'alarm-clock:disable',
					'click .js-btn-edit': 'alarm-clock:edit',
					'click .js-btn-delete': 'alarm-clock:delete'
				}
			});

			module.AlarmListView = marionette.CompositeView.extend({
				template: listTemplate,
				itemView: module.AlarmView,
				itemViewContainer: '.js-list',
				triggers: {
					'click .js-btn-stop': 'alarm-clock:stop',
					'click .js-btn-add': 'alarm-clock:add'
				}
			});
		});

		return application.AlarmClock.List;
	});