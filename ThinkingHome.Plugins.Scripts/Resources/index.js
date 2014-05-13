define(
	['app'],
	function (application) {

		var module = application.Common.SubApplication('Scripts.Index', 'list', {
			list: 'webapp/scripts/script-list',
			editor: 'webapp/scripts/script-editor'
		});
	
		return module;
	});