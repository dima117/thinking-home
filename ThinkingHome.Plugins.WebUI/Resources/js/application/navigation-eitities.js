define(['app'], function(application) {

	application.module('Navigation', function(module, app, backbone, marionette, $, _) {

		module.NavItem = backbone.Model.extend({
			urlRoot: 'api/webui/items',
			defaults: { 
				sortOrder: 0
			}
		});

		module.NavItemCollection = backbone.Collection.extend({
			url: 'api/webui/items',
			model: module.NavItem,
			comparator: 'sortOrder'
		});
	});
	
	return application.Navigation;
});