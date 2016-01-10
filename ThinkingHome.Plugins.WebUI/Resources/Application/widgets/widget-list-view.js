define([
	'lib',
	'text!application/settings/panel-list.tpl',
	'text!application/settings/widget-list-item.tpl',
	'text!application/settings/panel-list-item.tpl',
	'lang!application/lang.json'],
	function (lib, listTemplate, widgetTemplate, panelTemplate, lang) {

		var widgetView = lib.marionette.ItemView.extend({
			template: lib.handlebars.compile(widgetTemplate),
			triggers: { 'click .js-widget-edit': 'widget:edit' }
		});

		var panelView = lib.marionette.CompositeView.extend({
			template: lib.handlebars.compile(panelTemplate),
			childView: widgetView,
			childViewContainer: '.js-widget-list',
			ui: {
				toolbar: '.js-toolbar',
				typeSelector: '.js-widget-type'
			},
			triggers: {
				'click .js-widget-create': 'widget:create',
				'click .js-panel-rename': 'panel:rename',
				'click .js-panel-delete': 'panel:delete'
			},
			events: {
				'click .js-show-toolbar': function (e) {
					e.stopPropagation();
					e.preventDefault();

					if (!this._toolbarInited) {
						lib.utils.addListItems(this.ui.typeSelector, this.getOption('types'));
						this._toolbarInited = true;
					}

					this.ui.toolbar.addClass('th-panel-toolbar-active');
				},
				'click .js-hide-toolbar': function (e) {
					e.stopPropagation();
					e.preventDefault();

					this.ui.toolbar.removeClass('th-panel-toolbar-active');
				}
			},
			initialize: function() {
				this.on('childview:widget:edit', function (childView) {
					this.trigger('widget:edit', childView);
				});
			},
			templateHelpers: { lang: lang }
		});

		var listView = lib.marionette.CompositeView.extend({
			template: lib.handlebars.compile(listTemplate),
			childView: panelView,
			childViewContainer: '.js-list',
			childViewOptions: function (model, index) {
				return { collection: model.get('widgets'), types: this.model.get("types") };
			},
			triggers: {
				"click .js-dashboard-list": "open:dashboard:list",
				"click .js-create-panel": "panel:create"
			},
			templateHelpers: { lang: lang }
		});

		return {
			PanelListView: listView
		};
	});