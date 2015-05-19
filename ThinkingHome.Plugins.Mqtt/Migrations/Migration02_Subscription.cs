using System.Data;
using ECM7.Migrator.Framework;
using ForeignKeyConstraint = ECM7.Migrator.Framework.ForeignKeyConstraint;

namespace ThinkingHome.Plugins.Mqtt.Migrations
{
	[Migration(2)]
	public class Migration02_Subscription : Migration
	{
		public override void Apply()
		{
			Database.AddTable("Mqtt_Subscription",
				new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey, "newid()"),
				new Column("Path", DbType.String, ColumnProperty.NotNull, 0),
				new Column("BrokerId", DbType.Guid, ColumnProperty.NotNull));

			Database.AddForeignKey("FK_Mqtt_Subscription_BrokerId",
				"Mqtt_Subscription",
				"BrokerId",
				"Mqtt_Broker",
				"Id",
				ForeignKeyConstraint.Cascade);
		}

		public override void Revert()
		{
			Database.RemoveTable("Mqtt_Subscription");
		}
	}
}
