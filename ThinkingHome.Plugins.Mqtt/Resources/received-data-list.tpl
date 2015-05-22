<div>
	<h1>
		MQTT received data
	</h1>
	<div class="row">
		<div class="col-md-12">
			<h2>Connection</h2>
			<dl>
				<dt>host</dt>
				<dd>
					<%=host%>:<%=port%>
				</dd>
				<dt>path</dt>
				<dd>
					<%=path%>
				</dd>
			</dl>
		</div>
		<div class="col-md-12">
			<h2>Messages</h2>
			<table class="table">
				<thead>
					<tr>
						<th>path</th>
						<th>updated</th>
						<th>message</th>
					</tr>
				</thead>
				<tbody></tbody>
			</table>
		</div>
	</div>
</div>
