using System.Data;
using ECM7.Migrator.Framework;
using ForeignKeyConstraint = ECM7.Migrator.Framework.ForeignKeyConstraint;

namespace ThinkingHome.Plugins.UniUI.Migrations
{
	[Migration(3)]
	public class Migration03_WidgetParameter : Migration
	{
		public override void Apply()
		{
			Database.AddTable("UniUI_WidgetParameter",
				new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey, "newid()"),
				new Column("WidgetId", DbType.Guid, ColumnProperty.NotNull),
				new Column("ParameterKey", DbType.Guid, ColumnProperty.NotNull),
				new Column("ValueGuid", DbType.Guid, ColumnProperty.Null),
				new Column("ValueString", DbType.String.WithSize(int.MaxValue), ColumnProperty.Null),
				new Column("ValueInt", DbType.Int32, ColumnProperty.Null)
			);

			Database.AddUniqueConstraint("UK_UniUI_WidgetParameter_ParameterKey_WidgetId",
				"UniUI_WidgetParameter", "ParameterKey", "WidgetId");

			Database.AddForeignKey("FK_UniUI_WidgetParameter_WidgetId",
				"UniUI_WidgetParameter", "WidgetId", "UniUI_Widget", "Id", ForeignKeyConstraint.Cascade);
		}

		public override void Revert()
		{
			Database.RemoveTable("UniUI_WidgetParameter");
		}
	}
}
