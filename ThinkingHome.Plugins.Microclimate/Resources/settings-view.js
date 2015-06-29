define([
	'lib',
	'text!webapp/microclimate/settings.tpl',
	'text!webapp/microclimate/settings-row.tpl'],
	function (lib, tmplSettings, tmplSettingsRow) {

		var sensorTableRowView = lib.marionette.ItemView.extend({
			template: lib._.template(tmplSettingsRow),
			tagName: 'tr',
			triggers: {
				'click .js-delete-sensor': 'delete:sensor',
				'click .js-btn-details': 'show:sensor:details'
			}
		});

		var sensorTableView = lib.marionette.CompositeView.extend({
			template: lib._.template(tmplSettings),
			childView: sensorTableRowView,
			childViewContainer: 'tbody',
			ui: {
				displayName: '#tb-display-name',
				channel: '#select-channel',
				showHumidity: '#cb-show-humidity',
			},
			triggers: {
				'click .js-add-sensor': 'add:sensor'
			}
		});

		return {
			SensorTable: sensorTableView
		};
	});