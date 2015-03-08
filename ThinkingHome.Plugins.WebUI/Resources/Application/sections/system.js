define(
	['application/sections/list'],
	function (sections) {

		return {
			start: function () {
				sections.api.reload('loadSystemSections', 'System pages');
			}
		};
	});