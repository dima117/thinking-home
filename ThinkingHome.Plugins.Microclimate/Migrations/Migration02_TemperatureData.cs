using System.Data;
using ECM7.Migrator.Framework;
using ForeignKeyConstraint = ECM7.Migrator.Framework.ForeignKeyConstraint;

namespace ThinkingHome.Plugins.Microclimate.Migrations
{
	[Migration(2)]
	public class Migration02_TemperatureData : Migration
	{
		public override void Apply()
		{
			Database.AddTable("Microclimate_TemperatureData",
				new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey, "newid()"),
				new Column("Temperature", DbType.Int32, ColumnProperty.NotNull, 0),
				new Column("Humidity", DbType.Int32, ColumnProperty.NotNull, 0),
				new Column("SensorId", DbType.Guid, ColumnProperty.NotNull)
			);

			Database.AddForeignKey("FK_Microclimate_TemperatureData_SensorId",
				"Microclimate_TemperatureData", "SensorId", 
				"Microclimate_TemperatureSensor", "Id", ForeignKeyConstraint.Cascade);
		}

		public override void Revert()
		{
			Database.RemoveTable("Microclimate_TemperatureData");
		}
	}
}
