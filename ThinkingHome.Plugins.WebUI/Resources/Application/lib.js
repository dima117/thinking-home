define(
	['marionette', 'backbone', 'underscore', 'jquery', 'json2', 'chart'],
	function (marionette, backbone, underscore, jquery, json2, chartjs) {

		return {
			marionette: marionette,
			backbone: backbone,
			_: underscore,
			$: jquery,
			json2: json2,
			Chart: chartjs
		};
	});