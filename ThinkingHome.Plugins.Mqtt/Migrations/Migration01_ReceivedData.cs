using System.Data;
using ECM7.Migrator.Framework;

namespace ThinkingHome.Plugins.Mqtt.Migrations
{
	[Migration(1)]
	public class Migration01_ReceivedData : Migration
	{
		public override void Apply()
		{
			Database.AddTable("Mqtt_ReceivedData",
				new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey, "newid()"),
				new Column("Path", DbType.String, ColumnProperty.NotNull),
				new Column("Timestamp", DbType.DateTime, ColumnProperty.NotNull, "getdate()")
			);

			Database.AddUniqueConstraint("UK_Mqtt_ReceivedData", "Mqtt_ReceivedData", "Path");
		}

		public override void Revert()
		{
			Database.RemoveTable("Mqtt_ReceivedData");
		}
	}
}
