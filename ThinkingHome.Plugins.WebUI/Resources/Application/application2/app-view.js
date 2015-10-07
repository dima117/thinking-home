define(
	['lib', 'text!application2/app-layout.tpl'],
	function (lib, layoutTemplate) {

		/* 
			layout
			- content
			- доп меню
			- клики по общим ссылкам
			- панель времени
		*/
		var createLinkHandler = function (name, hideMenu) {
			return function (e) {
				e.preventDefault();
				e.stopPropagation();

				hideMenu && this.ui.navbarCollapse.collapse('hide');

				this.trigger(name);
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
				"click .js-link-home": createLinkHandler('navigate:home'),
				"click .js-link-apps": createLinkHandler('navigate:apps', true),
				"click .js-link-settings": createLinkHandler('navigate:settings', true),
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