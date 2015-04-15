using System.Data;
using ECM7.Migrator.Framework;

namespace ThinkingHome.Plugins.Microclimate.Migrations
{
	[Migration(1)]
	public class Migration01_TemperatureSensor : Migration
	{
		public override void Apply()
		{
			Database.AddTable("Microclimate_TemperatureSensor",
				new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey, "newid()"),
				new Column("Channel", DbType.Int32, ColumnProperty.NotNull),
				new Column("DisplayName", DbType.String.WithSize(255), ColumnProperty.NotNull),
				new Column("ShowHumidity", DbType.Boolean, ColumnProperty.NotNull, false)
			);
		}

		public override void Revert()
		{
			Database.RemoveTable("Microclimate_TemperatureSensor");
		}
	}
}
