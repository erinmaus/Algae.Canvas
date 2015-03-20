using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Platform
{
	/// <summary>
	/// A class that handles display routines.
	/// </summary>
	public abstract class Display : IInitializable, IDisposable
	{
		/// <summary>
		/// Gets or sets the display settings for the display.
		/// </summary>
		/// <remarks>
		/// Setting a different value, when the display is initialized, will cause the new settings to be applied.
		/// </remarks>
		public abstract DisplaySettings Settings
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the width of the display, as stored in the local display settings.
		/// </summary>
		public int Width
		{
			get { return Settings.Width; }
		}

		/// <summary>
		/// Gets the height of the display, as stored in the local display settings.
		/// </summary>
		public int Height
		{
			get { return Settings.Height; }
		}

		/// <summary>
		/// Gets the aspect ratio of the display.
		/// </summary>
		public float AspectRatio
		{
			get { return (float)Settings.Width / (float)Settings.Height; }
		}

		/// <summary>
		/// Gets if the display was initialized.
		/// </summary>
		public bool IsInitialized
		{
			get;
			protected set;
		}

		/// <summary>
		/// Flips the buffers.
		/// </summary>
		public abstract void Flip();

		/// <summary>
		/// Initializes the display to a default state.
		/// </summary>
		public abstract void Initialize();

		/// <summary>
		/// Disposes of all resources allocated by the display.
		/// </summary>
		public abstract void Dispose();

		/// <summary>
		/// Gets the handle to the underlying graphics context.
		/// </summary>
		/// <returns>A handle.</returns>
		/// <remarks>
		/// This is not to be confused with the display, which is the window
		/// that the graphics context resides in.
		/// </remarks>
		public abstract IntPtr GetHandle();

		/// <summary>
		/// Gets the display handle.
		/// </summary>
		/// <returns>Returns the handle on supported platforms, IntPtr.Zero otherwise.</returns>
		public abstract IntPtr GetDisplayHandle();

		/// <summary>
		/// Resizes a display.
		/// </summary>
		/// <param name="width">The new width.</param>
		/// <param name="height">The new height.</param>
		public void Resize(int width, int height)
		{
			// Create a copy and apply changes.
			DisplaySettings newSettings = Settings;
			newSettings.Width = width;
			newSettings.Height = height;

			Settings = newSettings;
		}
	}
}
