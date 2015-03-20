using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Platform
{
	/// <summary>
	/// A keyboard input event.
	/// </summary>
	public class KeyboardEventArgs : EventArgs
	{
		/// <summary>
		/// The key code that represents the event.
		/// </summary>
		public KeyCode Key
		{
			get;
			set;
		}

		/// <summary>
		/// The keyboard modifier.
		/// </summary>
		public KeyModifier Modifiers
		{
			get;
			set;
		}

		/// <summary>
		/// The textual representation of the character.
		/// </summary>
		public string Character
		{
			get;
			set;
		}
	}
}
