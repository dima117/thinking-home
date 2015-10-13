define(
	['application/sections/list'],
	function (sectionList) {

		return sectionList.extend({
			requestName: 'loadSystemSections',
			pageTitle: 'System pages'
		});
	});