using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Platform
{
	/// <summary>
	/// Represents a joystick.
	/// </summary>
	public abstract class Joystick : IInitializable, IDisposable
	{
		/// <summary>
		/// Gets if the joystick was initialized.
		/// </summary>
		public bool IsInitialized
		{
			get;
			protected set;
		}

		/// <summary>
		/// Initializes the joystick to a default state.
		/// </summary>
		/// <returns></returns>
		public abstract void Initialize();

		/// <summary>
		/// Dispsoses of all resources allocated by the joystick.
		/// </summary>
		public abstract void Dispose();

		/// <summary>
		/// Gets information about the provided joystick handle.
		/// </summary>
		/// <param name="handle">A handle to a physical joystick.</param>
		/// <returns>The joystick info, if supported. Otherwise, null is returned.</returns>
		public abstract JoystickInfo GetJoystickInfo(JoystickHandle handle);
	}
}
