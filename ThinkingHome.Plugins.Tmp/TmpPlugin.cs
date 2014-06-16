using System;
using System.IO;
using System.Media;
using System.Net.Mail;
using System.Threading;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.AlarmClock;
using ThinkingHome.Plugins.DlinkDCS930L;
using ThinkingHome.Plugins.Scripts;
using ThinkingHome.Plugins.WebUI.Attributes;
using ThinkingHome.Plugins.WebUI.Model;

namespace ThinkingHome.Plugins.Tmp
{
	[Plugin]
	public class TmpPlugin : Plugin
	{
		#region camera
		[ScriptCommand("sendDCS930Image")]
		public void SendImage(string to, string subject)
		{
			var bytes = Context.GetPlugin<DlinkDCS930LPlugin>().GetImage();

			var fileName = string.Format("{0:yyyyMMddHHmmss}.jpg", DateTime.Now);
			var path = Path.Combine(@"D:\", fileName);
			File.WriteAllBytes(path, bytes);

			Send(to, subject, fileName, bytes);
		}

		#endregion

		#region mail

		public void Send(string to, string subject, string fileName, byte[] bytes)
		{
			var message = new MailMessage
			{
				To = { to },
				Subject = subject
			};

			if (bytes != null)
			{
				var attachment = new Attachment(new MemoryStream(bytes), fileName);
				message.Attachments.Add(attachment);
			}

			ThreadPool.QueueUserWorkItem(state =>
			{
				try
				{
					SendEmailAwaitable(message);
				}
				catch (Exception ex)
				{
					Logger.ErrorException("mail exception", ex);
				}
			});
		}

		private void SendEmailAwaitable(MailMessage message)
		{
			using (var smtpClient = new SmtpClient())
			{
				smtpClient.Send(message);
			}
		}

		#endregion

		#region sounds

		private SoundPlayer doorBell;

		public override void Start()
		{
			doorBell = new SoundPlayer(TmpResources.doorbell);
		}

		public override void Stop()
		{
			doorBell.Dispose();
		}

		[ScriptCommand("playDoorBell")]
		public void DoorBell()
		{
			doorBell.Play();
		}

		#endregion

		#region test

		[OnAlarmStarted]
		public void Asfafasfasg1(Guid id)
		{
			Logger.Info("test1: {0}", id);
		}

		[OnAlarmStarted]
		public void Asfafasfasg2(Guid id)
		{
			Logger.Info("test2: {0}", id);
		}

		[Tile("0D03FD41-167A-4FFC-9F20-6ED51A3A2A84", "Tmp", "/api/alarm-clock/list")]
		public void TestTile(TileModel tile)
		{
			tile.content = "Это тестовый контент!";
		}

		[Tile("E6467D12-283A-4B07-B807-2FA2A7555ED4", "Weather", "/api/alarm-clock/list")]
		public void TestWeatherTile(TileModel tile)
		{
			tile.content = "10:00 — 5°C\n16:00 —  6°C\n22:00 —  4°C\n04:00 —  3°C";
		}
		[Tile("1DF06D69-4603-49C8-AA96-FF77BC0EB6D2", "News", "/api/alarm-clock/list", IsWide = true)]
		public void TestNewsTile(TileModel tile)
		{
			tile.content = "NASA призвало Россию продлить сотрудничество по МКС\nНа спутнике Плутона мог существовать подземный океан";
		}

		#endregion
	}
}
