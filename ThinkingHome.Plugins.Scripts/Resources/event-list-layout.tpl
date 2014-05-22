<div class="col-md-11">
	<h1>Events</h1>
	<div class="col-md-4">
		<form role="form">
			<div class="form-group">
				<label>
					event type
				</label>
				<select name="selectedEventId" data-items-field="eventList" class="form-control" />
			</div>
			<div class="form-group">
				<label>
					execute script
				</label>
				<select name="selectedScriptId" data-items-field="scriptList" class="form-control" />
				
			</div>
			<input type="button" class="btn btn-primary" value="Add subscription" />
		</form>
	</div>
	<div id="event-handler-list" class="col-md-7"></div>
</div>