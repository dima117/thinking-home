using System.Data;
using ECM7.Migrator.Framework;

namespace ThinkingHome.Plugins.Storage
{
	[Migration(1)]
	public class Migration01ItemTable : Migration
	{
		public override void Apply()
		{
			Database.AddTable("Storage_Item",
				new Column("Id", DbType.String, ColumnProperty.PrimaryKey),
				new Column("IntegerValue", DbType.Int32, ColumnProperty.Null),
				new Column("DateTimeValue", DbType.DateTime, ColumnProperty.Null),
				new Column("StringValue", DbType.String.WithSize(int.MaxValue), ColumnProperty.Null),
				new Column("ObjectValue", DbType.String.WithSize(int.MaxValue), ColumnProperty.Null)
			);
		}
		
		public override void Revert()
		{
			Database.RemoveTable("Storage_Item");
		}
	}
}
