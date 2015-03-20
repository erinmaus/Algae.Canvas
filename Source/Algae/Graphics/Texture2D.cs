using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// Defines a 2D texture.
	/// </summary>
	public class Texture2D : Texture
	{
		ITexture2D texture;

		/// <summary>
		/// Gets the width of the texture.
		/// </summary>
		public int Width
		{
			get { return texture.Width; }
		}

		/// <summary>
		/// Gets the height of the texture.
		/// </summary>
		public int Height
		{
			get { return texture.Height; }
		}

		internal Texture2D(ITexture2D texture)
			: base(TextureDimensions.Two)
		{
			this.texture = texture;
			InternalTexture = texture;
		}

		/// <summary>
		/// Creates a texture with the provided parameters.
		/// </summary>
		/// <param name="renderer">The renderer.</param>
		/// <param name="width">The width of the texture.</param>
		/// <param name="height">The height of the texture.</param>
		/// <param name="format">The texture format.</param>
		public Texture2D(Renderer renderer, int width, int height, TextureFormat format)
			: base(TextureDimensions.Two)
		{
			texture = renderer.CreateTexture2D(width, height, format);
			InternalTexture = texture;
		}

		/// <summary>
		/// Implementation of IDisposable.
		/// </summary>
		public override void Dispose()
		{
			texture.Dispose();
		}

#if DEBUG
		public void SaveDepth(string filename)
		{
			float[] data = new float[Width * Height];
			GetData(data, TextureFormat.Depth32);
			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(Width, Height);

			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					int c = (int)(data[y * Width + x] * 255);
					bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(c, c, c));
				}
			}

			bitmap.Save(filename);
		}

		public void Save(string filename)
		{
			Color[] data = new Color[Width * Height];
			GetData(data, TextureFormat.RedGreenBlueAlpha32);

			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(Width, Height);

			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					int r = (int)(data[y * Width + x].Red * 255);
					int g = (int)(data[y * Width + x].Green * 255);
					int b = (int)(data[y * Width + x].Blue * 255);
					int a = (int)(data[y * Width + x].Alpha * 255);

					bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(a, r, g, b));
				}
			}

			bitmap.Save(filename);
		}
#endif
	}
}
