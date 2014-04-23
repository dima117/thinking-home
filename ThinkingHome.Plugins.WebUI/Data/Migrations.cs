using System.Data;
using ECM7.Migrator.Framework;

namespace ThinkingHome.Plugins.WebUI.Data
{
	[Migration(1)]
	public class Migration01NavigationItemTable : Migration
	{
		public override void Apply()
		{
			Database.AddTable("WebUI_NavigationItem",
				new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey, "newid()"),
				new Column("Name", DbType.String.WithSize(200), ColumnProperty.NotNull),
				new Column("ModulePath", DbType.String.WithSize(200), ColumnProperty.NotNull),
				new Column("SortOrder", DbType.Int32, ColumnProperty.NotNull, int.MaxValue)
			);
		}

		public override void Revert()
		{
			Database.RemoveTable("WebUI_NavigationItem");
		}
	}
}