using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Platform
{
	/// <summary>
	/// An interface that allows an Algae platform context to queue events.
	/// </summary>
	interface IAlgaeEventProvider
	{
		/// <summary>
		/// Gets the event source of the event provider.
		/// </summary>
		IntPtr EventSource
		{
			get;
		}
	}
}
