using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Platform
{
	/// <summary>
	/// A class that provides joystick services.
	/// </summary>
	class AlgaeJoystick : Joystick, IAlgaeEventProvider
	{
		/// <summary>
		/// Gets the joystick event source.
		/// </summary>
		public IntPtr EventSource
		{
			get
			{
				if (!IsInitialized)
					throw new InvalidOperationException("Joystick is not initialized.");

				return AllegroMethods.al_get_joystick_event_source();
			}
		}

		/// <summary>
		/// Initializes the joystick to a default state.
		/// </summary>
		public override void Initialize()
		{
			if (IsInitialized)
				throw new InitializationException("Joystick is already initialized");

			if (AllegroMethods.al_install_joystick() == 0)
				throw new InitializationException("Could not initialize joystick");

			IsInitialized = true;
		}

		/// <summary>
		/// Disposes of all resources allocated by the joystick.
		/// </summary>
		public override void Dispose()
		{
			if (IsInitialized)
			{
				AllegroMethods.al_uninstall_joystick();

				IsInitialized = false;
			}
		}

		/// <summary>
		/// Gets information about the provided joystick handle.
		/// </summary>
		/// <param name="handle">A handle to a physical joystick.</param>
		/// <returns>The joystick info, if supported. Otherwise, null is returned.</returns>
		public override JoystickInfo GetJoystickInfo(JoystickHandle handle)
		{
			return new AlgaeJoystickInfo(handle);
		}
	}
}
