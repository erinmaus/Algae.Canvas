using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Graphics.OpenGL;

namespace CommaExcess.Algae.Graphics
{
	class GL3RenderTarget : IRenderTarget
	{
		GL3Texture2D depth;
		public ITexture2D Depth
		{
			get { return depth; }
		}

		bool isManaged;
		List<GL3Texture2D> attachments = new List<GL3Texture2D>();
		public ITexture2D this[int index]
		{
			get { return attachments[index]; }
		}

		public int ColorAttachments
		{
			get { return attachments.Count; }
		}

		public int Width
		{
			get;
			private set;
		}

		public int Height
		{
			get;
			private set;
		}

		int framebuffer;
		DrawBuffersEnum[] drawBuffers;

		public GL3RenderTarget(int width, int height, DepthAttachmentFormat depthFormat, params TextureFormat[] textureFormats)
		{
			if (depthFormat != DepthAttachmentFormat.None)
			{
				depth = new GL3Texture2D(width, height, (TextureFormat)depthFormat);
			}

			foreach (TextureFormat format in textureFormats)
			{
				GL3Texture2D texture = new GL3Texture2D(width, height, format);

				attachments.Add(texture);
			}

			Initialize(width, height);
		}

		void Initialize(int width, int height)
		{
			// Build framebuffer object.
			GL.GenFramebuffers(1, out framebuffer);

			// Prepare by binding the framebuffer.
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebuffer);

			// Attach depth (if a depth buffer was requested).
			if (depth != null)
			{
				if (HasStencil(depth.Format))
					GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, TextureTarget.Texture2D, depth.TextureID, 0);
				else
					GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, depth.TextureID, 0);
			}

			// Attach... attachments.
			// Do so in the order requested.
			// Also create the enum array used by glDrawBuffers.
			drawBuffers = new DrawBuffersEnum[ColorAttachments];

			int index = 0;
			foreach (GL3Texture2D attachment in attachments)
			{
				drawBuffers[index] = (DrawBuffersEnum)(DrawBuffersEnum.ColorAttachment0 + index);
				GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, (FramebufferAttachment)(FramebufferAttachment.ColorAttachment0 + index), TextureTarget.Texture2D, attachment.TextureID, 0);

				index++;
			}

			// Unbind the framebuffer.
			// Keep in mind that creating objects while rendering can disrupt state.
			// So if a previous framebuffer was bound, it won't be restored.
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

			FramebufferErrorCode status = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);

			if (status != FramebufferErrorCode.FramebufferComplete)
				throw new InvalidOperationException("Framebuffer is not complete.");

			// Set the state.
			Width = width;
			Height = height;
		}

		public void Bind()
		{
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebuffer);
			GL.DrawBuffers(drawBuffers.Length, drawBuffers);
		}

		public void Dispose()
		{
			// Depth is optional.
			if (depth != null)
				depth.Dispose();

			foreach (GL3Texture2D attachment in attachments)
			{
				attachment.Dispose();
			}

			GL.DeleteFramebuffers(1, ref framebuffer);
		}

		static bool HasStencil(TextureFormat format)
		{
			switch (format)
			{
				// Does not have stencil.
				case TextureFormat.Depth16:
				case TextureFormat.Depth24:
				case TextureFormat.Depth32:
					return false;

				// Has stencil.
				case TextureFormat.Depth24Stencil8:
				case TextureFormat.Depth32Stencil8:
					return true;

				// Sanity.
				default:
					throw new ArgumentException("Expected depth (or depth stencil) texture format.", "format");
			}
		}
	}
}
