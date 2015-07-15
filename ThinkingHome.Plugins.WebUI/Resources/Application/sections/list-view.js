define(
	['lib',
		'text!application/sections/list.tpl',
		'text!application/sections/list-item.tpl'],
	function (lib, listTemplate, itemTemplate) {

		var sectionView = lib.marionette.ItemView.extend({
			template: lib.handlebars.compile(itemTemplate),
			triggers: {
				'click .js-section-link': 'sections:navigate'
			}
		});

		var sectionListView = lib.marionette.CompositeView.extend({
			template: lib.handlebars.compile(listTemplate),
			childView: sectionView,
			childViewContainer: '.js-list',

			onRender: function () {

				if (this.options.title) {
					this.$('.js-title').text(this.options.title);
				}
			}
		});

		return {
			SectionView: sectionView,
			SectionListView: sectionListView
		};
	});