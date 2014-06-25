define(
	[	'app', 'common',
		'text!webapp/webui/tiles-editor-layout.tpl',
		'text!webapp/webui/tiles-editor-form.tpl',
		'text!webapp/webui/tiles-editor-list-item.tpl'],
	function (application, commonModule, layoutTemplate, formTemplate, itemTemplate) {

	application.module('WebUI.TilesEditor', function (module, app, backbone, marionette, $, _) {

		module.TilesEditorLayout = marionette.Layout.extend({
			template: _.template(layoutTemplate),
			regions: {
				regionForm: '#region-form',
				regionList: '#region-list'
			}
		});

		module.TilesEditorFormView = commonModule.FormView.extend({
			template: _.template(formTemplate),
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
			template: _.template(itemTemplate),
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