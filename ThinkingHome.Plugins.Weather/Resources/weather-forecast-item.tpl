<div class="row">
	<div class="col-md-4 col-sm-12">
		<h2 class="text-primary"><%= displayName %></h2>
		<span class="weather-now">
			<i class="wi wi-day-lightning"></i>&nbsp;<%= now.t %>&deg;C
		</span>
	</div>
	<div class="col-md-3 col-sm-5">
		<h3>Today</h3>
		<ul class="list-unstyled weather-list">
			<% _.each(today, function(obj) { %>
			<li><%= obj.time %> &mdash; <i class="wi wi-day-thunderstorm"></i>&nbsp;<%= obj.t %>&deg;C
			</li>
			<% }); %>
		</ul>
	</div>
	<div class="col-md-4 col-sm-5">
		<h3>Forecast</h3>
		<ul class="list-unstyled weather-list">
			<li>Jun, 2 &mdash; <i class="wi wi-day-thunderstorm"></i>&nbsp;+8..+11&deg;C</li>
			<li>Jun, 3 &mdash; <i class="wi wi-day-rain"></i>&nbsp;+10..+12&deg;C</li>
			<li>Jun, 4 &mdash; <i class="wi wi-day-sunny"></i>&nbsp;+4..+6&deg;C</li>
		</ul>
	</div>
</div>
