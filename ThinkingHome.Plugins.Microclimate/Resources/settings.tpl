<div>
	<h1>
		Microclimate sensors
	</h1>
	<div class="row">
		<div class="col-md-12">
			<form class="form-inline mc-form-add-sensor" role="form">
				<div class="form-group">
					<label for="tb-display-name">Display name</label>
					<input id="tb-display-name" class="form-control" type="text" />
				</div>
				<div class="form-group">
					<label for="select-channel">Channel</label>
					<select id="select-channel" class="form-control">
						{{#range 0 64 1}}
						<option value="{{this}}">{{this}}</option>
						{{/range}}
					</select>
				</div>
				<div class="checkbox">
					<label>
						<input id="cb-show-humidity" type="checkbox" /> Show humidity
					</label>
				</div>
				<input type="button" class="btn btn-primary js-add-sensor" value="Add sensor" />
			</form>
		</div>
		<div class="col-md-12">
			<table class="table">
				<thead>
					<tr>
						<th>display name</th>
						<th>channel</th>
						<th>show humidity</th>
						<th colspan="2"></th>
					</tr>
				</thead>
				<tbody></tbody>
			</table>
		</div>
	</div>
</div>
