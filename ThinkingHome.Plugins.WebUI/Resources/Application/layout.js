define(['lib'], function(lib) {

	var rootLayout = lib.marionette.LayoutView.extend({
		el: 'body'
	});

	return rootLayout;
});