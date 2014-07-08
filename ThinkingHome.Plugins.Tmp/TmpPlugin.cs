using System;
using System.IO;
using System.Media;
using System.Net.Mail;
using System.Threading;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.AlarmClock;
using ThinkingHome.Plugins.Audio;
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

		[ScriptCommand("playDoorBell")]
		public void DoorBell()
		{
			Context.GetPlugin<AudioPlugin>().Play(TmpResources.doorbell, true);
		}

		[ScriptCommand("stopDoorBell")]
		public void StopDoorBell()
		{
			Context.GetPlugin<AudioPlugin>().Stop();
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



		#endregion
	}
}
