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
 * Обработать знак и пределы чисел
 */


namespace ThinkingHome.Plugins.NooApi
{
	[Plugin]
	class NooApiPlugin : PluginBase
	{
		#region api
		object moLock = new object();

		[HttpCommand("/api/noolite")]
		public object GetChannel(HttpRequestParams request)
		{
			int? channel = request.GetRequiredInt32("ch");
			int? cmdnum = request.GetInt32("cmd");
			string cmd = request.GetString("cmd");

			// Парсим команду, выходим если ничего не нашлось.
			APICommand command;
			if (cmdnum != null)
			{
				if (!Enum.IsDefined(typeof(APICommand), cmdnum))
					throw new NullReferenceException("ERROR - Unrecognized command");
				command = (APICommand)cmdnum;
			}
			else
			{
				try
				{
					command = (APICommand)Enum.Parse(typeof(APICommand), cmd, true);
				}
				catch
				{
					throw new NullReferenceException("ERROR - Unrecognized command");
				}
			}


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
				case APICommand.RollColor:
				case APICommand.SwitchColor:
				case APICommand.SwitchMode:
				case APICommand.SwitchSpeed:
					Logger.Debug("nooAPI {1} command received, channel = {0}", channel, Enum.GetName(typeof(APICommand), command));
					if (channel != null)
						lock (moLock)
						{
							Context.GetPlugin<NooLitePlugin>().SendCommand((int)command, (int)channel, 0);
						}
					return "OK";

				// Установка яркости (формат в процентах и через аргументы d0, d1, d2)
				case APICommand.Set:
					Logger.Debug("nooAPI Set command received, channel = {0}", channel);
					int? brightness = request.GetInt32("br");
					if (brightness != null)
						lock (moLock)
						{
							int value = (int)(155*brightness/100);
							Context.GetPlugin<NooLitePlugin>().SendCommand((int)APICommand.Set, (int)channel, value);
						}
					else
					{
						int? format = request.GetRequiredInt32("fmt");
						switch (format)
						{
							case 1:
								int data = request.GetRequiredInt32("d0");
								lock (moLock)
								{
									Context.GetPlugin<NooLitePlugin>().SendCommand((int)APICommand.Set, (int)channel, data);
								}
								break;
							case 3:
								int d0 = request.GetRequiredInt32("d0");
								int d1 = request.GetRequiredInt32("d1");
								int d2 = request.GetRequiredInt32("d2");
								lock (moLock)
								{
									Context.GetPlugin<NooLitePlugin>().SendLedCommand((int)APICommand.Set, (int)channel, d0, d1, d2);
								}
								break;
							default:
								throw new NullReferenceException("Incorrect format value");
						}
					}
					return "OK";

				default:
					return "UNSUPPORTED";
			}
		}
		#endregion
	}
}
