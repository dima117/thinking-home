using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.NooApi.Commands;
using ThinkingHome.Plugins.NooLite;

/* TODO:
 * Вынести адрес API в конфиг, если возможно
 */


namespace ThinkingHome.Plugins.NooApi
{
	[Plugin]
	class NooApiPlugin : PluginBase
	{
		#region api
		object moLock = new object(); // Проверить, нужно ли его освободить

		[HttpCommand("/api/noolite")]
		public object GetChannel(HttpRequestParams request)
		{
			int? bright  = request.GetInt32("br");
			int? channel = request.GetInt32("ch");
			int? cmdnum = request.GetInt32("cmd");
			string cmd = request.GetString("cmd");

			if (channel == null)
				return "ERROR - channel not set";

			// Парсим команду, выходим если ничего не нашлось.
			APICommand command;
			try
			{
				command = (APICommand)Enum.Parse(typeof(APICommand), cmd, true);
				if (cmdnum != null)
				{
					if (!Enum.IsDefined(typeof(APICommand), cmdnum))
						return "ERROR - Unrecognized command";
					command = (APICommand)cmdnum;
				}
			}
			catch
			{
				return "ERROR - Unrecognized command";
			}


			var NooLite = new ThinkingHome.Plugins.NooLite.NooLitePlugin(); // Проверить, нужно ли его освободить
			switch (command)
			{
				// Команды без дополнительных аргументов
				case APICommand.On:
				case APICommand.Off:
				case APICommand.Toggle:
				case APICommand.Preset:
				case APICommand.PresetRec:
				case APICommand.RegUp:
				case APICommand.RegDown:
				case APICommand.RegToggle:
				case APICommand.RegStop:
				case APICommand.Bind:
				case APICommand.UnBind:
					Logger.Debug("nooAPI {1} command received, channel = {0}", channel, Enum.GetName(typeof(APICommand), command));
					if (channel != null)
						lock (moLock)
						{
							NooLite.SendCommand((int)command, (int)channel, 0);
						}
					return "OK";

				// Установка яркости (формат в процентах)
				case APICommand.Set:
					Logger.Debug("nooAPI Set command received, channel = {0}, value = {1}", channel, bright);
					if (channel != null && bright != null)
						lock (moLock)
						{
							int value = (int)(155*bright/100);
							NooLite.SendCommand((int)APICommand.Set, (int)channel, value);
						}
					return "OK";

				default:
					return "UNSUPPORTED";
			}
		}

		#endregion
	}
}
