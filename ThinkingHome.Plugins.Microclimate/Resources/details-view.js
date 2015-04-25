define(
	[
		'lib',
		'webapp/microclimate/details-chart',			// модуль для рисования графиков температуры/влажности
		'text!webapp/microclimate/details-template.tpl'	// шаблон для страницы подробной информации
	],
	function (lib, detailsСhart, tmplDetails) {

		var sensorDetailsView = lib.marionette.ItemView.extend({
			template: lib._.template(tmplDetails),
			ui: {
				tPanel: ".js-temperature-panel",
				hPanel: ".js-humidity-panel",
				tChart: ".js-temperature-chart",
				hChart: ".js-humidity-chart"
			},
			triggers: {
				'click .js-show-list': 'show:sensor:list'
			},
			onShow: function () {

				var showHumidity = this.model.get('showHumidity'),
					tClass = showHumidity ? "col-md-6" : "col-md-12",
					hClass = showHumidity ? "col-md-6" : "hidden";

				this.ui.tPanel.addClass(tClass);
				this.ui.hPanel.addClass(hClass);

				detailsСhart.build(this, showHumidity);
			}
		});

		return {
			SensorDetails: sensorDetailsView
		};
	});