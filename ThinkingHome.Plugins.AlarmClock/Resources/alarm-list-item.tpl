<div class="th-margin-bottom-32">
		<h3>
			<a href="#" 
				class="btn th-btn-width-96 pull-right js-btn-enable"
				data-action-text="Enable"
				data-action-class="btn-primary"
				data-state-class="btn-default">Disabled</a>

			<a href="#" 
				class="btn th-btn-width-96 pull-right js-btn-disable"
				data-action-text="Disable"
				data-action-class="btn-danger"
				data-state-class="btn-success">Enabled</a>
			<a href="#" class="js-btn-edit">
				<%=hours%>:<%=('0' + minutes).slice(-2)%>
			</a>
		</h3>
		<h4>
			<a href="#" class="js-btn-edit">
				<%=name%>
			</a>
		</h4>
		<p class="js-run-script">
			Execute user script: <em>
				<%= scriptName %>
			</em>.
		</p>
		<p class="js-play-sound">
			Play sound.
		</p>
</div>
