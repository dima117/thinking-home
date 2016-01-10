<div class="form-group">
	<label>
		{{displayName}}
	</label>

	{{#if items}}
	<select name="{{name}}" class="form-control js-field">
		<option value="">&lt;{{lang 'EMPTY'}}&gt;</option>
	</select>
	{{else}}
	<input name="{{name}}" class="form-control js-field" />
	{{/if}}
</div>