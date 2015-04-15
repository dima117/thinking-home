<div class="mc-sensor-actions"></div>
<div class="mc-sensor-name">
	<%= displayName %>
</div>

<% if (data) { %>

<div class="mc-sensor-data">
	<div class="mc-sensor-data-addon">
		
		<div>
			<% if (showHumidity) { %>
				h: <%= data.dh %>
			<% } else { %>
				&nbsp;
			<% } %>
		</div>
		<div>
			<strong>on <%= data.dd %></strong>
		</div>
		<% if (data.ddd) { %>
		<div>
			<small>
				<%= data.ddd %>
			</small>
		</div>
		<% } %>
	</div>
	<div class="mc-sensor-data-t">
		<%= data.dt %>
	</div>

</div>
<% } else { %>

<div class="mc-sensor-no-data">
	The sensor has no data
</div>
<% } %>

