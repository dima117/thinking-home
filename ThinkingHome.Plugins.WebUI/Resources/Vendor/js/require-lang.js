define(['text', 'i18n'], function (text, i18nManager) {
	var jsonParse = (typeof JSON !== 'undefined' && typeof JSON.parse === 'function') 
		? JSON.parse 
		: function(val) { return eval('(' + val + ')'); };

	return {
		load: function (name, req, onload, config) {
			text.get(req.toUrl(name), function (data) {
				var json = jsonParse(data),
					manager = new i18nManager(json);

				onload(manager);
			});
		}
	}
});
