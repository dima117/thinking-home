function thObject(config) {

	this.config = config;

	this.getUrl = function (plugin, method) {
		return this.config.urlFormat
			.replace('{0}', plugin)
			.replace('{1}', method);
	};

	this.send = function(plugin, method, data, onSuccess) {

		var json = JSON.stringify(data);

		$.ajax({
			type: 'GET',
			dataType: 'jsonp',
			url: this.getUrl(plugin, method),
			data: { json: json, callback: '?' }
		})
		.done(onSuccess);
	};
	

}