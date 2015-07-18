<div class="th-side-panel th-dashboard-list js-menu"></div>
<div class="th-container th-widget-container">
	{{#each this}}
	<div id="{{id}}" class="th-dashboard-widget">
		<div class="th-dashboard-widget-block">
			<div class="th-dashboard-widget-block-title">{{displayName}}</div>
			<p class="text-muted">loading...</p>
		</div>
	</div>
	{{/each}}
</div>