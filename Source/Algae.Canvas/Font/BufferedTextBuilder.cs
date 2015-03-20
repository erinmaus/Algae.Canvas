using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// Performs the complex operations of buffering text...
	/// </summary>
	public abstract class BufferedTextBuilder
	{
		/// <summary>
		/// Prepares a buffer.
		/// </summary>
		/// <param name="buffer">The buffer to prepare.</param>
		public virtual void Prepare(BufferedText buffer)
		{
			buffer.Reset();
		}

		/// <summary>
		/// Buffers text.
		/// </summary>
		/// <param name="buffer">The buffered text instance to store the data in.</param>
		/// <param name="text">The text.</param>
		public abstract void BufferText(BufferedText buffer, string text);

		/// <summary>
		/// Resets the state of the text builder.
		/// </summary>
		public abstract void Reset();
	}
}
