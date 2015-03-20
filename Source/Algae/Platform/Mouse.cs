using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Platform
{
	/// <summary>
	/// A class that provides mouse services.
	/// </summary>
	public abstract class Mouse : IInitializable, IDisposable
	{
		/// <summary>
		/// Gets if the mouse was initialized.
		/// </summary>
		public bool IsInitialized
		{
			get;
			protected set;
		}

		/// <summary>
		/// Initializes the mouse to a default state.
		/// </summary>
		public abstract void Initialize();

		/// <summary>
		/// Disposes of all resources allocated by the mouse.
		/// </summary>
		public abstract void Dispose();
	}
}
