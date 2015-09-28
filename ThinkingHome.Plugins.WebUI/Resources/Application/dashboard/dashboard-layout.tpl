<div class="th-side-panel th-dashboard-list js-menu"></div>
<div class="th-container th-dashboard-container">
	<div class="js-container">
		{{#each this}}
		<div class="panel panel-default js-panel">
			<div class="panel-heading">{{title}}</div>
			<div class="panel-body">
				{{#each widgets}}
				<div id="{{id}}" class="th-margin-bottom-8">
					<p class="text-muted">loading...</p>
				</div>
				{{/each}}
			</div>
		</div>
		{{/each}}
	</div>
</div>