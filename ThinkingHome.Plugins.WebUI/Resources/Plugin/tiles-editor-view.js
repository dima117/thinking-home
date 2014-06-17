define(
	[	'app',
		'tpl!webapp/webui/tiles-editor-layout.tpl',
		'tpl!webapp/webui/tiles-editor-form.tpl',
		'tpl!webapp/webui/tiles-editor-list-item.tpl'],
	function (application, layoutTemplate, formTemplate, itemTemplate) {

	application.module('WebUI.TilesEditor', function (module, app, backbone, marionette, $, _) {

		module.TilesEditorLayout = marionette.Layout.extend({
			template: layoutTemplate,
			regions: {
				regionForm: '#region-form',
				regionList: '#region-list'
			}
		});

		module.TilesEditorFormView = app.Common.FormView.extend({
			template: formTemplate,
			className: 'tiles-panel',
			events: {
				'click .js-add-tile': 'onBtnAddTileClick'
			},
			onBtnAddTileClick: function(e) {

				e.preventDefault();

				this.updateModel();
				this.trigger('webui:tiles-editor:add');
			}
		});
		
		module.TilesEditorListItemView = marionette.ItemView.extend({
			template: itemTemplate,
			tagName: 'a',
			className: 'tile btn-primary',
			onRender: function () {

				if (this.model.get('wide')) {
					this.$el.addClass('tile-double');
				}
			},
			triggers: {
				'click .js-delete-tile': 'webui:tiles-editor:delete'
			}
		});

		module.TilesEditorListView = marionette.CollectionView.extend({
			itemView: module.TilesEditorListItemView,
			className: 'tiles'
		});

	});

	return application.WebUI.TilesEditor;
});