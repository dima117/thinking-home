<div>
	<h1>
		{{lang 'Microclimate_sensors'}}
	</h1>
	<div class="row">
		<div class="col-md-12">
			<form class="form-inline mc-form-add-sensor" role="form">
				<div class="form-group">
					<label for="tb-display-name">{{lang 'Display_name'}}</label>
					<input id="tb-display-name" class="form-control" type="text" />
				</div>
				<div class="form-group">
					<label for="select-channel">{{lang 'Channel'}}</label>
					<select id="select-channel" class="form-control">
						{{#range 0 63 1}}
						<option value="{{this}}">{{this}}</option>
						{{/range}}
					</select>
				</div>
				<div class="checkbox">
					<label>
						<input id="cb-show-humidity" type="checkbox" /> {{lang 'Show_humidity'}}
					</label>
				</div>
				<input type="button" class="btn btn-primary js-add-sensor" value="{{lang 'Add_sensor'}}" />
			</form>
		</div>
		<div class="col-md-12">
			<table class="table">
				<thead>
					<tr>
						<th>{{lang 'display_name_lower'}}</th>
						<th>{{lang 'channel_lower'}}</th>
						<th>{{lang 'show_humidity_lower'}}</th>
						<th colspan="2"></th>
					</tr>
				</thead>
				<tbody></tbody>
			</table>
		</div>
	</div>
</div>
