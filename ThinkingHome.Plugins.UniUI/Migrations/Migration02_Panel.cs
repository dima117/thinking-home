using ECM7.Migrator.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ForeignKeyConstraint = ECM7.Migrator.Framework.ForeignKeyConstraint;

namespace ThinkingHome.Plugins.UniUI.Migrations
{
    [Migration(2)]
    public class Migration02_Panel : Migration
	{
		public override void Apply()
		{
            Database.AddTable("UniUI_Panel",
				new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey, "newid()"),
				new Column("DashboardId", DbType.Guid, ColumnProperty.NotNull),
				new Column("Title", DbType.String, ColumnProperty.NotNull),
				new Column("SortOrder", DbType.Int32, ColumnProperty.NotNull, 0)
			);

            Database.AddForeignKey("FK_UniUI_Panel_DashboardId",
                "UniUI_Panel", "DashboardId", "UniUI_Dashboard", "Id", ForeignKeyConstraint.Cascade);
		}

		public override void Revert()
		{
            Database.RemoveTable("UniUI_Panel");
		}
	}
}
