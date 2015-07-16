<div class="th-side-panel th-dashboard-list js-menu"></div>
<div class="th-container th-widget-container">
	{{#each this}}
	<div id="{{id}}" class="th-dashboard-widget">
		<p class="text-muted">{{displayName}}...</p>
	</div>
	{{/each}}
</div>