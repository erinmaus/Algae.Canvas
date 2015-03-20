using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae
{
	/// <summary>
	/// An interface that defines an initializable object.
	/// </summary>
	/// <remarks>
	/// Deinitialization is not a part of this interface.
	/// </remarks>
	public interface IInitializable
	{
		/// <summary>
		/// Gets if the object was initialized.
		/// </summary>
		bool IsInitialized
		{
			get;
		}

		/// <summary>
		/// Initializes the object to a default state.
		/// </summary>
		void Initialize();
	}
}
