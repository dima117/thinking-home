define(
	[
		'marionette',
		'backbone',
		'underscore',
		'jquery',
		'json2',
		'syphon',
		'chart',
		'chart.scatter'
	],
	function (marionette, backbone, underscore, jquery, json2, syphon, chartjs) {

		return {
			marionette: marionette,
			backbone: backbone,
			_: underscore,
			$: jquery,
			json2: json2,
			Chart: chartjs,
			syphon: syphon
		};
	});