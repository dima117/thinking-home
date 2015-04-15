define(
	['app', 'marionette', 'backbone', 'underscore', 'chart'],
	function (application, marionette, backbone, _, chartjs) {

		var tchart = undefined,
			hchart = undefined;

		var api = {
			prepareData: function (model) {
				var tdataset = [],
					hdataset = [],
					data = model.get('data');

				if (data && data.length) {
					_.each(data, function (el) {

						var timestamp = new Date(el.d).getTime();
						tdataset.push({ x: timestamp, y: el.t });
						hdataset.push({ x: timestamp, y: el.h });
					});

					tdataset = _.sortBy(tdataset, function (el) { return el.x; });
					hdataset = _.sortBy(hdataset, function (el) { return el.x; });
				}

				return { tdataset: tdataset, hdataset: hdataset };
			},

			build: function (view) {

				var data = api.prepareData(view.model);
				var options = {
					scaleType: "date",
					responsive: true
				};

				var ctxt = view.$('.js-chart-temperature').get(0).getContext("2d");
				tchart = new Chart(ctxt).Scatter([
					{
						label: 'Temperature',
						strokeColor: '#428bca',
						data: data.tdataset
					}], _.extend(options, { scaleLabel: "<%=value%>°C" }));

				if (view.model.get('showHumidity')) {

					var ctxh = view.$('.js-chart-humidity').get(0).getContext("2d");
					hchart = new Chart(ctxh).Scatter([
						{
							label: 'Humidity',
							strokeColor: '#d9534f',
							data: data.hdataset
						}], _.extend(options, { scaleLabel: "<%=value%>%" }));
				}
			}
		};

		return {
			build: api.build,
			getChart: function () { return chart; }
		};
	});