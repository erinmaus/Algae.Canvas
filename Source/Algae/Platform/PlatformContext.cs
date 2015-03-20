using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Platform
{
	/// <summary>
	/// A class that abstracts the underlying platform from the main program.
	/// </summary>
	/// <remarks>
	/// Only one platform context should be created during the lifetime of a program. Multiple windows should make use of the only 
	/// platform context.
	/// </remarks>
	public abstract class PlatformContext : IDisposable, IInitializable
	{
		/// <summary>
		/// Gets if the platform context was initialized.
		/// </summary>
		public bool IsInitialized
		{
			get;
			protected set;
		}

		/// <summary>
		/// Gets the full path to the executable.
		/// </summary>
		public abstract string ExecutablePath
		{
			get;
		}

		/// <summary>
		/// Initializes the platform context.
		/// </summary>
		public abstract void Initialize();

		/// <summary>
		/// Disposes of all resources allocated by the PlatformContext.
		/// </summary>
		public abstract void Dispose();
	}
}
