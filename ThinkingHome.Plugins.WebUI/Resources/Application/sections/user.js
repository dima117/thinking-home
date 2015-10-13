define(
	['application/sections/list'],
	function (sectionList) {

		return sectionList.extend({
			requestName: 'loadCommonSections',
			pageTitle: 'Common pages'
		});
	});