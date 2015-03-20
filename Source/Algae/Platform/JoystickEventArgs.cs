using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Platform
{
	/// <summary>
	/// A joystick input event.
	/// </summary>
	public class JoystickEventArgs : EventArgs
	{
		/// <summary>
		/// The handle that caused the event.
		/// </summary>
		public JoystickHandle Handle
		{
			get;
			set;
		}

		/// <summary>
		/// The stick that moved during the event.
		/// </summary>
		public int Stick
		{
			get;
			set;
		}

		/// <summary>
		/// The axis of movement.
		/// </summary>
		public int Axis
		{
			get;
			set;
		}

		/// <summary>
		/// The position of the axis during the event.
		/// </summary>
		public float Position
		{
			get;
			set;
		}

		/// <summary>
		/// The button that caused the event.
		/// </summary>
		public int Button
		{
			get;
			set;
		}
	}
}
