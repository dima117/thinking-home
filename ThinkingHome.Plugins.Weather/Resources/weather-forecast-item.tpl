<div class="row">
	<div class="col-md-4 col-sm-12">
		<h2 class="text-primary"><%= displayName %></h2>
		<span style="font-size: 52px;">
			<i class="wi wi-day-lightning"></i><%= now.t %>&deg;C
		</span>
	</div>
	<div class="col-md-3 col-sm-5">
		<h3>Today</h3>
		<ul class="list-unstyled" style="font-size: 22px;">
			<% _.each(today, function(obj) { %>
				<li><%= obj.time %> &mdash; <i class="wi wi-day-thunderstorm"></i><%= obj.t %>&deg;C</li>
			<% }); %>
		</ul>
	</div>
	<div class="col-md-4 col-sm-5">
		<h3>Forecast</h3>
		<ul class="list-unstyled" style="font-size: 22px; white-space: nowrap;">
			<li>Jun, 2 &mdash; <i class="wi wi-day-thunderstorm"></i>+8..+11&deg;C</li>
			<li>Jun, 3 &mdash; <i class="wi wi-day-rain"></i>+10..+12&deg;C</li>
			<li>Jun, 4 &mdash; <i class="wi wi-day-sunny"></i>+4..+6&deg;C</li>
			<li>Jun, 5 &mdash; <i class="wi wi-day-sunny"></i>+4..+8&deg;C</li>
		</ul>
	</div>
</div>
