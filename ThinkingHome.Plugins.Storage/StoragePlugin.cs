using System;
using NHibernate;
using NHibernate.Mapping.ByCode;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.Utils;

namespace ThinkingHome.Plugins.Storage
{
	public class StoragePlugin : Plugin
	{
		public override void InitDbModel(ModelMapper mapper)
		{
			base.InitDbModel(mapper);

			mapper.Class<Item>(cfg => cfg.Table("Storage_Item"));
		}

		#region get

		public int? GetInteger(string id, ISession session = null)
		{
			return GetValue(session, id, x => x.IntegerValue);
		}

		public DateTime? GetDateTime(string id, ISession session = null)
		{
			return GetValue(session, id, x => x.DateTimeValue);
		}

		public string GetString(string id, ISession session = null)
		{
			return GetValue(session, id, x => x.StringValue);
		}

		public T GetObject<T>(string id, ISession session = null) where T : class
		{
			return GetValue(session, id, ParseObject<T>);
		}

		#endregion

		#region set

		public void SetInteger(string id, int? value, ISession session = null)
		{
			SetValue(session, id, x => x.IntegerValue = value);
		}

		public void SetDateTime(string id, DateTime? value, ISession session = null)
		{
			SetValue(session, id, x => x.DateTimeValue = value);
		}

		public void SetString(string id, string value, ISession session = null)
		{
			SetValue(session, id, x => x.StringValue = value);
		}

		public void SetObject<T>(string id, T value, ISession session = null) where T : class
		{
			SetValue(session, id, x => x.ObjectValue = value.ToJson());
		}

		#endregion

		#region private

		private void SetValue(ISession session, string id, Action<Item> setter)
		{
			if (session == null)
			{
				using (var db2 = Context.OpenSession())
				{
					SetValue(db2, id, setter);
					db2.Flush();
				}
			}
			else
			{
				if (setter != null)
				{
					var item = session.Get<Item>(id) ?? new Item { Id = id };
					setter(item);
					session.SaveOrUpdate(item);
				}
			}
		}

		private T GetValue<T>(ISession session, string id, Func<Item, T> extractor)
		{
			if (extractor == null)
			{
				return default(T);
			}

			if (session == null)
			{
				using (var db2 = Context.OpenSession())
				{
					return GetValue(db2, id, extractor);
				}
			}

			var obj = session.Get<Item>(id);
			return obj == null ? default(T) : extractor(obj);
		}

		private static T ParseObject<T>(Item item) where T : class
		{
			return item == null ? null : Extensions.FromJson<T>(item.ObjectValue);
		}

		#endregion
	}
}
