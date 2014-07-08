using NAudio.Wave;

namespace ThinkingHome.Plugins.Audio.Internal
{

	/// <summary>
	/// Stream for looping playback
	/// </summary>
	public class LoopStream : WaveStream
	{
		readonly WaveStream sourceStream;

		/// <summary>
		/// Creates a new Loop stream
		/// </summary>
		/// <param name="sourceStream">The stream to read from. Note: the Read method of this stream should return 0 when it reaches the end
		///     or else we will not loop to the start again.</param>
		/// <param name="loop"></param>
		public LoopStream(WaveStream sourceStream, bool loop = false)
		{
			this.sourceStream = sourceStream;
			EnableLooping = loop;
		}

		public bool EnableLooping { get; set; }

		public override WaveFormat WaveFormat
		{
			get { return sourceStream.WaveFormat; }
		}

		public override long Length
		{
			get { return sourceStream.Length; }
		}

		public override long Position
		{
			get { return sourceStream.Position; }
			set { sourceStream.Position = value; }
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			int totalBytesRead = 0;

			while (totalBytesRead < count)
			{
				int bytesRead = sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
				if (bytesRead == 0)
				{
					if (sourceStream.Position == 0 || !EnableLooping)
					{
						// something wrong with the source stream
						break;
					}
					// loop
					sourceStream.Position = 0;
				}
				totalBytesRead += bytesRead;
			}
			return totalBytesRead;
		}
	}
}
