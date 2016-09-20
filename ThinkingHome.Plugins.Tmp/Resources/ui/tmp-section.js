define(
	['lib'],			// список зависимостей
	function (lib) {	// функция инициализации нашего модуля 

		// шаблон содержимого: заголовок и кнопка
		var myTemplate = lib.handlebars.compile(
			'<h1>Hello!</h1><input type="button" class="btn btn-default" value="click me" />');

		// описываем представление
		var myView = lib.marionette.ItemView.extend({

			// шаблон
			template: myTemplate,

			// события
			triggers: { 'click input': 'my-event' }
		});

		// описываем новый раздел
		var mySection = lib.common.AppSection.extend({

			// действия при открытии страницы
			start: function () {

				// создаем экземпляр представления
				var view = new myView();

				// подписываемся на события
				this.listenTo(view, 'my-event', function() {
					alert('I\'m happy!');
				});

				// отображаем представление пользователю
				this.application.setContentView(view);
			}
		});

		return mySection;
	});