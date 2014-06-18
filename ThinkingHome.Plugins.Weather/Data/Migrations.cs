using System.Data;
using ECM7.Migrator.Framework;
using ForeignKeyConstraint = ECM7.Migrator.Framework.ForeignKeyConstraint;

[assembly: MigrationAssembly("ThinkingHome.Plugins.Weather")]

namespace ThinkingHome.Plugins.Weather.Data
{
	[Migration(1)]
	public class Migration01WeatherLocationTable : Migration
	{
		public override void Apply()
		{
			Database.AddTable("Weather_Location",
				new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey, "newid()"),
				new Column("Query", DbType.String.WithSize(200), ColumnProperty.NotNull),
				new Column("DisplayName", DbType.String.WithSize(200), ColumnProperty.NotNull)
			);
		}

		public override void Revert()
		{
			Database.RemoveTable("Weather_Location");
		}
	}

	[Migration(2)]
	public class Migration02WeatherDataTable : Migration
	{
		public override void Apply()
		{
			Database.AddTable("Weather_Data",
				new Column("Date", DbType.DateTime, ColumnProperty.PrimaryKey),
				new Column("LocationId", DbType.Guid, ColumnProperty.PrimaryKey),
				new Column("Pressure", DbType.Decimal.WithSize(5, 2), ColumnProperty.NotNull),
				new Column("Humidity", DbType.Int32, ColumnProperty.NotNull),
				new Column("Cloudiness", DbType.Int32, ColumnProperty.NotNull),
				new Column("WindSpeed", DbType.Decimal.WithSize(5, 2), ColumnProperty.NotNull),
				new Column("WindDirection", DbType.Decimal.WithSize(5, 4), ColumnProperty.NotNull),
				new Column("WeatherDescription", DbType.String.WithSize(50), ColumnProperty.Null),
				new Column("WeatherCode", DbType.String.WithSize(5), ColumnProperty.Null)
			);

			Database.AddForeignKey("FK_Weather_Data_LocationId",
				"Weather_Data", "LocationId", "Weather_Location", "Id", ForeignKeyConstraint.Cascade);
		}

		public override void Revert()
		{
			Database.RemoveConstraint("Weather_Data", "FK_Weather_Data_LocationId");
			Database.RemoveTable("Weather_Data");
		}
	}
}