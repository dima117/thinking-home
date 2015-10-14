define(
	['lib', 'application/sections/list-model', 'application/sections/list-view'],
	function (lib, models, views) {

		var sectionList = lib.common.AppSection.extend({
			start: function () {
				// todo: переписать выбор метода для загрузки списка
				models[this.requestName]().done(this.bind('displayList'));
			},

			displayList: function (items) {
				var view = new views.SectionListView({ collection: items, title: this.pageTitle });
				this.listenTo(view, 'childview:sections:navigate', this.bind('onSectionSelect'))
				this.application.setContentView(view);
			},

			onSectionSelect: function (childView) {
				var path = childView.model.get('path');
				this.application.navigate(path);
			}
		});

		return sectionList;
	});