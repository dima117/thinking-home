define(['lib',
	'webapp/scripts/script-editor-model',
	'webapp/scripts/script-editor-view',
	'lang!webapp/scripts/lang.json'],
	function (lib, models, views, lang) {

		var scriptEditor = lib.common.AppSection.extend({
			start: function (scriptId) {
				if (scriptId) {
					this.edit(scriptId);
				} else {
					this.add();
				}
			},

			redirectToList: function () {
				this.application.navigate('webapp/scripts/script-list');
			},

			save: function (view, data) {
				view.model.set(data);
				models.saveScript(view.model).done(this.bind('redirectToList'));
			},

			createEditor: function (model) {
				var view = new views.ScriptEditorView({ model: model });

				this.listenTo(view, 'scripts:editor:cancel', this.bind('redirectToList'));
				this.listenTo(view, 'scripts:editor:save', this.bind('save', view));

				this.application.setContentView(view);
			},

			edit: function (scriptId) {
				models.loadScript(scriptId).done(this.bind('createEditor'));
			},

			add: function () {
				var name = window.prompt(lang.get('Enter_script_name'), '');
				var model = new models.ScriptData({ name: name || lang.get('noname') });
				this.createEditor(model);
			}
		});

		return scriptEditor;
	});