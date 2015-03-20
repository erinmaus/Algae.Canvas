using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae
{
	/// <summary>
	/// A structure that holds settings used by a display.
	/// </summary>
	public struct DisplaySettings
	{
		/// <summary>
		/// Gets or sets the width of a display.
		/// </summary>
		public int Width;

		/// <summary>
		/// Gets or sets the height of a display.
		/// </summary>
		public int Height;

		/// <summary>
		/// Gets or sets a value indicating if the display should be fullscreen or not.
		/// </summary>
		public bool IsFullscreen;

		/// <summary>
		/// Gets or sets how many samples used for multisampling.
		/// </summary>
		public int Samples;

		/// <summary>
		/// Gets or sets a value indicating if the display can be resized or not.
		/// </summary>
		/// <remarks>This is not necessarily supported.</remarks>
		public bool IsResizable;

		/// <summary>
		/// Creates a display settings structure with the provided values.
		/// </summary>
		/// <param name="width">The width of the display.</param>
		/// <param name="height">The height of the display.</param>
		/// <param name="isFullscreen">Whether or not the display should be fullscreen.</param>
		/// <param name="samples">The amount of samples to use for multisampling.</param>
		/// <param name="isResizable">Whether or not the display is resizable.</param>
		public DisplaySettings(int width, int height, bool isFullscreen = false, int samples = 0, bool isResizable = false)
		{
			Width = width;
			Height = height;
			IsFullscreen = isFullscreen;
			Samples = samples;
			IsResizable = isResizable;
		}
	}
}
