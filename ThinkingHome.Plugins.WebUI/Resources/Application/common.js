define([
	'app',
	'json!api/webui/styles.json',
	'application/common/utils'],
	function (application, cssFiles, utils) {

		var common = {
			utils: utils
		};

		utils.loadCss.apply(null, cssFiles);
		
		return common;
	});