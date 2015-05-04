<form>
	<h1>
		<%= name %>
	</h1>
	<p>
		<input type="button" value="Save" class="btn btn-primary js-btn-save" />&nbsp;
		<input type="button" value="Cancel" class="btn btn-default js-btn-cancel" />
	</p>
	<div class="th-margin-bottom-6">
		<textarea name="body" class="js-script-body " />
	</div>
	<p>
		<a href="#" class="btn btn-default btn-xs js-full-screen-editing">Full screen editing</a>
	</p>
	<p class="help-block">
		Press <kbd>Esc</kbd> when cursor is in the editorto toggle exit full screen editing.
	</p>
</form>
