using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CommaExcess.Algae.Platform
{
	/// <summary>
	/// A class that abstracts the underlying platform, hardware, and services from the main program.
	/// </summary>
	class AlgaePlatformContext : PlatformContext
	{
		public override string ExecutablePath
		{
			get
			{
#if WINDOWS
				return Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + Path.DirectorySeparatorChar;
#else
				IntPtr path = AllegroMethods.al_get_standard_path(0);
				string executablePath = Marshal.PtrToStringAnsi(AllegroMethods.al_path_cstr(path, '/'));

				AllegroMethods.al_destroy_path(path);

				return executablePath;
#endif
			}
		}

		/// <summary>
		/// Initializes the platform context.
		/// </summary>
		public override void Initialize()
		{
			if (IsInitialized)
				throw new InitializationException("Platform context is already initialized.");

			int r = AllegroMethods.al_install_system(AllegroMethods.Version, IntPtr.Zero);
			if (r == 0)
				throw new InitializationException("Could not initialize base system.");

			IsInitialized = true;
		}

		/// <summary>
		/// Disposes of all resources allocated by the platform context.
		/// </summary>
		public override void Dispose()
		{
			if (IsInitialized)
				AllegroMethods.al_uninstall_system();
		}
	}
}
