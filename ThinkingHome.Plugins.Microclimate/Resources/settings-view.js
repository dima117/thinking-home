define([
	'lib',
	'text!webapp/microclimate/settings.tpl',
	'text!webapp/microclimate/settings-row.tpl',
	'lang!webapp/microclimate/lang.json'],
	function (lib, tmplSettings, tmplSettingsRow, lang) {

		var sensorTableRowView = lib.marionette.ItemView.extend({
			template: lib.handlebars.compile(tmplSettingsRow),
			tagName: 'tr',
			triggers: {
				'click .js-delete-sensor': 'delete:sensor',
				'click .js-btn-details': 'show:sensor:details'
			},
			templateHelpers: { lang: lang }
		});

		var sensorTableView = lib.marionette.CompositeView.extend({
			template: lib.handlebars.compile(tmplSettings),
			childView: sensorTableRowView,
			childViewContainer: 'tbody',
			ui: {
				displayName: '#tb-display-name',
				channel: '#select-channel',
				showHumidity: '#cb-show-humidity',
			},
			triggers: {
				'click .js-add-sensor': 'add:sensor'
			},
			templateHelpers: { lang: lang }
		});

		return {
			SensorTable: sensorTableView
		};
	});