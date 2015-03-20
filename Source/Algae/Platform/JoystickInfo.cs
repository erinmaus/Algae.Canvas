using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Platform
{
	/// <summary>
	/// Represents information about a joystick.
	/// </summary>
	public abstract class JoystickInfo
	{
		/// <summary>
		/// The handle that this JoystickInfo represents.
		/// </summary>
		public JoystickHandle Handle
		{
			get;
			protected set;
		}

		/// <summary>
		/// Gets a value indicating if the joystick is active or not.
		/// </summary>
		public abstract bool IsActive
		{
			get;
		}

		/// <summary>
		/// Gets the name of the joystick.
		/// </summary>
		public abstract string Name
		{
			get;
		}

		/// <summary>
		/// Gets the name of the provided stick.
		/// </summary>
		/// <param name="stick">The stick.</param>
		/// <returns>The name of the stick.</returns>
		public abstract string GetStickName(int stick);

		/// <summary>
		/// Gets the axis name of the stick.
		/// </summary>
		/// <param name="stick">The stick.</param>
		/// <param name="axis">The axis.</param>
		/// <returns>The axis name.</returns>
		public abstract string GetStickAxisName(int stick, int axis);

		/// <summary>
		/// Gets the index of the provided button.
		/// </summary>
		/// <param name="button">The button.</param>
		/// <returns>The button name.</returns>
		public abstract string GetButtonName(int button);
	}
}
