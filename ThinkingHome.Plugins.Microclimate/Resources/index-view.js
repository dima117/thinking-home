define(
	['app', 'marionette', 'backbone', 'underscore',
	 'text!webapp/microclimate/list-template.tpl',	// шаблон для списка объектов
	 'text!webapp/microclimate/item-template.tpl'	// шаблон для элемента списка
	],
	function (application, marionette, backbone, _, tmplList, tmplListItem) {

		var sensorView = marionette.ItemView.extend({
			template: _.template(tmplListItem),
			className: 'mc-sensor-panel btn btn-default',
			triggers: {
				'click': 'show:sensor:details'
			}
		});

		var sensorListView = marionette.CompositeView.extend({
			template: _.template(tmplList),
			childView: sensorView,
			childViewContainer: '.js-list'
		});

		return {
			SensorList: sensorListView
		};
	});