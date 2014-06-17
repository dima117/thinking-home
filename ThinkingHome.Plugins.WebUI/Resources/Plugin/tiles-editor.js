define(['app', 'webapp/webui/tiles-editor-model', 'webapp/webui/tiles-editor-view'], function (application) {

	application.module('WebUI.TilesEditor', function (module, app, backbone, marionette, $, _) {

		var layoutView;

		var api = {

			addTile: function () {

				var key = this.model.get('selectedKey');

				app.request('update:tiles:editor-add', key).done(api.reloadList);
			},

			deleteTile: function (itemView) {

				var title = itemView.model.get('title');

				if (app.Common.utils.confirm('Delete the tile "{0}"?', title)) {

					var id = itemView.model.get('id');
					app.request('update:tiles:editor-delete', id).done(api.reloadList);
				}
			},

			reloadForm: function () {

				app.request('load:tiles:editor-form')
					.done(function (formData) {

						var form = new module.TilesEditorFormView({ model: formData });
						form.on('webui:tiles-editor:add', api.addTile);
						layoutView.regionForm.show(form);
					});
			},
			
			reloadList: function () {

				app.request('load:tiles:editor-list')
					.done(function (list) {

						var view = new module.TilesEditorListView({ collection: list });
						view.on('itemview:webui:tiles-editor:delete', api.deleteTile);
						layoutView.regionList.show(view);
					});
			}
		};

		module.start = function () {

			// init layout
			layoutView = new module.TilesEditorLayout();
			app.setContentView(layoutView);

			api.reloadForm();
			api.reloadList();
		};
	});

	return application.WebUI.TilesEditor;
});