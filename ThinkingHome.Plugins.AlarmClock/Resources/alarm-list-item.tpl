<div class="th-margin-bottom-32">
	<h3>
		<a href="#"
			class="btn th-btn-width-120 pull-right js-btn-enable"
			data-action-text="{{lang 'Enable'}}"
			data-action-class="btn-primary"
			data-state-class="btn-default">{{lang 'Disabled'}}</a>

		<a href="#"
			class="btn th-btn-width-120 pull-right js-btn-disable"
			data-action-text="{{lang 'Disable'}}"
			data-action-class="btn-danger"
			data-state-class="btn-success">{{lang 'Enabled'}}</a>
		<a href="#" class="js-btn-edit">
			{{hours}}:{{pad minutes 2}}
		</a>
	</h3>
	<h4>
		<a href="#" class="js-btn-edit">
			{{name}}
		</a>
	</h4>
	<p>
		{{#if scriptId}}
		{{lang 'Execute_user_script'}}: <em>{{scriptName}}</em>.
		{{else}}
		{{lang 'Play_sound'}}
		{{/if}}
	</p>
</div>
