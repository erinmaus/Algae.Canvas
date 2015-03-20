using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// Defines a 2D texture.
	/// </summary>
	interface ITexture2D : ITexture, IDisposable
	{
		int Width
		{
			get;
		}

		int Height
		{
			get;
		}
	}
}
