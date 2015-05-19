using System.Data;
using ECM7.Migrator.Framework;

namespace ThinkingHome.Plugins.Mqtt.Migrations
{
	[Migration(1)]
	public class Migration01_Broker : Migration
	{
		public override void Apply()
		{
			Database.AddTable("Mqtt_Broker",
				new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey, "newid()"),
				new Column("Alias", DbType.String, ColumnProperty.NotNull),
				new Column("Host", DbType.String, ColumnProperty.NotNull),
				new Column("Port", DbType.Int32, ColumnProperty.Null),
				new Column("NeedsAuthorization", DbType.Boolean, ColumnProperty.NotNull, false),
				new Column("Login", DbType.String, ColumnProperty.Null),
				new Column("Password", DbType.String, ColumnProperty.Null)
			);
		}

		public override void Revert()
		{
			Database.RemoveTable("Mqtt_Broker");
		}
	}
}
