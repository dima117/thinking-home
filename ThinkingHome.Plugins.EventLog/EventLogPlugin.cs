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
		/*
		 *	todo: план работ
			3 таблицы: события, данные событий, метки событий
		 *  событие: id и timestamp
		 *  данные событий - таблицы плагинов с полем-ссылкой на событие (можно прикреплять коллекцию объектов к одному событию)
		 *  метки: GUID
		 *  общий класс данных (с единственным строковым полем)
		 *  в плагине регистр имен меток (заполняется динамически или атрибутами)
	 
		 * 
		 * команды:
		 *		- записать событие с данными и метками (как вариант, разнести добавление данных и меток по времени)
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
		 * var event1 = Context.GetPlugin<EventLogPlugin>().SaveEvent("label1", "label2");
		 * var event2 = Context.GetPlugin<EventLogPlugin>().SaveEvent(data, "label1", "label2");
		 * 
		 * event1.AddLabel(new Guid("8CFA5FCF-ED95-448A-B2D5-21196ECCD7FA"))
		 * event2.AddEventData(myObj)
		*/
		public override void InitDbModel(ModelMapper mapper)
		{
			base.InitDbModel(mapper);
			mapper.Class<LogItem>(cfg => cfg.Table("EventLog_LogItem"));
		}

		public LogItem SaveEvent<TEventData>(ISession session, TEventData data = null)
			where TEventData : class, ILogItemData
		{
			var now = DateTime.Now;

			var item = new LogItem { Id = Guid.NewGuid(), Timestamp = now };
			session.Save(item);

			if (data != null)
			{
				data.LogItem = item;
				session.Save(data);
			}

			session.Flush();

			return item;
		}


	}
}
