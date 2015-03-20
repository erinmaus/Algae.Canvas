using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Graphics.OpenGL;

namespace CommaExcess.Algae.Graphics
{
	class GL3Texture2D : GL3Texture, ITexture2D
	{
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

		public GL3Texture2D(int width, int height, TextureFormat format)
			: base(format, TextureTarget.Texture2D)
		{
			Width = width;
			Height = height;

			// Generate texture.
			TextureID = GL.GenTexture();
			
			// Initialize the texture.
			SetData<Color>(null, format);

			// Set some sane default values.
			Repeat = TextureRepeatMode.Clamp;
			Filter = TextureFilterMode.Linear;

			// Set sane default depth texture values.
			if (IsDepthTexture)
			{
				DepthMode = TextureDepthMode.CompareToRef;
				DepthFunction = TextureDepthFunction.LessEqual;
			}
		}

		public override void SetData<T>(T[] data, TextureFormat format, int level = 0)
		{
			// Buffer data.
			Bind(0);
			GL.TexImage2D(TextureTarget.Texture2D, level, GetInternalFormat(format), Width, Height, 0, GetComponents(format), GetComponentType(format), data);
		}

		public override void GetData<T>(T[] data, TextureFormat format, int level = 0)
		{
			// Fetch data.
			Bind(0);
			GL.GetTexImage(TextureTarget.Texture2D, level, GetComponents(format), GetComponentType(format), data);
		}

		public void Dispose()
		{
			GL.DeleteTexture(TextureID);
		}
	}
}
