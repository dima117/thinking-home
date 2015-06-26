define(['lib', 'common'], function (lib, common) {

	var rootLayout = lib.marionette.LayoutView.extend({
		el: 'body',
		onRender: function () {

			var timePanel = this.$('.js-cur-time');
			common.utils.displayCurrentTime(timePanel);
		},
		regions: {
			content: ".js-region-content"
		},
		events: {
			"click .js-nav-link": "hideMenu"
		},
		hideMenu: function() {

			this.$(".js-navbar-collapse").collapse('hide');
		},
		setContentView: function (view) {

			this.getRegion('content').show(view);
		}
	});

	return rootLayout;
});