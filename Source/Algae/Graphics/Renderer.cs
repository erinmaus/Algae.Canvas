using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// An enumeration that defines a buffer function.
	/// </summary>
	public enum BufferFunction
	{
		/// <summary>
		/// A buffer function that guarantees failure.
		/// </summary>
		Never,

		/// <summary>
		/// Passes only when the source is less than the destination.
		/// </summary>
		Less,

		/// <summary>
		/// Passes only when both source and destination are equal.
		/// </summary>
		Equal,

		/// <summary>
		/// Passes only when the source is less than or equal to the destination.
		/// </summary>
		LessEqual,

		/// <summary>
		/// Passes only when the source is greater than the destination.
		/// </summary>
		Greater,

		/// <summary>
		/// Passes only when the source and the destination are not equal.
		/// </summary>
		NotEqual,

		/// <summary>
		/// Passes only when the source is greater than or equal to the destination.
		/// </summary>
		GreaterEqual,

		/// <summary>
		/// A buffer function that guarantees success.
		/// </summary>
		Always
	}

	/// <summary>
	/// An enumeration that defines a blend function.
	/// </summary>
	public enum BlendFunction
	{
		/// <summary>
		/// A blend function.
		/// </summary>
		Zero,

		/// <summary>
		/// A blend function.
		/// </summary>
		One,

		/// <summary>
		/// A blend function.
		/// </summary>
		SourceColor,

		/// <summary>
		/// A blend function.
		/// </summary>
		InverseSourceColor,

		/// <summary>
		/// A blend function.
		/// </summary>
		DestinationColor,

		/// <summary>
		/// A blend function.
		/// </summary>
		InverseDestinationColor,

		/// <summary>
		/// A blend function.
		/// </summary>
		SourceAlpha,

		/// <summary>
		/// A blend function.
		/// </summary>
		InverseSourceAlpha,

		/// <summary>
		/// A blend function.
		/// </summary>
		DestinationAlpha,

		/// <summary>
		/// A blend function.
		/// </summary>
		InverseDestinationAlpha
	}

	/// <summary>
	/// An enumeration that describes the cull mode for rendering.
	/// </summary>
	public enum CullMode
	{
		/// <summary>
		/// Cull clockwise polygons.
		/// </summary>
		Back,

		/// <summary>
		/// Cull counter-clockwise polygons.
		/// </summary>
		Front
	}

	/// <summary>
	/// An enumeration that describes a stencil function.
	/// </summary>
	public enum StencilFunction
	{
		/// <summary>
		/// Keep the current value.
		/// </summary>
		Keep,

		/// <summary>
		/// Set the current value to zero.
		/// </summary>
		Zero,

		/// <summary>
		/// Replace the current value.
		/// </summary>
		Replace,

		/// <summary>
		/// Increment the current value by one.
		/// </summary>
		Increment,

		/// <summary>
		/// Increment the current value by one and wrap, if necessary.
		/// </summary>
		IncrementWrap,

		/// <summary>
		/// Decrement the current value by one.
		/// </summary>
		Decrement,

		/// <summary>
		/// Decrement the current value by one and wrap, if necessary.
		/// </summary>
		DecrementWrap,

		/// <summary>
		/// Invert the current value.
		/// </summary>
		Invert
	}

	/// <summary>
	/// The base renderer class.
	/// </summary>
	public abstract class Renderer
	{
		/// <summary>
		/// Gets the name of the renderer.
		/// </summary>
		public string Name
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the tag of the renderer.
		/// </summary>
		public string Tag
		{
			get;
			private set;
		}

		/// <summary>
		/// Constructs the renderer.
		/// </summary>
		/// <param name="name">The name of the renderer.</param>
		/// <param name="tag">The tag of the renderer.</param>
		protected Renderer(string name, string tag)
		{
			Name = name;
			Tag = tag;
		}

		/// <summary>
		/// Compiles a material definition.
		/// </summary>
		/// <param name="definition">The definition to compile.</param>
		/// <returns>The resulting compiled material.</returns>
		internal abstract ICompiledMaterial CompileMaterial(MaterialDefinition definition);

		/// <summary>
		/// Creates a mesh.
		/// </summary>
		/// <param name="vertexDeclaration">The vertex declaration.</param>
		/// <returns>The mesh.</returns>
		internal abstract IMesh CreateMesh(VertexDeclaration vertexDeclaration);

		/// <summary>
		/// Creates a 2D texture.
		/// </summary>
		/// <param name="width">The width of the texture.</param>
		/// <param name="height">The height of the texture.</param>
		/// <param name="format">The format of the texture.</param>
		/// <returns>The texture.</returns>
		internal abstract ITexture2D CreateTexture2D(int width, int height, TextureFormat format);

		/// <summary>
		/// Creates a render target.
		/// </summary>
		/// <param name="width">The width of the render target.</param>
		/// <param name="height">The height of the render target.</param>
		/// <param name="depthFormat">The depth format of the render target.</param>
		/// <param name="textureFormats">The texture formats of the color attachments.</param>
		/// <returns>The render target.</returns>
		internal abstract IRenderTarget CreateRenderTarget(int width, int height, DepthAttachmentFormat depthFormat, params TextureFormat[] textureFormats);

		/// <summary>
		/// Applies a view.
		/// </summary>
		/// <param name="view">The view to apply.</param>
		public abstract void ApplyView(View view);

		/// <summary>
		/// Clears the back buffer to the provided color.
		/// </summary>
		/// <param name="color">The color to clear to.</param>
		public abstract void Clear(Color color);

		/// <summary>
		/// Clears the depth buffer.
		/// </summary>
		/// <param name="value">The value to clear the depth buffer to.</param>
		public abstract void ClearDepth(float value = 1.0f);

		/// <summary>
		/// Clears the stencil buffer.
		/// </summary>
		/// <param name="value">The value to clear the stencil buffer to.</param>
		public abstract void ClearStencil(int value = 0);

		/// <summary>
		/// Clears the attachment to the provided color.
		/// </summary>
		/// <param name="color">The color to clear.</param>
		/// <param name="attachmentIndex">The attachment index.</param>
		public abstract void ClearAttachment(Color color, int attachmentIndex);

		/// <summary>
		/// Clears the depth buffer.
		/// </summary>
		/// <param name="value">The value to clear the depth buffer to.</param>
		public abstract void ClearAttachmentDepth(float value = 1.0f);

		/// <summary>
		/// Clears the stencil buffer.
		/// </summary>
		/// <param name="value">The value to clear the stencil buffer to.</param>
		public abstract void ClearAttachmentStencil(int value = 0);

		/// <summary>
		/// Sets the blending mode.
		/// </summary>
		/// <param name="source">The source blend function.</param>
		/// <param name="destination">The destination blend function.</param>
		public abstract void SetBlendMode(BlendFunction source, BlendFunction destination);

		/// <summary>
		/// Sets the color mask.
		/// </summary>
		/// <param name="red">The red mask.</param>
		/// <param name="green">The green mask.</param>
		/// <param name="blue">The blue mask.</param>
		/// <param name="alpha">The alpha mask.</param>
		public abstract void SetColorMask(bool red, bool green, bool blue, bool alpha);

		/// <summary>
		/// Sets the cull mode.
		/// </summary>
		/// <param name="mode">The cull mode.</param>
		public abstract void SetCullMode(CullMode mode);

		/// <summary>
		/// Sets the depth buffer function.
		/// </summary>
		/// <param name="function">The buffer function.</param>
		public abstract void SetDepthFunction(BufferFunction function);

		/// <summary>
		/// Sets the depth mask.
		/// </summary>
		/// <param name="enable">The depth mask.</param>
		public abstract void SetDepthMask(bool enable);

		/// <summary>
		/// Sets the depth offset.
		/// </summary>
		/// <param name="factor">The factor to multiply the depth values by.</param>
		/// <param name="units">The constant to add to depth values.</param>
		public abstract void SetDepthOffset(float factor, float units);

		public abstract void SetStencilOperation(StencilFunction depthFail, StencilFunction stencilFail, StencilFunction depthPass);

		public abstract void SetStencilWriteMask(int mask);

		public abstract void SetStencilFunction(BufferFunction function, int reference, int mask);

		public abstract void Finish();
	}
}
