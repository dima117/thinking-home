using System.Data;
using ECM7.Migrator.Framework;
using ForeignKeyConstraint = ECM7.Migrator.Framework.ForeignKeyConstraint;

namespace ThinkingHome.Plugins.Scripts.Data
{
	[Migration(1)]
	public class Migration01UserScriptTable : Migration
	{
		public override void Apply()
		{
			Database.AddTable("Scripts_UserScript",
				new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey, "newid()"),
				new Column("Name", DbType.String.WithSize(200), ColumnProperty.NotNull),
				new Column("Body", DbType.String.WithSize(int.MaxValue), ColumnProperty.NotNull)
			);
		}

		public override void Revert()
		{
			Database.RemoveTable("Scripts_UserScript");
		}
	}

	[Migration(2)]
	public class Migration01EventHandlerTable : Migration
	{
		public override void Apply()
		{
			Database.AddTable("Scripts_EventHandler",
				new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey, "newid()"),
				new Column("EventAlias", DbType.String.WithSize(200), ColumnProperty.NotNull),
				new Column("UserScriptId", DbType.Guid, ColumnProperty.NotNull)
			);

			Database.AddForeignKey("FK_Scripts_EventHandler_UserScriptId",
				"Scripts_EventHandler", "UserScriptId", "Scripts_UserScript", "Id", ForeignKeyConstraint.Cascade);
		}

		public override void Revert()
		{
			Database.RemoveTable("Scripts_EventHandler");
		}
	}
}