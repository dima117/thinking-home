using System.Data;
using ECM7.Migrator.Framework;

namespace ThinkingHome.Plugins.Mqtt.Migrations
{
	[Migration(2)]
	public class Migration02_ReceivedDataMessage : Migration
	{
		public override void Apply()
		{
			Database.AddColumn("Mqtt_ReceivedData", 
				new Column("Message", DbType.String.WithSize(int.MaxValue), ColumnProperty.Null));
		}

		public override void Revert()
		{
			Database.RemoveColumn("Mqtt_ReceivedData", "Mqtt_ReceivedData");
		}
	}
}