using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using FreeType = SharpFont;

using CommaExcess.Algae;
using CommaExcess.Algae.Graphics;

using BinaryReader = System.IO.BinaryReader;
using Stream = System.IO.Stream;

namespace CommaExcess.Hologine.Graphics
{
	/// <summary>
	/// Internal class. Used to construct fonts.
	/// </summary>
	class FontBuilder : IDisposable
	{
		FreeType.Library library;
		FreeType.Face face;

		// The size of the font, in pixels.
		int size;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="size">The size of the font, in pixels.</param>
		public FontBuilder(Stream stream, int size)
		{
			this.size = size;

			library = new FreeType.Library();
			using (BinaryReader reader = new BinaryReader(stream))
			{
				face = new FreeType.Face(library, reader.ReadBytes((int)stream.Length), 0);
			}

			face.SetPixelSizes(0, (uint)size);
		}

		/// <summary>
		/// Builds a glyph.
		/// </summary>
		/// <param name="c">The character representing the glyph to build.</param>
		/// <returns>The glyph.</returns>
		public FontGlyph BuildGlyph(char c)
		{
			uint index = face.GetCharIndex(c);

			face.LoadGlyph(index, FreeType.LoadFlags.NoBitmap, FreeType.LoadTarget.Normal);

			Path path = new Path() { FillRule = CanvasPathFillRule.FontNonZero };
			float shift = 1.0f / 64.0f;

			// Helper method to convert a FTVector to a Vector2, considering the shift and scalar values.
			Func<FreeType.FTVector, Vector2> convert = v => new Vector2(v.X * shift, v.Y * shift);

			face.Glyph.Outline.Decompose(new FreeType.OutlineFuncs()
			{
				MoveFunction = (end, user) =>
				{
					path.MoveTo(convert(end));

					return 0;
				},

				LineFunction = (end, user) =>
				{
					path.LineTo(convert(end));

					return 0;
				},

				ConicFunction = (control, end, user) =>
				{
					path.QuadraticCurveTo(convert(control), convert(end));

					return 0;
				},

				CubicFunction = (control1, control2, end, user) =>
				{
					path.CubicCurveTo(convert(control1), convert(control2), convert(end));

					return 0;
				}
			}, IntPtr.Zero);

			path.End();

			return new FontGlyph(c, path, face.Glyph.Advance.X * shift);
		}

		/// <summary>
		/// Gets the kerning value.
		/// </summary>
		/// <param name="a">The left character.</param>
		/// <param name="b">The right character.</param>
		/// <returns>The kerning value.</returns>
		public float GetKerning(char a, char b)
		{
			return face.GetKerning(a, b, FreeType.KerningMode.Default).X / 64.0f;
		}

		/// <summary>
		/// Disposes of all resources allocated by the font builder.
		/// </summary>
		public void Dispose()
		{
			face.Dispose();
			library.Dispose();
		}
	}
}
