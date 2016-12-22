define(['lib', 'webapp/scripts/script-list-model'], function (lib, models) {

    var executeScriptView = lib.marionette.ItemView.extend({
        template: lib.handlebars.compile(
			'<a href="#" class="btn btn-default btn-block js-btn-exec">' +
                '{{displayName}} <div class="text-muted"><small>{{data.scriptName}}<small></div></a>'),
        triggers: {
            'click .js-btn-exec': 'script:execute'
        }
    });

    var executeScriptWidget = lib.common.Widget.extend({
		show: function (model) {
		    var view = new executeScriptView({ model: model });
		    var scriptId = model.get('data').scriptId;

		    this.listenTo(view, 'script:execute', function () {
		        models.runScript(scriptId);
			});

			this.region.show(view);
		}	
	});

    return executeScriptWidget;
});
