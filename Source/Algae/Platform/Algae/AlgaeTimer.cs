using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Platform
{
	/// <summary>
	/// A class that handles timing.
	/// </summary>
	class AlgaeTimer : IInitializable, IDisposable, IAlgaeEventProvider
	{
		/// <summary>
		/// Gets if the timer was initialized.
		/// </summary>
		public bool IsInitialized
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets a value indicating if the timer is currently running.
		/// </summary>
		public bool IsRunning
		{
			get;
			private set;
		}

		IntPtr timer;

		/// <summary>
		/// Gets the event source associated with the timer.
		/// </summary>
		public IntPtr EventSource
		{
			get { return AllegroMethods.al_get_timer_event_source(timer); }
		}

		float interval = 1.0f / 60.0f;

		/// <summary>
		/// Gets or sets the interval of the timer.
		/// </summary>
		public float Interval
		{
			get { return interval; }
			set
			{
				if (IsInitialized)
					AllegroMethods.al_set_timer_speed(timer, value);

				interval = value;
			}
		}

		/// <summary>
		/// Initializes the timer to a default state.
		/// </summary>
		public void Initialize()
		{
			if (IsInitialized)
				throw new InitializationException("Timer is already initialized.");

			timer = AllegroMethods.al_create_timer(interval);

			if (timer == IntPtr.Zero)
				throw new InitializationException("Could not create timer.");

			IsInitialized = true;
		}

		/// <summary>
		/// Starts the timer.
		/// </summary>
		public void Start()
		{
			AllegroMethods.al_start_timer(timer);

			IsRunning = true;
		}

		/// <summary>
		/// Stops the timer.
		/// </summary>
		public void Stop()
		{
			AllegroMethods.al_stop_timer(timer);

			IsRunning = false;
		}

		/// <summary>
		/// Disposes of all resources allocated by this timer.
		/// </summary>
		public void Dispose()
		{
			if (timer != IntPtr.Zero)
				AllegroMethods.al_destroy_timer(timer);
		}
	}
}
