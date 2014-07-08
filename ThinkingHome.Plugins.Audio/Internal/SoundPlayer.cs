using System.IO;
using NAudio.Wave;
using NLog;

namespace ThinkingHome.Plugins.Audio.Internal
{
	internal class SoundPlayer : ISoundPlayer
	{
		private readonly Logger logger;
		private readonly WaveOut waveOut = new WaveOut();

		public void Dispose()
		{
			logger.Info("dispose");
			waveOut.Dispose();
		}

		public SoundPlayer(Logger logger, Stream stream, bool loop = false)
		{	
			this.logger = logger;
			var reader = new WaveFileReader(stream);
			var loopStream = new LoopStream(reader, loop);
			
			logger.Info("init");
			waveOut.Init(loopStream);
		}

		public void Play()
		{
			logger.Info("play");
			waveOut.Play();
		}

		public void Stop()
		{
			logger.Info("stop");
			waveOut.Stop();
		}
	}
}
