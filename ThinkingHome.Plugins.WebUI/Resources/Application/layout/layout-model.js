define(
	['lib'],
	function(lib) {
		
		var menuItemModel = lib.backbone.Model.extend({
			defaults: {
				title: 'undefined',
				active: false
			}
		});

		var menuItemCollection = lib.backbone.Collection.extend({
			model: menuItemModel,
			comparator: 'sortOrder'
		});

		return {
			MenuItemCollection: menuItemCollection
		};
	});