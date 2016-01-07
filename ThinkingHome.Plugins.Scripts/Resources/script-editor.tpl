<form>
	<h1>
		{{name}}
	</h1>
	<p>
		<input type="button" value="{{lang 'Save'}}" class="btn btn-primary js-btn-save" />&nbsp;
		<input type="button" value="{{lang 'Cancel'}}" class="btn btn-default js-btn-cancel" />
	</p>
	<div class="th-margin-bottom-6">
		<textarea name="body" class="js-script-body " />
	</div>
	<div class="cm-s-bootstrap-dark js-editor-panel">
		<a href="#" class="btn btn-default btn-xs js-full-screen">{{lang 'Fullscreen_editing'}}</a>
		<a href="#" class="btn btn-default btn-xs js-exit-full-screen hidden">{{lang 'Exit_fullscreen'}}</a>
	</div>
</form>
