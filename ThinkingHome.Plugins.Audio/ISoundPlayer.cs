using System;

namespace ThinkingHome.Plugins.Audio
{
	public interface ISoundPlayer : IDisposable
	{
		void Play();
		
		void Stop();
	}
}
