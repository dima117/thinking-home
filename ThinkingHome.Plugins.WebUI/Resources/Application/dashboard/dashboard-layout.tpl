<div class="th-side-panel th-dashboard-list js-menu"></div>
<div class="th-container th-dashboard-container">
	{{#each this}}
	<div class="panel panel-default">
		<div class="panel-heading">
			<h3 class="panel-title">{{title}}</h3>
		</div>
		<div class="panel-body">
			{{#each widgets}}
			<div id="{{id}}">
				<p class="text-muted">loading...</p>
			</div>
			{{/each}}
		</div>
	</div>
	{{/each}}
</div>