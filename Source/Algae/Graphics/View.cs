using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// Defines a view.
	/// </summary>
	/// <remarks>A view serves as the basis for a camera.</remarks>
	public struct View
	{
		/// <summary>
		/// The viewport associated with the view.
		/// </summary>
		public Viewport Viewport;

		/// <summary>
		/// A value indicating if the view has depth buffering enabled.
		/// </summary>
		public bool DepthEnabled;

		/// <summary>
		/// A value indicating if stenciling enabled.
		/// </summary>
		public bool StencilEnabled;

		/// <summary>
		/// A value indicating if culling is enabled.
		/// </summary>
		public bool CullEnabled;

		/// <summary>
		/// An optional render target.
		/// </summary>
		public RenderTarget RenderTarget;
	}
}
