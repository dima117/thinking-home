<form role="form">
	<div class="form-group">
		<label>
			{{lang 'Event'}}
		</label>
		<select name="selectedEventAlias" class="form-control js-event-list" />
	</div>
	<div class="form-group">
		<label>
			{{lang 'Script'}}
		</label>
		<select name="selectedScriptId" class="form-control js-script-list" />

	</div>
	<input type="button" class="btn btn-primary js-btn-add-subscription" value="{{lang 'Add_subscription'}}" />
</form>
