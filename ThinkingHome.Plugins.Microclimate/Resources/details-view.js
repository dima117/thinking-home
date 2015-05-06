define(
	['lib', 'text!webapp/microclimate/details-template.tpl'],
	function (lib, tmplDetails) {

		var sensorDetailsView = lib.marionette.ItemView.extend({

			// #region charts

			buildChartData: function () {

				var tdataset = [],
					hdataset = [],
					data = this.model.get('data');

				if (data && data.length) {
					lib._.each(data, function (el) {

						var timestamp = new Date(el.d).getTime();
						tdataset.push({ x: timestamp, y: el.t });
						hdataset.push({ x: timestamp, y: el.h });
					});

					tdataset = lib._.sortBy(tdataset, function (el) { return el.x; });
					hdataset = lib._.sortBy(hdataset, function (el) { return el.x; });
				}

				return { tdataset: tdataset, hdataset: hdataset };
			},

			chartOptions: {
				scaleType: "date",
				scaleFontFamily: "'Segoe UI','HelveticaNeue-Light',sans-serif",
				scaleFontColor: "#bbb",
				scaleGridLineColor: "rgba(255,255,255,.1)",
				scaleLineColor: "rgba(255,255,255,.2)",
				tooltipCornerRadius: 3,
				tooltipFillColor: "rgba(255,255,255,0.85)",
				tooltipFontColor: "#294C69",
				tooltipFontFamily: "'Segoe UI','HelveticaNeue-Light',sans-serif",
				responsive: true
			},

			initChart: function (el, title, labelFormat, color, points) {

				var options = lib._.extend(this.chartOptions, { scaleLabel: labelFormat }),
					ctxt = el.get(0).getContext("2d"),
					chart = new lib.Chart(ctxt).Scatter([
					{
						label: title,
						strokeColor: color,
						pointStrokeColor: "#294C69",
						data: points
					}], options);

				var legend = chart.generateLegend();
				lib.$(legend).insertAfter(el);

				return chart;
			},

			// #endregion

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

				// init charts
				var data = this.buildChartData();

				this.tchart = this.initChart(
					this.ui.tChart, 'Temperature', '<%=value%>°C', '#689fcd', data.tdataset);

				if (showHumidity) {

					this.hchart = this.initChart(
						this.ui.hChart, 'Humidity', '<%=value%>%', '#f66', data.hdataset);
				}
			},

			onDestroy: function () {
				
				if (this.tchart) {

					this.tchart.destroy();
					this.tchart = undefined;
				}

				if (this.hchart) {

					this.hchart.destroy();
					this.hchart = undefined;
				}
			}
		});

		return {
			SensorDetails: sensorDetailsView
		};
	});