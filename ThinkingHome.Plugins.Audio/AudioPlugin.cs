using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Audio.Internal;

namespace ThinkingHome.Plugins.Audio
{
	[Plugin]
	public class AudioPlugin : Plugin
	{
		private WaveOut waveOut;
		private readonly object lockObject = new object();

		public override void InitPlugin()
		{
			waveOut = new WaveOut();
		}

		public override void StopPlugin()
		{
			waveOut.Dispose();
			waveOut = null;
		}

		public void Play(Stream stream, bool loop = false)
		{
			lock (lockObject)
			{
				if (waveOut.PlaybackState != PlaybackState.Stopped)
				{
					waveOut.Stop();
				}

				var reader = new WaveFileReader(stream);
				var loopStream = new LoopStream(reader, loop);
				waveOut = new WaveOut();
				waveOut.Init(loopStream);
				waveOut.Play();
			}
		}

		public void Stop()
		{
			lock (lockObject)
			{
				waveOut.Stop();
			}
		}
	}
}
