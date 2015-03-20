using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommaExcess.Algae;
using CommaExcess.Algae.Graphics;

namespace CommaExcess.Hologine.Graphics
{
	/// <summary>
	/// Buffers text!
	/// </summary>
	public class BufferedText
	{
		struct BufferedGlyph
		{
			public char Glyph;
			public Path Path;
			public Matrix Transform;
			public Color Color;
		}

		// List of glyphs with rendering data.
		List<BufferedGlyph> glyphs = new List<BufferedGlyph>();

		// The font to extract glyphs from.
		Font font;

		/// <summary>
		/// Gets the font that the buffered text renderer uses.
		/// </summary>
		public Font Font
		{
			get { return font; }
		}

		/// <summary>
		/// Creates a buffered text.
		/// </summary>
		public BufferedText(Font font)
		{
			this.font = font;
		}

		/// <summary>
		/// Emits a glyph. Used by a buffered text builder.
		/// </summary>
		/// <param name="glyph">The glyph.</param>
		/// <param name="transform">The transform of the glyph.</param>
		/// <param name="color">The color.</param>
		public void EmitGlyph(char glyph, Matrix transform, Color color)
		{
			glyphs.Add(new BufferedGlyph()
			{
				Glyph = glyph,
				Path = Font[glyph].Path,
				Transform = transform,
				Color = color
			});
		}

		/// <summary>
		/// Resets the internal state of the buffered text.
		/// </summary>
		public void Reset()
		{
			glyphs.Clear();
		}

		/// <summary>
		/// Renders the text into a canvas.
		/// </summary>
		/// <param name="canvas">The canvas to render into.</param>
		public void Draw(Canvas canvas)
		{
			for (int i = 0; i < glyphs.Count; i++)
			{
				canvas.Paint(glyphs[i].Path, glyphs[i].Color, glyphs[i].Transform);
			}
		}
	}
}
