using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Platform
{
	/// <summary>
	/// A class that provides mouse services.
	/// </summary>
	class AlgaeMouse : Mouse, IAlgaeEventProvider
	{
		/// <summary>
		/// Gets the keyboard event source.
		/// </summary>
		public IntPtr EventSource
		{
			get
			{
				if (!IsInitialized)
					throw new InvalidOperationException("Keyboard is not initialized.");

				return AllegroMethods.al_get_keyboard_event_source();
			}
		}

		/// <summary>
		/// Initializes the mouse to a default state.
		/// </summary>
		public override void Initialize()
		{
			if (IsInitialized)
				throw new InitializationException("Mouse is already initialized.");

			if (AllegroMethods.al_install_mouse() == 0)
				throw new InitializationException("Could not initialize mouse.");

			IsInitialized = true;
		}

		/// <summary>
		/// Disposes of all resources allocated by the mouse.
		/// </summary>
		public override void Dispose()
		{
			if (IsInitialized)
			{
				AllegroMethods.al_uninstall_mouse();

				IsInitialized = false;
			}
		}
	}
}
