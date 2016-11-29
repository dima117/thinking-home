define(
	['lib', 'text!my-plugin/xxx-filter.tpl'],			// список зависимостей
	function (lib, myTemplate) {	// функция инициализации нашего модуля 

	    var myFilterView = lib.marionette.ItemView.extend({
	        template: lib.handlebars.compile(myTemplate)
	    });

		// шаблон содержимого: заголовок и кнопка
		var myTemplate = lib.handlebars.compile(
			'<h1>Hello!</h1><input class="msg" /><input type="button" class="btn btn-default" value="click me" />');

		var layoutTemplate = '<div>' +
            '<h1>List items</h1>' +
            '<div id="region-filter"></div>' +
            '<div id="region-list"></div>' +
            '</div>';

	    // определяем параметры представления
		var myLayout = lib.marionette.LayoutView.extend({
		    template: lib.handlebars.compile(layoutTemplate),
		    regions: {
		        filter: '#region-filter',
		        list: '#region-list'
		    }
		});
		// описываем представление
		var myView = lib.marionette.ItemView.extend({

			// шаблон
			template: myTemplate,

			// события
			triggers: { 'click .btn': 'my-event' },

            getMsg: function() {
                return this.$('.msg').val();
            }
		});

		// описываем новый раздел
		var mySection = lib.common.AppSection.extend({

			// действия при открытии страницы
			start: function () {
			    // создаем экземпляр layout view и добавляем его на страницу
			    var layoutView = new myLayout();
			    this.application.setContentView(layoutView);

				// создаем экземпляр представления
				var view = new myView();

				// подписываемся на события
				this.listenTo(view, 'my-event', function() {
				    this.application.radio.sendMessage('channel-name', view.getMsg());
				});

				this.listenTo(this.application.radio, 'channel-name', function (msg) {
				    alert(msg.data);
                });

				var filterView = new myFilterView();

				// отображаем представление пользователю
				layoutView.filter.show(filterView);
				layoutView.list.show(view);
			}
		});

		return mySection;
	});