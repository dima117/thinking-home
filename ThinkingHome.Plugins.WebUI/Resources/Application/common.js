define([
	'app',
	'json!api/webui/styles.json',
	'application/common/complex-view',
	'application/common/sortable-view',
	'application/common/form-view',
	'application/common/utils'],
	function (application, cssFiles, complexView, sortableView, formView, utils) {

		var commonModule = {
			ComplexView: complexView
		};

		application.Common.ComplexView = complexView;

		application.module('Common', function (module, app, backbone, marionette, $, _) {

			module.utils.loadCss.apply(null, cssFiles);
			module.utils.displayCurrentTime('.js-cur-time');
		});

		return application.Common;
	});