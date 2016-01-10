<div>
	<ul class="breadcrumb th-margin-top-16">
		<li>
			<a href="#" class="js-dashboard-list">{{lang 'Dashboard_list'}}</a>
		</li>
		<li>
			<a href="#" class="js-dashboard">
				{{dashboardTitle}}
			</a>
		</li>
		<li class="active">
			{{panelTitle}} &rarr; {{typeDisplayName}}
		</li>
	</ul>
	<div class="row">
		<div class="col-md-8">
			<div class="form-group">
				<label>
					{{lang 'Display_name'}}
				</label>
				<input class="form-control js-display-name" />
			</div>
			<form class="js-fields">
			</form>
			<div>
				<a href="#" class="btn btn-danger pull-right js-delete">{{lang 'Delete_widget'}}</a>
				<a href="#" class="btn btn-primary js-save">{{lang 'Save'}}</a>
				<a href="#" class="btn btn-default js-cancel">{{lang 'Cancel'}}</a>
			</div>
		</div>
	</div>
</div>