using System.Data;
using ECM7.Migrator.Framework;

namespace ThinkingHome.Plugins.UniUI.Migrations
{
	[Migration(1)]
	public class Migration01_Dashboard : Migration
	{
		public override void Apply()
		{
			Database.AddTable("UniUI_Dashboard",
				new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey, "newid()"),
				new Column("Title", DbType.String, ColumnProperty.NotNull),
				new Column("SortOrder", DbType.Int32, ColumnProperty.NotNull, 0)
			);
		}

		public override void Revert()
		{
			Database.RemoveTable("UniUI_Dashboard");
		}
	}
}
