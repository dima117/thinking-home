define(['app', 'webapp/webui/tiles-model', 'webapp/webui/tiles-view'], function (application) {

	application.module('WebUI.Tiles', function (module, app, backbone, marionette, $, _) {

		module.start = function () {

			var obj = ['4:00 - 10°C', '10:00 - 12°C', '16:00 - 18°C', '22:00 - 14°C'];

			var data = [
				{ id: 1, title: 'Погода', content: obj },
				{ id: 2, title: 'Будильник' },
				{ id: 3, title: 'Расписание' },
				{ id: 4, title: 'Погода', content: obj },
				{ id: 5, title: 'Расписание электричек', wide: true },
				{ id: 6, title: 'Будильник' },
				{ id: 7, title: 'Новости', wide: true },
				{ id: 8, title: 'Расписание' },
				{ id: 9, title: 'Погода', content: obj },
				{ id: 10, title: 'Youtube' }
			];
			var collection = new module.TileCollection(data);
			var view = new module.TileCollectionView({
				collection: collection
			});

			app.setContentView(view);
		};
	});

	return application.WebUI.Tiles;
});