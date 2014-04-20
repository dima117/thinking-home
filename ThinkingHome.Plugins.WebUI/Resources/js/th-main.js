requirejs.config({
	baseUrl: 'js',
	paths: {
		jquery: 'jquery.min',
		underscore: 'underscore.min'
	}
	//},
	//shim: {
	//	underscore: { exports: '_' }
	//}
});

require(['underscore', 'jquery'], function (xxx, jq) {
	console.log('ver=', $.fn.jquery);
	console.log('jq ver=', jq.fn.jquery);
	
	console.log('underscore identity call: ', _.identity(5));
	console.log('underscore identity call: ', xxx.identity(5));
});