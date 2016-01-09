define(
	['application/sections/list', 'lang!application/lang.json'],
	function (sectionList, lang) {

		return sectionList.extend({
			requestName: 'loadSystemSections',
			pageTitle: lang.get('Settings')
		});
	});