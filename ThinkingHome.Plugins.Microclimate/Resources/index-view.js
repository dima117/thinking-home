define(
	['lib',
	 'text!webapp/microclimate/list-template.tpl',	// шаблон для списка объектов
	 'text!webapp/microclimate/item-template.tpl'	// шаблон для элемента списка
	],
	function (lib, tmplList, tmplListItem) {

		var sensorView = lib.marionette.ItemView.extend({
			template: lib._.template(tmplListItem),
			className: 'mc-sensor-panel btn btn-default',
			triggers: {
				'click': 'show:sensor:details'
			}
		});

		var sensorListView = lib.marionette.CompositeView.extend({
			template: lib._.template(tmplList),
			childView: sensorView,
			childViewContainer: '.js-list'
		});

		return {
			SensorList: sensorListView
		};
	});