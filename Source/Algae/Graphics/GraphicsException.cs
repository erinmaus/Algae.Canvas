using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// A fatal graphics exception.
	/// </summary>
	public class GraphicsException : Exception
	{
		/// <summary>
		/// The underlying system function that reported the error.
		/// </summary>
		public string Function 
		{ 
			get; 
			private set; 
		}

		/// <summary>
		/// Constructs a graphics exception.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="function">The function.</param>
		public GraphicsException(string message, string function)
			: base(message)
		{
			Function = function;
		}
	}
}
