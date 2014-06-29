<div class="row">
	<div class="col-md-4 col-sm-12">
		<h2 class="text-primary"><%= name %></h2>
		<span class="weather-now">
			<i class="wi <%= now.icon %>"></i>&nbsp;<%= now.t %>&deg;C
		</span>
	</div>
	<div class="col-md-3 col-sm-5">
		<h3>Next day</h3>
		<ul class="list-unstyled weather-list">
			<% _.each(day, function(obj) { %>
			<li><%= obj.time %> &mdash; <i class="wi <%= obj.icon %>"></i>&nbsp;<%= obj.t %>&deg;C
			</li>
			<% }); %>
		</ul>
	</div>
	<div class="col-md-4 col-sm-5">
		<h3>Forecast</h3>
		<ul class="list-unstyled weather-list">
			<% _.each(forecast, function(obj) { %>
			<li><%= obj.date %> &mdash; <i class="wi <%= obj.icon %>"></i>&nbsp;<%= obj.t %>&deg;C
			</li>
			<% }); %>
		</ul>
	</div>
</div>
