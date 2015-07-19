<div class="th-side-panel th-dashboard-list js-menu"></div>
<div class="th-container th-dashboard-container">
	{{#each this}}
	<div id="{{id}}" class="th-dashboard-widget">
		<div class="th-dashboard-widget-container">
			<div class="th-dashboard-widget-block">
				<div class="th-dashboard-widget-block-title">{{displayName}}</div>
				<div class="th-dashboard-widget-block-content">
					<p class="text-muted">loading...</p>
				</div>
			</div>
		</div>
	</div>
	{{/each}}
</div>