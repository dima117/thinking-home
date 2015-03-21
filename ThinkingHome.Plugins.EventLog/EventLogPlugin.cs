using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Mapping.ByCode;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.EventLog.Data;

namespace ThinkingHome.Plugins.EventLog
{
	[Plugin]
	public class EventLogPlugin : PluginBase
	{
		private Dictionary<string, Guid> tagAliases = new Dictionary<string, Guid>();

		/*
		 *	todo: план работ
			3 таблицы: события, данные событий (*), метки событий
		 *  событие: id, timestamp, data
		 *  данные событий - таблицы плагинов с полем-ссылкой на событие (можно прикреплять коллекцию объектов к одному событию)
		 *  тэги: GUID + alias
	 
		 * 
		 * команды:
		 *		- записать событие с данными и метками и строкой данных
		 *		- получить события: фильтр по времени, фильтр по типу данных, фильтр по меткам
		 *			(события за период, события со всеми заданными метками, последнее событие - через linq,
		 *			последнее событие за последний период (например, час), объекты данных с такими же фильтрами)
		 *			
		 * скриптовые команды: записать событие с метками (переаются строки) и общим типом данных - строка
		 * 
		 * 
		 * todo: продумать примеры использования!!
		 * host.executeMethod("saveEvent", ["time", "test"], "example event data");
		 * 
		 * var eventLog = Context.GetPlugin<EventLogPlugin>();
		 * 
		 * var event1 = eventLog.AddEvent(data, "label1", "label2");
		 *
		 * var myData = new MyDataType { LogItem: event1 } 
		 * session.Save(myData);
		 * 
		 */

		public override void InitDbModel(ModelMapper mapper)
		{
			base.InitDbModel(mapper);
			mapper.Class<LogItem>(cfg => cfg.Table("EventLog_LogItem"));
		}

		private LogItem AddEventInternal(ISession session, string data, string[] tags)
		{
			var now = DateTime.Now;

			var item = new LogItem
			{
				Id = Guid.NewGuid(), 
				Timestamp = now,
				Data = data
			};

			session.Save(item);

			return item;
		}

		public LogItem AddEvent(string data, params string[] tags)
		{
			using (var session = Context.OpenSession())
			{
				return AddEventInternal(session, data, tags);
			}
		}

		public LogItem AddEvent(ISession session, string data, params string[] tags)
		{
			return AddEventInternal(session,data, tags);
		}
	}
}
