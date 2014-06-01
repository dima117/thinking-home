define(
	[
		'app',
		'tpl!webapp/alarm-clock/list.tpl',
		'tpl!webapp/alarm-clock/list-item.tpl',
		'tpl!webapp/alarm-clock/editor.tpl'],
	function (application, listTemplate, itemTemplate, editorTemplate) {

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
					'click .js-btn-disable': 'btnDisableClick',
					'click .js-btn-edit': 'btnEditClick',
					'click .js-btn-delete': 'btnDeleteClick'
				},
				btnEditClick: function (e) {
					e.preventDefault();
					this.trigger('alarm-clock:edit');
				},
				btnEnableClick: function (e) {
					e.preventDefault();
					this.trigger('alarm-clock:set-state', true);
				},
				btnDisableClick: function (e) {
					e.preventDefault();
					this.trigger('alarm-clock:set-state', false);
				},
				btnDeleteClick: function (e) {
					e.preventDefault();
					this.trigger('alarm-clock:delete');
				}
			});

			module.AlarmListView = marionette.CompositeView.extend({
				template: listTemplate,
				itemView: module.AlarmView,
				itemViewContainer: '.js-list',
				events: {
					'click .js-btn-stop': 'btnStopClick',
					'click .js-btn-add': 'btnAddClick'
				}, btnStopClick: function (e) {
					e.preventDefault();
					this.trigger('alarm-clock:stop');
				}, btnAddClick: function (e) {
					e.preventDefault();
					this.trigger('alarm-clock:add');
				}
			});
			
			module.AlarmEditorView = app.Common.FormView.extend({
				template: editorTemplate,
				events: {
					'click .js-btn-save': 'btnSaveClick',
					'click .js-btn-cancel': 'btnCancelClick'
				},
				btnSaveClick: function (e) {
					e.preventDefault();

					this.updateModel();
					this.trigger('alarm-clock:editor:save');
				},
				btnCancelClick: function (e) {
					e.preventDefault();
					this.trigger('alarm-clock:editor:cancel');
				}
			});
		});

		return application.AlarmClock.List;
	});