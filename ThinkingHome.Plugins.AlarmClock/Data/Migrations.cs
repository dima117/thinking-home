using System.Data;
using ECM7.Migrator.Framework;
using ForeignKeyConstraint = ECM7.Migrator.Framework.ForeignKeyConstraint;

[assembly: MigrationAssembly("ThinkingHome.Plugins.AlarmClock")]

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

	[Migration(2)]
	public class Migration02PlaySoundAndScriptId : Migration
	{
		public override void Apply()
		{
			Database.AddColumn("AlarmClock_AlarmTime",
				new Column("UserScriptId", DbType.Guid, ColumnProperty.Null)
			);

			Database.AddForeignKey("AlarmClock_AlarmTime_UserScriptId",
				"AlarmClock_AlarmTime", "UserScriptId", "Scripts_UserScript", "Id", ForeignKeyConstraint.Cascade);

		}

		public override void Revert()
		{
			Database.RemoveConstraint("AlarmClock_AlarmTime", "AlarmClock_AlarmTime_UserScriptId");
			Database.RemoveColumn("AlarmClock_AlarmTime", "UserScriptId");
		}
	}
}