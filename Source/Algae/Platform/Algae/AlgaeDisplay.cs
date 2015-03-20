using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CommaExcess.Algae.Platform
{
	/// <summary>
	/// A class that implements display routines.
	/// </summary>
	class AlgaeDisplay : Display, IAlgaeEventProvider
	{
		bool dirtySettings = false;
		DisplaySettings settings = new DisplaySettings(800, 600, false, 0);

		IntPtr display;

		/// <summary>
		/// Gets the event source associated with the Algae display.
		/// </summary>
		public IntPtr EventSource
		{
			get { return AllegroMethods.al_get_display_event_source(display); }
		}

		/// <summary>
		/// Gets or sets the display settings for the display.
		/// </summary>
		/// <remarks>
		/// Setting a different value, when the display is initialized, will cause the new settings to be applied.
		/// </remarks>
		public override DisplaySettings Settings
		{
			get
			{
				if (dirtySettings)
				{
					int w = AllegroMethods.al_get_display_width(display);
					int h = AllegroMethods.al_get_display_height(display);
					bool fullscreen = ((AllegroDisplayFlags)AllegroMethods.al_get_display_flags(display) & AllegroDisplayFlags.ALLEGRO_FULLSCREEN_WINDOW)
						== AllegroDisplayFlags.ALLEGRO_FULLSCREEN_WINDOW;
					bool isResizable = ((AllegroDisplayFlags)AllegroMethods.al_get_display_flags(display) & AllegroDisplayFlags.ALLEGRO_RESIZABLE)
						== AllegroDisplayFlags.ALLEGRO_RESIZABLE;
					int samples = AllegroMethods.al_get_display_option(display, (int)AllegroDisplayOptions.ALLEGRO_SAMPLES);

					settings = new DisplaySettings(w, h, fullscreen, samples);
					dirtySettings = false;
				}
				
				return settings;
			}
			set
			{
				if (IsInitialized)
				{
					AllegroMethods.al_set_display_flag(display, (int)AllegroDisplayFlags.ALLEGRO_FULLSCREEN_WINDOW, value.IsFullscreen ? 1 : 0);
					AllegroMethods.al_resize_display(display, value.Width, value.Height);

#if WINDOWS
					if (!value.IsFullscreen)
					{
						CenterDisplay();
					}
#endif

					dirtySettings = true;
				}
				else
				{
					settings = value;
					dirtySettings = false;
				}
			}
		}

		/// <summary>
		/// Flips the buffers.
		/// </summary>
		public override void Flip()
		{
			if (IsInitialized)
				AllegroMethods.al_flip_display();
		}

		/// <summary>
		/// Initializes the display to a default state.
		/// </summary>
		public override void Initialize()
		{
			if (IsInitialized)
				throw new InitializationException("Display is already initialized.");

			if (settings.Samples > 0)
			{
				AllegroMethods.al_set_new_display_option((int)AllegroDisplayOptions.ALLEGRO_SAMPLE_BUFFERS, 1, (int)AllegroDisplayImportance.ALLEGRO_SUGGEST);
				AllegroMethods.al_set_new_display_option((int)AllegroDisplayOptions.ALLEGRO_SAMPLES, settings.Samples, (int)AllegroDisplayImportance.ALLEGRO_SUGGEST);
			}

			AllegroMethods.al_set_new_display_option((int)AllegroDisplayOptions.ALLEGRO_STENCIL_SIZE, 1, (int)AllegroDisplayImportance.ALLEGRO_SUGGEST);

			int flags = (int)(AllegroDisplayFlags.ALLEGRO_OPENGL | AllegroDisplayFlags.ALLEGRO_OPENGL_3_0 | AllegroDisplayFlags.ALLEGRO_OPENGL_FORWARD_COMPATIBLE);

			if (settings.IsFullscreen)
				flags |= (int)AllegroDisplayFlags.ALLEGRO_FULLSCREEN_WINDOW;

			if (settings.IsResizable)
				flags |= (int)AllegroDisplayFlags.ALLEGRO_RESIZABLE;

			AllegroMethods.al_set_new_display_flags(flags);

			display = AllegroMethods.al_create_display(settings.Width, settings.Height);

			if (display == IntPtr.Zero)
			{
				throw new InitializationException("Could not create underlying display.");
			}
#if WINDOWS
			if (!settings.IsFullscreen)
				CenterDisplay();
#endif

			dirtySettings = true;
			IsInitialized = true;
		}

#if WINDOWS
		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int Left, Top, Right, Bottom;

			public int X
			{
				get { return Left; }
				set { Right -= (Left - value); Left = value; }
			}

			public int Y
			{
				get { return Top; }
				set { Bottom -= (Top - value); Top = value; }
			}

			public int Height
			{
				get { return Bottom - Top; }
				set { Bottom = value + Top; }
			}

			public int Width
			{
				get { return Right - Left; }
				set { Right = value + Left; }
			}
		}

		[DllImport("user32.dll")]
		static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

		[DllImport("user32.dll")]
		static extern bool SystemParametersInfo(int action, int ui, out RECT param, int w);

		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

		public void CenterDisplay()
		{
			IntPtr handle = GetHandle();

			RECT displayRect, desktopRect;
			GetWindowRect(handle, out displayRect);
			SystemParametersInfo(0x0030 /* SPI_GETWORKAREA */, 0, out desktopRect, 0);

			int x = desktopRect.X + (desktopRect.Width - displayRect.Width) / 2;
			int y = desktopRect.Y + (desktopRect.Height - displayRect.Height) / 2;

			MoveWindow(handle, x, y, displayRect.Width, displayRect.Height, true);
		}
#endif

		/// <summary>
		/// Disposes of all resources allocated by the display.
		/// </summary>
		public override void Dispose()
		{
			if (display != IntPtr.Zero)
			{
				AllegroMethods.al_destroy_display(display);
			}
		}

		/// <summary>
		/// Gets the handle to the display.
		/// </summary>
		/// <returns>A handle.</returns>
		public override IntPtr GetHandle()
		{
#if WINDOWS
			return AllegroMethods.al_get_win_window_handle(display);
#else
			return IntPtr.Zero;
#endif
		}

		/// <summary>
		/// Gets the display handle.
		/// </summary>
		/// <returns>Returns the handle on supported platforms, IntPtr.Zero otherwise.</returns>
		public override IntPtr GetDisplayHandle()
		{
			return display;
		}

		public void MarkDirty()
		{
			dirtySettings = true;
		}
	}
}
