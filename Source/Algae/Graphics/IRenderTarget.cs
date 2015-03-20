using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	interface IRenderTarget : IDisposable
	{
		ITexture2D Depth
		{
			get;
		}

		ITexture2D this[int index]
		{
			get;
		}

		int ColorAttachments
		{
			get;
		}

		int Width
		{
			get;
		}

		int Height
		{
			get;
		}

		void Bind();
	}
}
