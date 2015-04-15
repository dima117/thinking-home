define([
	'app',
	'marionette',
	'backbone',
	'underscore',
	'text!webapp/microclimate/settings.tpl'
],
	function (application, marionette, backbone, _, tmplSettings) {

		var sensorTableRowView = marionette.ItemView.extend({
			template: _.template(
				'<td><%= displayName %></td>' +
				'<td class="col-md-1"><%= channel %></td>' +
				'<td class="col-md-1"><%= showHumidity %></td>' +
				'<td class="col-md-1"><a class="js-btn-add-tile" href="#">add tile <i class="fa fa-external-link-square th-no-text-decoration"></i></a></td>' +
				'<td class="col-md-1"><a class="js-delete-sensor" href="#">delete</a></td>'),
			tagName: 'tr',
			triggers: {
				'click .js-delete-sensor': 'delete:sensor',
				'click .js-btn-add-tile': 'add:sensor:tile'
			}
		});

		var sensorTableView = marionette.CompositeView.extend({
			template: _.template(tmplSettings),
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