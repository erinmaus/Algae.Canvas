using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// A structure that represents a viewport.
	/// </summary>
	public struct Viewport
	{
		/// <summary>
		/// The viewport's X component.
		/// </summary>
		public int X;

		/// <summary>
		/// The viewport's Y component.
		/// </summary>
		public int Y;

		/// <summary>
		/// The viewport's width.
		/// </summary>
		public int Width;

		/// <summary>
		/// The viewport's height.
		/// </summary>
		public int Height;

		/// <summary>
		/// Constructs a viewport.
		/// </summary>
		/// <param name="x">The X component.</param>
		/// <param name="y">The Y component.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		public Viewport(int x, int y, int width, int height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		/// <summary>
		/// Constructs a viewport.
		/// </summary>
		/// <param name="viewport">The components of the viewport.</param>
		public Viewport(int[] viewport)
		{
			X = viewport[0];
			Y = viewport[1];
			Width = viewport[2];
			Height = viewport[3];
		}
	}
}
