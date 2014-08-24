define(
	['app', 'text!webapp/scripts/script-list.tpl', 'text!webapp/scripts/script-list-item.tpl'],
	function (application, listTemplate, itemTemplate) {

		application.module('Scripts.List', function (module, app, backbone, marionette, $, _) {

			module.ScriptView = marionette.ItemView.extend({
				template: _.template(itemTemplate),
				triggers: {
					'click .js-btn-run': 'scripts:run',
					'click .js-btn-edit': 'scripts:edit',
					'click .js-btn-delete': 'scripts:delete',
					'click .js-btn-add-tile': 'scripts:add-tile'
				}
			});

			module.ScriptListView = marionette.CompositeView.extend({
				template: _.template(listTemplate),
				childView: module.ScriptView,
				childViewContainer: '.js-list',
				triggers: {
					'click .js-add-script': 'scripts:add'
				}
			});
		});

		return application.Scripts.List;
	});