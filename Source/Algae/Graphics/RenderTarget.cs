using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// An enumeration of depth attachment formats.
	/// </summary>
	public enum DepthAttachmentFormat
	{
		/// <summary>
		/// No depth attachment.
		/// </summary>
		None,

		/// <summary>
		/// Defines a 16-bit depth format.
		/// </summary>
		Depth16 = TextureFormat.Depth16,

		/// <summary>
		/// Defines a 24-bit depth format.
		/// </summary>
		Depth24 = TextureFormat.Depth24,

		/// <summary>
		/// Defines a 32-bit depth format.
		/// </summary>
		Depth32 = TextureFormat.Depth32,

		/// <summary>
		/// Defines a 24-bit depth format with 8-bit stencil.
		/// </summary>
		Depth24Stencil8 = TextureFormat.Depth24Stencil8,

		/// <summary>
		/// Defines a 32-bit depth format with 8-bit stencil.
		/// </summary>
		Depth32Stencil8 = TextureFormat.Depth32Stencil8
	}

	/// <summary>
	/// Defines a render target.
	/// </summary>
	public class RenderTarget : IDisposable
	{
		IRenderTarget renderTarget;

		Texture2D depth;
		/// <summary>
		/// Gets the depth buffer attached to the render target.
		/// </summary>
		public Texture2D Depth
		{
			get
			{
				if (depth == null)
					depth = new Texture2D(renderTarget.Depth);

				return depth;
			}
		}

		// Sparse list of Texture2D wrappers.
		Dictionary<int, Texture2D> attachments = new Dictionary<int, Texture2D>();

		/// <summary>
		/// Gets a color attachment at the provided index.
		/// </summary>
		/// <param name="index">The index</param>
		/// <returns>The color attachment.</returns>
		public Texture2D this[int index]
		{
			get
			{
				if (!attachments.ContainsKey(index))
					attachments.Add(index, new Texture2D(renderTarget[index]));

				return attachments[index];
			}
		}

		/// <summary>
		/// Gets the amount of color attachments.
		/// </summary>
		public int ColorAttachments
		{
			get { return renderTarget.ColorAttachments; }
		}

		/// <summary>
		/// Gets the width of the render target.
		/// </summary>
		public int Width
		{
			get { return renderTarget.Width; }
		}

		/// <summary>
		/// Gets the height of the render target.
		/// </summary>
		public int Height
		{
			get { return renderTarget.Height; }
		}

		/// <summary>
		/// Creates a render target.
		/// </summary>
		/// <param name="renderer">The renderer.</param>
		/// <param name="width">The width of the render target.</param>
		/// <param name="height">The height of the render target.</param>
		/// <param name="depthFormat">The depth format of the render target.</param>
		/// <param name="textureFormats">The texture formats of the color attachments.</param>
		public RenderTarget(Renderer renderer, int width, int height, DepthAttachmentFormat depthFormat, params TextureFormat[] textureFormats)
		{
			renderTarget = renderer.CreateRenderTarget(width, height, depthFormat, textureFormats);
		}

		/// <summary>
		/// Prepares the render target for use.
		/// </summary>
		internal void Bind()
		{
			renderTarget.Bind();
		}

		/// <summary>
		/// Implementation of IDisposable.
		/// </summary>
		public void Dispose()
		{
			renderTarget.Dispose();
		}
	}
}
