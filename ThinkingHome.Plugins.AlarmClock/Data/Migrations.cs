using System.Data;
using ECM7.Migrator.Framework;

namespace ThinkingHome.Plugins.AlarmClock.Data
{
	[Migration(1)]
	public class Migration01UserScriptTable : Migration
	{
		public override void Apply()
		{
			Database.AddTable("AlarmClock_AlarmTime",
				new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey, "newid()"),
				new Column("Name", DbType.String.WithSize(200), ColumnProperty.NotNull),
				new Column("Hours", DbType.Int32, ColumnProperty.NotNull),
				new Column("Minutes", DbType.Int32, ColumnProperty.NotNull),
				new Column("Enabled", DbType.Boolean, ColumnProperty.NotNull, false)
			);
		}

		public override void Revert()
		{
			Database.RemoveTable("AlarmClock_AlarmTime");
		}
	}
}