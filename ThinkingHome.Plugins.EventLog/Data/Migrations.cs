using System.Data;
using ECM7.Migrator.Framework;
using ForeignKeyConstraint = ECM7.Migrator.Framework.ForeignKeyConstraint;

[assembly: MigrationAssembly("ThinkingHome.Plugins.EventLog")]

namespace ThinkingHome.Plugins.EventLog.Data
{
	[Migration(1)]
	public class Migration01LogItemTable : Migration
	{
		public override void Apply()
		{
			Database.AddTable("EventLog_LogItem",
				new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey, "newid()"),
				new Column("Timestamp", DbType.DateTime, ColumnProperty.NotNull)
			);
		}

		public override void Revert()
		{
			Database.RemoveTable("EventLog_LogItem");
		}
	}

	[Migration(2)]
	public class Migration02LogItemDataTable : Migration
	{
		public override void Apply()
		{
			Database.AddTable("EventLog_LogItemData",
				new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey),
				new Column("LogItemId", DbType.Guid, ColumnProperty.NotNull),
				new Column("Data", DbType.String.WithSize(int.MaxValue), ColumnProperty.Null)
				
			);

			Database.AddForeignKey("FK_EventLog_LogItemData_LogItemId",
				"EventLog_LogItemData", "LogItemId", "EventLog_LogItem", "Id", ForeignKeyConstraint.Cascade);
		}

		public override void Revert()
		{
			Database.RemoveConstraint("EventLog_LogItemData", "FK_EventLog_LogItemData_LogItemId");
			Database.RemoveTable("EventLog_LogItemData");
		}
	}
}