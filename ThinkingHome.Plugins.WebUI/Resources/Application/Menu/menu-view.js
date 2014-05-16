define(
	['app', 'tpl!application/menu/menu-item.tpl'],

	function (application, itemTemplate) {

		application.module('Menu', function (module, app, backbone, marionette, $, _) {
		
			module.MenuItemView = marionette.ItemView.extend({
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

			module.CommonMenuView = marionette.CollectionView.extend({
				tagName: 'ul',
				className: 'nav navbar-nav',
				itemView: module.MenuItemView
			});
			
			module.SystemMenuView = marionette.CollectionView.extend({
				tagName: 'ul',
				className: 'nav navbar-nav',
				itemView: module.MenuItemView
			});

		});

		return application.Menu;
	});