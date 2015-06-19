define(['lib'], function(lib) {

	var rootLayout = lib.marionette.LayoutView.extend({
		el: 'body',
		regions: {
			content: ".js-region-content"
		}
	});


	return rootLayout;
});