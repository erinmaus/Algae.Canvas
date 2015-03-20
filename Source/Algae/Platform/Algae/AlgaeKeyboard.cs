using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Platform
{
	/// <summary>
	/// A class that provides keyboard services.
	/// </summary>
	class AlgaeKeyboard : Keyboard, IAlgaeEventProvider
	{
		/// <summary>
		/// Gets the mouse event source.
		/// </summary>
		public IntPtr EventSource
		{
			get
			{
				if (!IsInitialized)
					throw new InvalidOperationException("Mouse is not initialized.");

				return AllegroMethods.al_get_mouse_event_source();
			}
		}

		/// <summary>
		/// Initializes the keyboard to a default state.
		/// </summary>
		public override void Initialize()
		{
			if (IsInitialized)
				throw new InitializationException("Keyboard is already initialized.");

			if (AllegroMethods.al_install_keyboard() == 0)
				throw new InitializationException("Could not initialize keyboard.");

			IsInitialized = true;
		}

		/// <summary>
		/// Disposes of all resources allocated by the keyboard.
		/// </summary>
		public override void Dispose()
		{
			if (IsInitialized)
			{
				AllegroMethods.al_uninstall_keyboard();

				IsInitialized = false;
			}
		}
	}
}
