<div class="form-group">
	<label>
		<%=displayName%>
	</label>
	<%if (items && items.length) {%>

		<select name="<%=name%>" class="form-control js-field">
			<option value="">&lt;EMPTY&gt;</option>
		</select>

	<%} else {%>
	
		<input name="<%=name%>" class="form-control js-field" />
	<%}%>
</div>