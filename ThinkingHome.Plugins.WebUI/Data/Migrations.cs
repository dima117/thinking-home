using System.Data;
using ECM7.Migrator.Framework;

[assembly: MigrationAssembly("ThinkingHome.Plugins.WebUI")]

namespace ThinkingHome.Plugins.WebUI.Data
{
	[Migration(1)]
	public class Migration01TilesTable : Migration
	{
		public override void Apply()
		{
			Database.AddTable("WebUI_Tile",
				new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey, "newid()"),
				new Column("HandlerKey", DbType.String.WithSize(200), ColumnProperty.NotNull),
				new Column("SortOrder", DbType.Int32, ColumnProperty.NotNull, 0)
			);
		}

		public override void Revert()
		{
			Database.RemoveTable("WebUI_Tile");
		}
	}
	
	[Migration(2)]
	public class Migration02TileParameters : Migration
	{
		public override void Apply()
		{
			Database.AddColumn("WebUI_Tile", 
				new Column("SerializedParameters", DbType.String.WithSize(int.MaxValue)));
		}

		public override void Revert()
		{
			Database.RemoveColumn("WebUI_Tile", "SerializedParameters");
		}
	}
}