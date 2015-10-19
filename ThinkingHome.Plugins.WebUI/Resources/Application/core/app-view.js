define(
	['lib', 'text!application/core/app-layout.tpl'],
	function (lib, layoutTemplate) {

		var createLinkHandler = function (route, hideMenu) {
			return function (e) {
				e.preventDefault();
				e.stopPropagation();

				hideMenu && this.ui.navbarCollapse.collapse('hide');

				this.trigger('navigate', route);
			}
		};

		var layoutView = lib.marionette.LayoutView.extend({
			el: 'body',
			template: lib.handlebars.compile(layoutTemplate),
			ui: {
				info: '.js-info',
				navbarCollapse: '.js-navbar-collapse'
			},
			regions: {
				content: ".js-content"
			},
			events: {
				"click .js-link-home": createLinkHandler(),
				"click .js-link-apps": createLinkHandler('apps', true),
				"click .js-link-settings": createLinkHandler('settings', true),
			},

			// api
			setInfoText: function (text) {
				this.ui.info.text(text);
			},

			setContentView: function (view) {
				this.getRegion('content').show(view);
			}
		});

		return {
			LayoutView: layoutView
		};
	});