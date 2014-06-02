<h3><%= hours %>:<%= ('0' + minutes).slice(-2) %>&emsp;<%= name %></h3>
<p class="js-run-script">
	<strong>Execute user script:</strong> <%= scriptName %>.
</p>
<p class="js-play-sound">
	<strong>Play sound.</strong>
</p>
<div>
	<a href="#" class="btn btn-default btn-xs js-btn-enable">Enable</a>
	<a href="#" class="btn btn-default btn-xs js-btn-disable">Disable</a>&nbsp;
	<a href="#" class="btn btn-default btn-xs js-btn-edit">Edit</a>&nbsp;
	<a href="#" class="btn btn-danger btn-xs js-btn-delete">Delete</a>
</div>
