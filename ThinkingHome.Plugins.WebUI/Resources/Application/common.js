define([
	'json!api/webui/styles.json',
	'application/utils'],
	function (cssFiles, utils) {

		var common = {
			utils: utils
		};

		utils.loadCss.apply(null, cssFiles);
		
		return common;
	});