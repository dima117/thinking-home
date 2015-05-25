using System.Data;
using ECM7.Migrator.Framework;

namespace ThinkingHome.Plugins.Microclimate.Migrations
{
	[Migration(4)]
	public class Migration04_SensorCurrentData : Migration
	{
		public override void Apply()
		{
			Database.AddColumn("Microclimate_TemperatureSensor",
				new Column("CurrentTemperature", DbType.Int32, ColumnProperty.NotNull, 0));

			Database.AddColumn("Microclimate_TemperatureSensor",
				new Column("CurrentHumidity", DbType.Int32, ColumnProperty.NotNull, 0));

			Database.AddColumn("Microclimate_TemperatureSensor",
				new Column("Timestamp", DbType.DateTime, ColumnProperty.NotNull, "getdate()"));
		}

		public override void Revert()
		{
			Database.RemoveColumn("Microclimate_TemperatureSensor", "CurrentTemperature");
			Database.RemoveColumn("Microclimate_TemperatureSensor", "CurrentHumidity");
			Database.RemoveColumn("Microclimate_TemperatureSensor", "Timestamp");
		}
	}
}
