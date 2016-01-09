{{#each this}}
<div class="panel panel-default js-panel">
	<div class="panel-heading">{{title}}</div>
	<div class="panel-body">
		{{#each widgets}}
		<div id="{{id}}" class="th-margin-bottom-8">
			<p class="text-muted">{{lang 'loading'}}...</p>
		</div>
		{{/each}}
	</div>
</div>
{{/each}}
