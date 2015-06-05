using System.Data;
using ECM7.Migrator.Framework;
using ForeignKeyConstraint = ECM7.Migrator.Framework.ForeignKeyConstraint;

namespace ThinkingHome.Plugins.UniUI.Migrations
{
	[Migration(2)]
	public class Migration02_Widget : Migration
	{
		public override void Apply()
		{
			Database.AddTable("UniUI_Widget",
				new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey, "newid()"),
				new Column("DashboardId", DbType.Guid, ColumnProperty.NotNull),
				new Column("TypeAlias", DbType.String, ColumnProperty.NotNull),
				new Column("DisplayName", DbType.String, ColumnProperty.NotNull),
				new Column("SortOrder", DbType.Int32, ColumnProperty.NotNull, 0)
			);

			Database.AddForeignKey("FK_UniUI_Widget_DashboardId",
				"UniUI_Widget", "DashboardId", "UniUI_Dashboard", "Id", ForeignKeyConstraint.Cascade);
		}

		public override void Revert()
		{
			Database.RemoveTable("UniUI_Widget");
		}
	}
}
