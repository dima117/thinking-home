define(
	[	'app', 
		'text!application/sections/list.tpl',
		'text!application/sections/list-item.tpl'],
	function (application, listTemplate, itemTemplate) {

		application.module('WebUI.Sections', function (module, app, backbone, marionette, $, _) {

			module.SectionView = marionette.ItemView.extend({
				template: _.template(itemTemplate),
				triggers: {
					'click .js-btn-add-tile': 'sections:add-tile'
				},
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
				template: _.template(listTemplate),
				childView: module.SectionView,
				childViewContainer: '.js-list',
				
				onRender: function() {

					if (this.options.title) {
						this.$('.js-title').text(this.options.title);
					}
				}
			});
		});

		return application.WebUI.Sections;
	});