define(
	['app', 'tpl!js/application/item.tpl'],

	function (application, itemTemplate) {

		application.module('Navigation', function (module, app, backbone, marionette, $, _) {
		
			module.NavItemView = marionette.ItemView.extend({
				template: itemTemplate,
				tagName: 'li',
				events: {
					'click a': 'itemClicked'
				},
				itemClicked: function (e) {
					e.preventDefault();
					e.stopPropagation();

					var path = this.model.get('path');
					app.trigger("page:open", path);
				}
			});

			module.NavMenuView = marionette.CollectionView.extend({
				tagName: 'ul',
				className: 'nav navbar-nav',
				itemView: module.NavItemView
			});

		});

		return application.Navigation;
	});