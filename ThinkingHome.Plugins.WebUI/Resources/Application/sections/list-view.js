define(
	['lib',
		'text!application/sections/list.tpl',
		'text!application/sections/list-item.tpl'],
	function (lib, listTemplate, itemTemplate) {

		var sectionView = lib.marionette.ItemView.extend({
			template: lib._.template(itemTemplate),
			triggers: {
				'click .js-btn-add-tile': 'sections:add-tile',
				'click .js-section-link': 'sections:navigate'
			}
		});

		var sectionListView = lib.marionette.CompositeView.extend({
			template: lib._.template(listTemplate),
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