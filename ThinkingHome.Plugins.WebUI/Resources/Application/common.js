define([
	'app',
	'json!api/webui/styles.json',
	'application/common/sortable-view',
	'application/common/complex-view',
	'application/common/form-view',
	'application/common/utils'],
	function (application, cssFiles) {

		application.module('Common', function (module, app, backbone, marionette, $, _) {

			module.utils.loadCss.apply(null, cssFiles);
			module.utils.displayCurrentTime('.js-cur-time');
		});

		return application.Common;
	});