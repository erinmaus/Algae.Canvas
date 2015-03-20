using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// An OpenGL 3 implementation of a renderer.
	/// </summary>
	class GL3Renderer : Renderer, IInitializable
	{
		public bool IsInitialized
		{
			get;
			set;
		}

		public ICompiledMaterial CurrentMaterial
		{
			get;
			internal set;
		}

		#region Windows-specific glue

#if WINDOWS
		[DllImport("opengl32.dll")]
		static extern IntPtr wglGetCurrentContext();

		[DllImport("opengl32.dll")]
		static extern IntPtr wglGetCurrentDC();

		[DllImport("user32.dll")]
		static extern IntPtr WindowFromDC(IntPtr hDC);

		GraphicsContext dummyContext;
		ContextHandle dummyHandle;
		IWindowInfo dummyWindow;
#endif

		#endregion

		public GL3Renderer()
			: base("opengl", "3")
		{
		}

		public void Initialize()
		{
			if (IsInitialized)
				throw new InitializationException("Renderer has been initialized.");

			// Create dummy context for OpenTK
			// On Windows, this is a problem because of an OpenTK bug, so a workaround must be employed.
#if WINDOWS
			IntPtr handle = wglGetCurrentContext();
			dummyHandle = new ContextHandle(handle);
			dummyWindow = Utilities.CreateWindowsWindowInfo(WindowFromDC(wglGetCurrentDC()));
			dummyContext = new GraphicsContext(dummyHandle, dummyWindow, null, 3, 0, GraphicsContextFlags.Default);
#else
			dummyContext = GraphicsContext.CreateDummyContext();
#endif

			// Always enable blending.
			// XXX: Can't think of a reason to have it disabled?
			GL.Enable(EnableCap.Blend);

			IsInitialized = true;
		}

		internal override ICompiledMaterial CompileMaterial(MaterialDefinition definition)
		{
			return new GL3CompiledMaterial(this, definition);
		}

		internal override IMesh CreateMesh(VertexDeclaration vertexDeclaration)
		{
			return new GL3Mesh(vertexDeclaration);
		}

		internal override ITexture2D CreateTexture2D(int width, int height, TextureFormat format)
		{
			return new GL3Texture2D(width, height, format);
		}

		internal override IRenderTarget CreateRenderTarget(int width, int height, DepthAttachmentFormat depthFormat, params TextureFormat[] textureFormats)
		{
			return new GL3RenderTarget(width, height, depthFormat, textureFormats);
		}

		public override void ApplyView(View view)
		{
			if (view.DepthEnabled)
				GL.Enable(EnableCap.DepthTest);
			else
				GL.Disable(EnableCap.DepthTest);

			if (view.StencilEnabled)
				GL.Enable(EnableCap.StencilTest);
			else
				GL.Disable(EnableCap.StencilTest);

			if (view.CullEnabled)
				GL.Enable(EnableCap.CullFace);
			else
				GL.Disable(EnableCap.CullFace);

			GL.Viewport(view.Viewport.X, view.Viewport.Y, view.Viewport.Width, view.Viewport.Height);

			// Unbind any framebuffer if the view has none.
			if (view.RenderTarget == null)
			{
				GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
				GL.DrawBuffer(DrawBufferMode.Back);
			}
			else
			{
				view.RenderTarget.Bind();
			}
		}

		public override void Clear(Color color)
		{
			GL.ClearColor(color.Red, color.Green, color.Blue, color.Alpha);
			GL.ColorMask(true, true, true, true);
			GL.Clear(ClearBufferMask.ColorBufferBit);
		}

		public override void ClearDepth(float value = 1.0f)
		{
			GL.ClearDepth(value);
			GL.DepthMask(true);
			GL.Clear(ClearBufferMask.DepthBufferBit);
		}

		public override void ClearStencil(int value = 0)
		{
			GL.ClearStencil(value);
			GL.StencilMask(~0);
			GL.Clear(ClearBufferMask.StencilBufferBit);
		}

		public override void ClearAttachment(Color color, int index = 0)
		{
			// Clear the specified color buffer.
			GL.ClearBuffer(ClearBuffer.Color, index, ref color.Red);
		}

		public override void ClearAttachmentDepth(float value = 1.0f)
		{
			GL.ClearBuffer(ClearBuffer.Depth, 0, ref value);
		}

		public override void ClearAttachmentStencil(int value = 0)
		{
			GL.ClearBuffer(ClearBuffer.Stencil, 0, ref value);
		}

		public override void SetBlendMode(BlendFunction source, BlendFunction destination)
		{
			// Convert them...
			BlendingFactorSrc src = BlendingFactorSrc.Zero;

			switch (source)
			{
				case BlendFunction.Zero:
					src = BlendingFactorSrc.Zero;
					break;
				case BlendFunction.One:
					src = BlendingFactorSrc.One;
					break;
				case BlendFunction.SourceColor:
					src = BlendingFactorSrc.Src1Color;
					break;
				case BlendFunction.InverseSourceColor:
					src = BlendingFactorSrc.OneMinusSrc1Color;
					break;
				case BlendFunction.DestinationColor:
					src = BlendingFactorSrc.DstColor;
					break;
				case BlendFunction.InverseDestinationColor:
					src = BlendingFactorSrc.OneMinusDstColor;
					break;
				case BlendFunction.SourceAlpha:
					src = BlendingFactorSrc.SrcAlpha;
					break;
				case BlendFunction.InverseSourceAlpha:
					src = BlendingFactorSrc.OneMinusSrcAlpha;
					break;
				case BlendFunction.DestinationAlpha:
					src = BlendingFactorSrc.DstAlpha;
					break;
				case BlendFunction.InverseDestinationAlpha:
					src = BlendingFactorSrc.OneMinusDstAlpha;
					break;
			}

			BlendingFactorDest dest = BlendingFactorDest.Zero;

			switch (destination)
			{
				case BlendFunction.Zero:
					dest = BlendingFactorDest.Zero;
					break;
				case BlendFunction.One:
					dest = BlendingFactorDest.One;
					break;
				case BlendFunction.SourceColor:
					dest = BlendingFactorDest.Src1Color;
					break;
				case BlendFunction.InverseSourceColor:
					dest = BlendingFactorDest.OneMinusSrc1Color;
					break;
				case BlendFunction.DestinationColor:
				case BlendFunction.InverseDestinationColor:
					// Unsupported...
					dest = BlendingFactorDest.Zero;
					break;
				case BlendFunction.SourceAlpha:
					dest = BlendingFactorDest.SrcAlpha;
					break;
				case BlendFunction.InverseSourceAlpha:
					dest = BlendingFactorDest.OneMinusSrcAlpha;
					break;
				case BlendFunction.DestinationAlpha:
					dest = BlendingFactorDest.DstAlpha;
					break;
				case BlendFunction.InverseDestinationAlpha:
					dest = BlendingFactorDest.OneMinusDstAlpha;
					break;
			}

			GL.BlendFunc(src, dest);
		}

		public override void SetColorMask(bool red, bool green, bool blue, bool alpha)
		{
			GL.ColorMask(red, green, blue, alpha);
		}

		public override void SetCullMode(CullMode mode)
		{
			switch (mode)
			{
				case CullMode.Back:
					GL.CullFace(CullFaceMode.Back);
					break;
				case CullMode.Front:
					GL.CullFace(CullFaceMode.Front);
					break;
			}
		}

		public override void SetDepthFunction(BufferFunction function)
		{
			switch (function)
			{
				case BufferFunction.Never:
					GL.DepthFunc(DepthFunction.Never);
					break;
				case BufferFunction.Less:
					GL.DepthFunc(DepthFunction.Less);
					break;
				case BufferFunction.Equal:
					GL.DepthFunc(DepthFunction.Equal);
					break;
				case BufferFunction.LessEqual:
					GL.DepthFunc(DepthFunction.Lequal);
					break;
				case BufferFunction.Greater:
					GL.DepthFunc(DepthFunction.Greater);
					break;
				case BufferFunction.NotEqual:
					GL.DepthFunc(DepthFunction.Notequal);
					break;
				case BufferFunction.GreaterEqual:
					GL.DepthFunc(DepthFunction.Gequal);
					break;
				case BufferFunction.Always:
					GL.DepthFunc(DepthFunction.Always);
					break;
			}
		}

		public override void SetDepthMask(bool enable)
		{
			GL.DepthMask(enable);
		}

		public override void SetDepthOffset(float factor, float units)
		{
			GL.PolygonOffset(factor, units);
		}

		static StencilOp GetStencilOp(StencilFunction func)
		{
			switch (func)
			{
				case StencilFunction.Keep:
				default:
					return StencilOp.Keep;
				case StencilFunction.Zero:
					return StencilOp.Zero;
				case StencilFunction.Replace:
					return StencilOp.Replace;
				case StencilFunction.Increment:
					return StencilOp.Incr;
				case StencilFunction.IncrementWrap:
					return StencilOp.IncrWrap;
				case StencilFunction.Decrement:
					return StencilOp.Decr;
				case StencilFunction.DecrementWrap:
					return StencilOp.DecrWrap;
				case StencilFunction.Invert:
					return StencilOp.Invert;
			}
		}

		public override void SetStencilOperation(StencilFunction depthFail, StencilFunction stencilFail, StencilFunction depthPass)
		{
			GL.StencilOp(GetStencilOp(depthFail), GetStencilOp(stencilFail), GetStencilOp(depthPass));
		}

		public override void SetStencilWriteMask(int mask)
		{
			GL.StencilMask(mask);
		}

		public override void SetStencilFunction(BufferFunction function, int reference, int mask)
		{
			switch (function)
			{
				case BufferFunction.Never:
					GL.StencilFunc(OpenTK.Graphics.OpenGL.StencilFunction.Never, reference, mask);
					break;
				case BufferFunction.Less:
					GL.StencilFunc(OpenTK.Graphics.OpenGL.StencilFunction.Less, reference, mask);
					break;
				case BufferFunction.Equal:
					GL.StencilFunc(OpenTK.Graphics.OpenGL.StencilFunction.Equal, reference, mask);
					break;
				case BufferFunction.LessEqual:
					GL.StencilFunc(OpenTK.Graphics.OpenGL.StencilFunction.Lequal, reference, mask);
					break;
				case BufferFunction.Greater:
					GL.StencilFunc(OpenTK.Graphics.OpenGL.StencilFunction.Greater, reference, mask);
					break;
				case BufferFunction.NotEqual:
					GL.StencilFunc(OpenTK.Graphics.OpenGL.StencilFunction.Notequal, reference, mask);
					break;
				case BufferFunction.GreaterEqual:
					GL.StencilFunc(OpenTK.Graphics.OpenGL.StencilFunction.Gequal, reference, mask);
					break;
				case BufferFunction.Always:
					GL.StencilFunc(OpenTK.Graphics.OpenGL.StencilFunction.Always, reference, mask);
					break;
			}
		}

		public override void Finish()
		{
			GL.Finish();
		}
	}
}
