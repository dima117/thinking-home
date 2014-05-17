define(
	['app', 'tpl!webapp/webui/section-list.tpl', 'tpl!webapp/webui/section-list-item.tpl'],
	function (application, listTemplate, itemTemplate) {

		application.module('WebUI.Sections', function (module, app, backbone, marionette, $, _) {

			module.SectionView = marionette.ItemView.extend({
				template: itemTemplate,
				events: {
					'click .js-section-link': 'sectionLinkClicked'
				},
				sectionLinkClicked: function(e) {
					e.preventDefault();
					e.stopPropagation();

					var path = this.model.get('path');
					app.trigger("page:load", path);
				}
			});

			module.SectionListView = marionette.CompositeView.extend({
				template: listTemplate,
				itemView: module.SectionView,
				itemViewContainer: '.js-list',
				
				onRender: function() {

					if (this.options.title) {
						this.$('.js-title').text(this.options.title);
					}
				}
			});
		});

		return application.WebUI.Sections;
	});