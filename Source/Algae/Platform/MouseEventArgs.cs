using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Platform
{
	/// <summary>
	/// A mouse input event.
	/// </summary>
	public class MouseEventArgs : EventArgs
	{
		/// <summary>
		/// The position of the mouse.
		/// </summary>
		public Vector3 Position
		{
			get;
			set;
		}

		/// <summary>
		/// The difference in position of the mouse.
		/// </summary>
		public Vector3 Difference
		{
			get;
			set;
		}
		
		/// <summary>
		/// The button pressed.
		/// </summary>
		public int Button
		{
			get;
			set;
		}
	}
}
