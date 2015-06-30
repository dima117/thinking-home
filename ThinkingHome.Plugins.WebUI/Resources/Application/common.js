define([
	'app',
	'json!api/webui/styles.json',
	'application/common/complex-view',
	'application/common/utils'],
	function (application, cssFiles, complexView, utils) {

		var common = {
			ComplexView: complexView,
			utils: utils
		};

		utils.loadCss.apply(null, cssFiles);
		
		return common;
	});