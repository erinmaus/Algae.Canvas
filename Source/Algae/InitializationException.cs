using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae
{
	/// <summary>
	/// An exception that describes a problem during initialization of an IInitializable.
	/// </summary>
	public class InitializationException : Exception
	{
		/// <summary>
		/// Constructs an instance of an InitializationException with the corresponding message.
		/// </summary>
		/// <param name="message">A message that describes the exception.</param>
		public InitializationException(string message)
			: base(message)
		{
		}
	}
}
