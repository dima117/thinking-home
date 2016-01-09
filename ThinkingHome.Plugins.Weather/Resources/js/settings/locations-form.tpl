<form role="form">
	<div class="form-group">
		<label>
			{{lang 'Display_name'}}
		</label>
		<input name="displayName" class="form-control" placeholder="{{lang 'example_Moscow'}}" />
	</div>
	<div class="form-group">
		<label>
			{{lang 'Weather_service_API_query'}}
		</label>
		<input name="query" class="form-control" placeholder="{{lang 'example_Moscow_ru'}}" />
	</div>

	<input type="button" class="btn btn-primary js-btn-add-location" value="{{lang 'Add_location'}}" />
</form>


