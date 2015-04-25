define(['lib'],
	function (lib) {

		var tchart = undefined,
			hchart = undefined;

		var api = {
			prepareData: function (model) {
				var tdataset = [],
					hdataset = [],
					data = model.get('data');

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

			build: function (view, showHumidity) {

				var data = api.prepareData(view.model);
				var options = {
					scaleType: "date",
					responsive: true
				};

				var ctxt = view.ui.tChart.get(0).getContext("2d");
				tchart = new lib.Chart(ctxt).Scatter([
					{
						label: 'Temperature',
						strokeColor: '#428bca',
						data: data.tdataset
					}], lib._.extend(options, { scaleLabel: "<%=value%>°C" }));

				if (showHumidity) {

					var ctxh = view.ui.hChart.get(0).getContext("2d");
					hchart = new lib.Chart(ctxh).Scatter([
						{
							label: 'Humidity',
							strokeColor: '#d9534f',
							data: data.hdataset
						}], lib._.extend(options, { scaleLabel: "<%=value%>%" }));
				}
			}
		};

		return {
			build: api.build,
			getCharts: function () {
				return {
					t: tchart,
					h: hchart
				};
			}
		};
	});