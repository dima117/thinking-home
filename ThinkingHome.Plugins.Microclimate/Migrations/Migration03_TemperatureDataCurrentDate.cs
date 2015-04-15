using System.Data;
using ECM7.Migrator.Framework;

namespace ThinkingHome.Plugins.Microclimate.Migrations
{
	[Migration(3)]
	public class Migration03_TemperatureDataCurrentDate : Migration
	{
		public override void Apply()
		{
			Database.AddColumn("Microclimate_TemperatureData", 
				new Column("CurrentDate", DbType.DateTime, ColumnProperty.NotNull, "getdate()"));
		}

		public override void Revert()
		{
			Database.RemoveColumn("Microclimate_TemperatureData", "CurrentDate");
		}
	}
}