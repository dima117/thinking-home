define(
	[	'app', 'marionette', 'backbone', 'underscore',
		'webapp/microclimate/details-chart',			// модуль для рисования графиков температуры/влажности
		'text!webapp/microclimate/details-template.tpl'	// шаблон для страницы подробной информации
	],
	function (application, marionette, backbone, _, detailsСhart, tmplDetails) {

		var sensorDetailsView = marionette.ItemView.extend({
			template: _.template(tmplDetails),
			triggers: {
				'click .js-show-list': 'show:sensor:list'
			},
			onShow: function () {
				detailsСhart.build(this);
			}
		});

		return {
			SensorDetails: sensorDetailsView
		};
	});