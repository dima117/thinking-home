<div class="th-side-panel th-dashboard-list js-menu"></div>
<div class="th-container th-dashboard-container">
	{{#each this}}
	<div id="{{id}}" class="th-widget">
		<div class="th-widget-container">
			<div class="th-widget-block">
				<div class="th-widget-block-title">{{displayName}}</div>
				<div class="th-widget-block-content">
					<p class="text-muted">loading...</p>
				</div>
			</div>
		</div>
	</div>
	{{/each}}
</div>