define(['lib',
	'text!webapp/scripts/script-list.tpl',
	'text!webapp/scripts/script-list-item.tpl',
	'lang!webapp/scripts/lang.json'],
	function (lib, listTemplate, itemTemplate, lang) {

		var scriptView = lib.marionette.ItemView.extend({
			template: lib.handlebars.compile(itemTemplate),
			triggers: {
				'click .js-btn-run': 'scripts:run',
				'click .js-btn-edit': 'scripts:edit',
				'click .js-btn-delete': 'scripts:delete'
			},
			templateHelpers: { lang: lang }
		});

		var scriptListView = lib.marionette.CompositeView.extend({
			template: lib.handlebars.compile(listTemplate),
			childView: scriptView,
			childViewContainer: '.js-list',
			triggers: { 'click .js-add-script': 'scripts:add' },
			templateHelpers: { lang: lang }
		});

		return {
			ScriptView: scriptView,
			ScriptListView: scriptListView
		};
	});