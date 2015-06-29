define(
	['lib'],
	function (lib) {

		var widgetCollection = lib.backbone.Collection.extend({
			comparator: "sortOrder"
		});

		var api = {
			loadWidgetList: function (id) {

				var defer = lib.$.Deferred();

				lib.$.getJSON('/api/uniui/widget/list', { id: id })
					.done(function (data) {

						var info = new lib.backbone.Model(data.info),
							widgets = new widgetCollection(data.widgets);

						defer.resolve({ info: info, widgets: widgets });
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		return {
			loadWidgetList: api.loadWidgetList
		};
	});