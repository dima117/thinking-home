<div>
	<h1>
		{{lang 'MQTT_received_data'}}
	</h1>
	<div class="row">
		<div class="col-md-12">
			<h2>{{lang 'Connection'}}</h2>
			<dl>
				<dt>{{lang 'host'}}</dt>
				<dd>{{host}}:{{port}}</dd>
				<dt>{{lang 'path'}}</dt>
				<dd>{{path}}</dd>
			</dl>
			<p>
				<a href="#" class="btn btn-default js-reload">{{lang 'Reload'}}</a>
			</p>
		</div>
		<div class="col-md-12">
			<h2>{{lang 'Messages'}}</h2>
			<table class="table">
				<thead>
					<tr>
						<th>{{lang 'path'}}</th>
						<th>{{lang 'updated'}}</th>
						<th colspan="2">{{lang 'message'}}</th>
					</tr>
				</thead>
				<tbody></tbody>
			</table>
		</div>
	</div>
</div>
