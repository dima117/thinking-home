using System.Data;
using ECM7.Migrator.Framework;

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
				new Column("Timestamp", DbType.DateTime, ColumnProperty.NotNull),
				new Column("Data", DbType.String.WithSize(int.MaxValue), ColumnProperty.Null)
			);
		}

		public override void Revert()
		{
			Database.RemoveTable("EventLog_LogItem");
		}
	}
}