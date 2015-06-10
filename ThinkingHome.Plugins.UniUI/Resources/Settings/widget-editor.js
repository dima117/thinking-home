define(['app', 'common',
		'webapp/uniui/settings/widget-editor-model.js',
		'webapp/uniui/settings/widget-editor-view.js'
],
	function (application, common, models, views) {

		var api = {

			openEditor: function(id) {

				models.editWidget(id)
					.done(function(data) {

						var view = new views.WidgetEditorView({
							model: data.info,
							collection: data.fields
						});

						api.view = view;
						application.setContentView(view);
					});
			}
		};

		return {
			start: api.openEditor
		};
	});