define(
	['app', 'tpl!webapp/scripts/script-list.tpl', 'tpl!webapp/scripts/script-list-item.tpl'],
	function (application, listTemplate, itemTemplate) {

		application.module('Scripts.List', function (module, app, backbone, marionette, $, _) {

			module.ScriptView = marionette.ItemView.extend({
				template: itemTemplate,
				events: {
					'click .js-btn-run': 'btnRunClick',
					'click .js-btn-edit': 'btnEditClick',
					'click .js-btn-delete': 'btnDeleteClick'
				},
				btnRunClick: function (e) {
					e.preventDefault();
					this.trigger('scripts:run');
				},
				btnEditClick: function (e) {
					e.preventDefault();
					this.trigger('scripts:edit');
				},
				btnDeleteClick: function (e) {
					e.preventDefault();
					this.trigger('scripts:delete');
				}
			});

			module.ScriptListView = marionette.CompositeView.extend({
				template: listTemplate,
				itemView: module.ScriptView,
				itemViewContainer: '.js-list',
				events: {
					'click .js-add-script': 'btnAddClick'
				},
				btnAddClick: function (e) {
					e.preventDefault();
					this.trigger('scripts:add');
				},
			});
		});

		return application.Scripts.List;
	});