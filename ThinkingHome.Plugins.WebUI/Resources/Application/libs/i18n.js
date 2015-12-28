define([], function() {
	var i18nManager = function(data) {
		this.data = data;
	}

	i18nManager.prototype.get = function(key, defaultValue) {
		return (this.data && this.data[key]) || defaultValue || key;
	}

	return i18nManager;
});