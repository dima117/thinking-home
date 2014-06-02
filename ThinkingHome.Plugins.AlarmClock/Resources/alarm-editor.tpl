<form>
	<h1>Edit alarm settings</h1>
	<p>
		<input type="button" value="Save" class="btn btn-primary js-btn-save" />&nbsp;
		<input type="button" value="Cancel" class="btn btn-default js-btn-cancel" />
	</p>
	<div class="form-group">
		<label>
			Name
		</label>
		<div class="row">
			<div class="col-sm-4">
				<input name="name" class="form-control" />
			</div>
		</div>
	</div>
	<div class="form-group">
		<label>
			Time
		</label>
		<ul class="list-inline">
			<li>
				<select name="hours" class="form-control">
					<% for (var h = 0; h < 24; h++) { %>
					<option value="<%= h %>"><%= ('0' + h).slice(-2) %></option>
					<% } %>
				</select></li>
			<li>
				<select name="minutes" class="form-control">
					<% for (var m = 0; m < 60; m++) { %>
					<option value="<%= m %>"><%= ('0' + m).slice(-2) %></option>
					<% } %>
				</select></li>
		</ul>
	</div>
	<div class="form-group">
		<label>
			Action
		</label>
		<div class="row">
			<div class="col-sm-4">
				<select name="scriptId" data-items-field="scripts" class="form-control">
					<option value="">&lt;PLAY SOUND&gt;</option>
				</select>
			</div>
		</div>
	</div>
</form>
