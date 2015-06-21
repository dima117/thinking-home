<div>
	<ul class="breadcrumb th-margin-top-16">
		<li>
			<a href="#" class="js-dashboard-list">Dashboard list</a>
		</li>
		<li>
			<a href="#" class="js-dashboard">
				<%=dashboardTitle%>
			</a>
		</li>
		<li class="active">
			<%=typeDisplayName%>
		</li>
	</ul>
	<form>
		<div class="form-group">
			<label>
				Display name
			</label>
			<input name="displayName" class="form-control js-display-name" />
		</div>
		<div class="js-fields"></div>
	</form>
	<div>
		<a href="#" class="btn btn-primary js-save">Save</a>
		<a href="#" class="btn btn-default js-cancel">Cancel</a>
	</div>
</div>