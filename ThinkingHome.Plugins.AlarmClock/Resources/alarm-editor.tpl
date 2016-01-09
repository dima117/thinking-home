<form>
	<h1>{{lang 'Edit_alarm_settings'}}</h1>
	<div class="row">
		<div class="col-md-4">
			<div class="form-group">
				<label>
					{{lang 'Name'}}
				</label>
				<input name="name" class="form-control" />
			</div>
			<div class="form-group">
				<label>
					{{lang 'Time'}}
				</label>
				<ul class="list-inline">
					<li>
						<select name="hours" class="form-control">
							{{#range 0 23 1}}
							<option value="{{this}}">{{pad this 2}}</option>
							{{/range}}
						</select>
					</li>
					<li>
						<select name="minutes" class="form-control">
							{{#range 0 59 1}}
							<option value="{{this}}">{{pad this 2}}</option>
							{{/range}}
						</select>
					</li>
				</ul>
			</div>
			<div class="form-group">
				<label>
					{{lang 'Action'}}
				</label>
				<select name="scriptId" class="form-control js-script-list">
					<option value="">&lt;{{lang 'Play_sound'}}&gt;</option>
				</select>
			</div>
			<p>
				<input type="button" value="{{lang 'Save'}}" class="btn btn-primary js-btn-save" />&nbsp;
				<input type="button" value="{{lang 'Cancel'}}" class="btn btn-default js-btn-cancel" />
				<input type="button" value="{{lang 'Delete'}}" class="btn btn-danger pull-right js-btn-delete" />
			</p>
		</div>
	</div>
</form>
