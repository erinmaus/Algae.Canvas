using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Platform
{
	/// <summary>
	/// An opaque structure that represents a unique joystick.
	/// </summary>
	public struct JoystickHandle
	{
		IntPtr handle;

		/// <summary>
		/// Creates a joystick handle from the provided source.
		/// </summary>
		/// <param name="handle">The provided source.</param>
		internal JoystickHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		/// <summary>
		/// Compares two joystick handles for equality.
		/// </summary>
		/// <param name="a">The first joystick handle.</param>
		/// <param name="b">The second joystick handle.</param>
		/// <returns>True if the handles are equal, false otherwise.</returns>
		public static bool operator ==(JoystickHandle a, JoystickHandle b)
		{
			return (a.handle == b.handle);
		}

		/// <summary>
		/// Compares two joystick handles for inequality.
		/// </summary>
		/// <param name="a">The first joystick handle.</param>
		/// <param name="b">The second joystick handle.</param>
		/// <returns>True if the handles are not equal, false otherwise.</returns>
		public static bool operator !=(JoystickHandle a, JoystickHandle b)
		{
			return !(a == b);
		}

		/// <summary>
		/// Compares a joystick handle with an object for equality.
		/// </summary>
		/// <param name="obj">The object to compare against.</param>
		/// <returns>True if the objects are equal, false otherwise.</returns>
		public override bool Equals(object obj)
		{
			if (obj is JoystickHandle)
				return this == (JoystickHandle)obj;
			else
				return false;
		}

		/// <summary>
		/// Gets the hash code.
		/// </summary>
		/// <returns>The hash code.</returns>
		public override int GetHashCode()
		{
			return handle.GetHashCode();
		}

		/// <summary>
		/// Implicit conversion to an IntPtr, for internal use.
		/// </summary>
		/// <param name="handle">The handle.</param>
		/// <returns>The underlying source.</returns>
		public static implicit operator IntPtr(JoystickHandle handle)
		{
			return handle.handle;
		}
	}
}
