using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Graphics.OpenGL;

namespace CommaExcess.Algae.Graphics
{
	abstract class GL3Texture : ITexture
	{
		// The OpenGL texture target (e.g., GL_TEXTURE_2D).
		TextureTarget target;

		/// <summary>
		/// Gets or sets the OpenGL texture ID.
		/// </summary>
		public int TextureID
		{
			get;
			protected set;
		}

		TextureFormat format;
		public TextureFormat Format
		{
			get { return format; }
		}

		TextureFilterMode filter;
		public TextureFilterMode Filter
		{
			get { return filter; }
			set
			{
				Bind(0);

				switch (value)
				{
					case TextureFilterMode.Nearest:
						GL.TexParameter(target, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
						GL.TexParameter(target, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
						break;
					case TextureFilterMode.Linear:
						GL.TexParameter(target, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
						GL.TexParameter(target, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
						break;
					case TextureFilterMode.Mipmaps:
						GL.TexParameter(target, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
						GL.TexParameter(target, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
						break;
					default:
						throw new ArgumentException("Invalid texture filter mode.", "value");
				}

				filter = value;
			}
		}

		int anisotropy = 1;
		public int Anisotropy
		{
			get
			{
				return anisotropy;
			}
			set
			{
				Bind(0);
				GL.TexParameter(target, (TextureParameterName)ExtTextureFilterAnisotropic.TextureMaxAnisotropyExt, (float)value);

				anisotropy = value;
			}
		}

		TextureRepeatMode repeat;
		public TextureRepeatMode Repeat
		{
			get
			{
				return repeat;
			}
			set
			{
				TextureWrapMode wrap;

				switch (value)
				{
					case TextureRepeatMode.Clamp:
						wrap = TextureWrapMode.ClampToEdge;
						break;
					case TextureRepeatMode.Repeat:
						wrap = TextureWrapMode.Repeat;
						break;
					case TextureRepeatMode.Mirror:
						wrap = TextureWrapMode.MirroredRepeat;
						break;
					default:
						throw new ArgumentException("Invalid texture repeat mode.", "value");
				}

				Bind(0);
				GL.TexParameter(target, TextureParameterName.TextureWrapS, (int)wrap);
				GL.TexParameter(target, TextureParameterName.TextureWrapT, (int)wrap);
				GL.TexParameter(target, TextureParameterName.TextureWrapR, (int)wrap);

				repeat = value;
			}
		}

		TextureDepthMode mode;
		public TextureDepthMode DepthMode
		{
			get { return mode; }
			set
			{
				Bind(0);

				if (!IsDepthTexture)
					throw new InvalidOperationException("Texture is not a depth texture.");

				switch (value)
				{
					case TextureDepthMode.CompareToRef:
						GL.TexParameter(target, TextureParameterName.TextureCompareMode, (int)All.CompareRefToTexture);
						break;
					case TextureDepthMode.None:
						GL.TexParameter(target, TextureParameterName.TextureCompareMode, (int)All.None);
						break;
					default:
						throw new ArgumentException("Invalid depth mode.", "value");
				}

				mode = value;
			}
		}

		TextureDepthFunction function;
		public TextureDepthFunction DepthFunction
		{
			get { return function; }
			set
			{
				Bind(0);

				if (!IsDepthTexture)
					throw new InvalidOperationException("Texture is not a depth texture.");

				All depthFunc;

				switch (value)
				{
					case TextureDepthFunction.Never:
						depthFunc = All.Never;
						break;
					case TextureDepthFunction.Less:
						depthFunc = All.Less;
						break;
					case TextureDepthFunction.Equal:
						depthFunc = All.Equal;
						break;
					case TextureDepthFunction.LessEqual:
						depthFunc = All.Lequal;
						break;
					case TextureDepthFunction.Greater:
						depthFunc = All.Greater;
						break;
					case TextureDepthFunction.NotEqual:
						depthFunc = All.Notequal;
						break;
					case TextureDepthFunction.GreaterEqual:
						depthFunc = All.Gequal;
						break;
					case TextureDepthFunction.Always:
						depthFunc = All.Always;
						break;
					default:
						throw new ArgumentException("Invalid depth function.", "value");
				}

				GL.TexParameter(target, TextureParameterName.TextureCompareFunc, (int)depthFunc);

				function = value;
			}
		}

		protected bool IsDepthTexture
		{
			get
			{ 
				return Format == TextureFormat.Depth16
					|| Format == TextureFormat.Depth24 
					|| Format == TextureFormat.Depth32
					|| Format == TextureFormat.Depth24Stencil8
					|| Format == TextureFormat.Depth32Stencil8;
			}
		}

		public GL3Texture(TextureFormat format, TextureTarget target)
		{
			this.format = format;
			this.target = target;
		}

		public abstract void SetData<T>(T[] data, TextureFormat format, int level = 0) where T : struct;

		public abstract void GetData<T>(T[] data, TextureFormat format, int level = 0) where T : struct;

		public void GenerateMipmaps()
		{
			GL.GenerateMipmap((GenerateMipmapTarget)target);
		}

		public void Bind(int sampler)
		{
			// Hop to the provided sampler.
			GL.ActiveTexture((TextureUnit)(TextureUnit.Texture0 + sampler));
			GL.BindTexture(target, TextureID);

			// Switch back to the default sampler.
			GL.ActiveTexture(TextureUnit.Texture0);
		}

		public static PixelInternalFormat GetInternalFormat(TextureFormat format)
		{
			switch (format)
			{
				case TextureFormat.RedGreenBlueAlpha32:
					return PixelInternalFormat.Rgba32f;
				case TextureFormat.RedGreen32:
					return PixelInternalFormat.Rg32f;
				case TextureFormat.Red32:
					return PixelInternalFormat.R32f;
				case TextureFormat.RedGreenBlueAlpha8:
					return PixelInternalFormat.Rgba;
				case TextureFormat.RedGreen8:
					return PixelInternalFormat.Rg8;
				case TextureFormat.Red8:
					return PixelInternalFormat.R8;
				case TextureFormat.Depth16:
					return PixelInternalFormat.DepthComponent16;
				case TextureFormat.Depth24:
					return PixelInternalFormat.DepthComponent24;
				case TextureFormat.Depth32:
					return PixelInternalFormat.DepthComponent32;
				case TextureFormat.Depth24Stencil8:
					return PixelInternalFormat.Depth24Stencil8;
				case TextureFormat.Depth32Stencil8:
					return PixelInternalFormat.Depth32fStencil8;
				default:
					throw new ArgumentException("Invalid texture format.", "format");
			}
		}

		public static PixelFormat GetComponents(TextureFormat format)
		{
			switch (format)
			{
				case TextureFormat.RedGreenBlueAlpha32:
				case TextureFormat.RedGreenBlueAlpha8:
					return PixelFormat.Rgba;
				case TextureFormat.RedGreen32:
				case TextureFormat.RedGreen8:
					return PixelFormat.Rg;
				case TextureFormat.Red32:
				case TextureFormat.Red8:
					return PixelFormat.Red;
				case TextureFormat.Depth16:
				case TextureFormat.Depth24:
				case TextureFormat.Depth32:
					return PixelFormat.DepthComponent;
				case TextureFormat.Depth24Stencil8:
				case TextureFormat.Depth32Stencil8:
					return PixelFormat.DepthStencil;
				default:
					throw new ArgumentException("Invalid texture format.", "format");
			}
		}

		public static PixelType GetComponentType(TextureFormat format)
		{
			switch (format)
			{
				case TextureFormat.RedGreenBlueAlpha32:
				case TextureFormat.RedGreen32:
				case TextureFormat.Red32:
					return PixelType.Float;
				case TextureFormat.RedGreenBlueAlpha8:
				case TextureFormat.RedGreen8:
				case TextureFormat.Red8:
					return PixelType.UnsignedByte;
				case TextureFormat.Depth16:
					return PixelType.Float;
				case TextureFormat.Depth24:
					return PixelType.Float;
				case TextureFormat.Depth32:
					return PixelType.Float;
				case TextureFormat.Depth24Stencil8:
					return PixelType.UnsignedInt248;
				case TextureFormat.Depth32Stencil8:
					return PixelType.Float32UnsignedInt248Rev;
				default:
					throw new ArgumentException("Invalid texture format.", "format");
			}
		}
	}
}
