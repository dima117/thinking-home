define(
	[
		'lib',
		'text!webapp/alarm-clock/list.tpl',
		'text!webapp/alarm-clock/list-item.tpl'],
	function (lib, listTemplate, itemTemplate) {

		var alarmView = lib.marionette.ItemView.extend({
			template: lib._.template(itemTemplate),
			onRender: function () {

				var enabled = this.model.get('enabled');
				var scriptId = this.model.get('scriptId');

				this.$('.js-btn-enable').toggleClass('hidden', enabled).stateSwitcher();
				this.$('.js-btn-disable').toggleClass('hidden', !enabled).stateSwitcher();

				this.$('.js-play-sound').toggleClass('hidden', !!scriptId);
				this.$('.js-run-script').toggleClass('hidden', !scriptId);


			},
			triggers: {
				'click .js-btn-enable': 'alarm-clock:enable',
				'click .js-btn-disable': 'alarm-clock:disable',
				'click .js-btn-edit': 'alarm-clock:edit'
			}
		});

		var alarmListView = lib.marionette.CompositeView.extend({
			template: lib._.template(listTemplate),
			childView: alarmView,
			childViewContainer: '.js-list',
			triggers: {
				'click .js-btn-stop': 'alarm-clock:stop',
				'click .js-btn-add': 'alarm-clock:add'
			}
		});


		return {
			AlarmView: alarmView,
			AlarmListView: alarmListView
		};
	});