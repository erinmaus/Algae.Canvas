using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// Defines the dimensions of a texture.
	/// </summary>
	public enum TextureDimensions
	{
		/// <summary>
		/// A one dimensional texture.
		/// </summary>
		One,
		
		/// <summary>
		/// A two dimensional texture.
		/// </summary>
		Two,

		/// <summary>
		/// A three dimensional texture.
		/// </summary>
		Three
	}

	/// <summary>
	/// Defines a texture format.
	/// </summary>
	public enum TextureFormat
	{
		/// <summary>
		/// Defines a RGBA format with 32-bits per floating point component.
		/// </summary>
		RedGreenBlueAlpha32,

		/// <summary>
		/// Defines a RG format with 32-bits per floating point component.
		/// </summary>
		RedGreen32,

		/// <summary>
		/// Defines a R format with 32-bits per floating point component.
		/// </summary>
		Red32,

		/// <summary>
		/// Defines a RGBA format with 8-bits per normalized byte component.
		/// </summary>
		RedGreenBlueAlpha8,

		/// <summary>
		/// Defines a RG format with 8-bits per normalized byte component.
		/// </summary>
		RedGreen8,

		/// <summary>
		/// Defines a R format with 8-bits per normalized byte component.
		/// </summary>
		Red8,

		/// <summary>
		/// Defines a 16-bit depth format.
		/// </summary>
		Depth16,

		/// <summary>
		/// Defines a 24-bit depth format.
		/// </summary>
		Depth24,

		/// <summary>
		/// Defines a 32-bit depth format.
		/// </summary>
		Depth32,

		/// <summary>
		/// Defines a 24-bit depth format with 8-bit stencil.
		/// </summary>
		Depth24Stencil8,

		/// <summary>
		/// Defines a 32-bit depth format with 8-bit stencil.
		/// </summary>
		Depth32Stencil8
	}

	/// <summary>
	/// Defines a texture filter mode.
	/// </summary>
	public enum TextureFilterMode
	{
		/// <summary>
		/// Uses nearest neighbor filtering.
		/// </summary>
		Nearest,

		/// <summary>
		/// Uses linear filtering.
		/// </summary>
		Linear,

		/// <summary>
		/// Uses linear filtering with mipmaps.
		/// </summary>
		Mipmaps
	}

	/// <summary>
	/// Defines a repeat mode.
	/// </summary>
	public enum TextureRepeatMode
	{
		/// <summary>
		/// Clamps to texture edges.
		/// </summary>
		Clamp,

		/// <summary>
		/// Repeats the texture.
		/// </summary>
		Repeat,

		/// <summary>
		/// Mirrors the texture on repeats.
		/// </summary>
		Mirror
	}

	/// <summary>
	/// Defines a depth mode.
	/// </summary>
	public enum TextureDepthMode
	{
		/// <summary>
		/// Compares the depth to a reference value.
		/// </summary>
		CompareToRef,

		/// <summary>
		/// Performs no comparison.
		/// </summary>
		None
	}

	/// <summary>
	/// Defines a depth comparison function, used when depth comparisons are requested.
	/// </summary>
	/// <remarks>Implement the others!</remarks>
	public enum TextureDepthFunction
	{
		/// <summary>
		/// Never.
		/// </summary>
		Never,

		/// <summary>
		/// Less.
		/// </summary>
		Less,

		/// <summary>
		/// Equal.
		/// </summary>
		Equal,

		/// <summary>
		/// Less equal.
		/// </summary>
		LessEqual,

		/// <summary>
		/// Greater.
		/// </summary>
		Greater,

		/// <summary>
		/// Not equal.
		/// </summary>
		NotEqual,

		/// <summary>
		/// Greater equal.
		/// </summary>
		GreaterEqual,

		/// <summary>
		/// Always.
		/// </summary>
		Always
	}

	/// <summary>
	/// Defines the base texture class.
	/// </summary>
	public abstract class Texture : IDisposable
	{
		/// <summary>
		/// Gets or sets the underlying texture.
		/// </summary>
		internal ITexture InternalTexture
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the dimension count of the texture.
		/// </summary>
		public TextureDimensions Dimensions
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the internal texture format.
		/// </summary>
		public TextureFormat Format
		{
			get { return InternalTexture.Format; }
		}

		/// <summary>
		/// Gets or sets the filter mode.
		/// </summary>
		public TextureFilterMode Filter
		{
			get { return InternalTexture.Filter; }
			set { InternalTexture.Filter = value; }
		}

		/// <summary>
		/// Gets or sets the anisotropy value.
		/// </summary>
		public int Anisotropy
		{
			get { return InternalTexture.Anisotropy; }
			set { InternalTexture.Anisotropy = value; }
		}

		/// <summary>
		/// Gets or sets the repeat mode.
		/// </summary>
		public TextureRepeatMode Repeat
		{
			get { return InternalTexture.Repeat; }
			set { InternalTexture.Repeat = value; }
		}

		/// <summary>
		/// Gets or sets the depth mode.
		/// </summary>
		public TextureDepthMode DepthMode
		{
			get { return InternalTexture.DepthMode; }
			set { InternalTexture.DepthMode = value; }
		}

		/// <summary>
		/// Gets or sets the depth function.
		/// </summary>
		public TextureDepthFunction DepthFunction
		{
			get { return InternalTexture.DepthFunction; }
			set { InternalTexture.DepthFunction = value; }
		}

		/// <summary>
		/// Initializes the texture to a default state.
		/// </summary>
		/// <param name="dimensions">The dimensions of the texture.</param>
		public Texture(TextureDimensions dimensions)
		{
			Dimensions = dimensions;
		}

		/// <summary>
		/// Sets the texture data at the provided level.
		/// </summary>
		/// <typeparam name="T">The type of the pixel data.</typeparam>
		/// <param name="data">The data.</param>
		/// <param name="format">The format of the data.</param>
		/// <param name="level">The optional mipmap level.</param>
		public virtual void SetData<T>(T[] data, TextureFormat format, int level = 0) where T : struct
		{
			InternalTexture.SetData(data, format, level);
		}

		/// <summary>
		/// Gets the texture data at the provided level.
		/// </summary>
		/// <typeparam name="T">The type of the pixel data.</typeparam>
		/// <param name="data">The data.</param>
		/// <param name="format">The format of the data.</param>
		/// <param name="level">The optional mipmap level.</param>
		public virtual void GetData<T>(T[] data, TextureFormat format, int level = 0) where T : struct
		{
			InternalTexture.GetData(data, format, level);
		}

		/// <summary>
		/// Automatically generates mipmaps.
		/// </summary>
		public virtual void GenerateMipmaps()
		{
			InternalTexture.GenerateMipmaps();
		}

		/// <summary>
		/// Disposes of all unmanaged resources allocated by the texture.
		/// </summary>
		public abstract void Dispose();

		/// <summary>
		/// Binds a texture to the provided sampler.
		/// </summary>
		/// <param name="sampler">The sampler.</param>
		internal void Bind(int sampler)
		{
			InternalTexture.Bind(sampler);
		}
	}
}
