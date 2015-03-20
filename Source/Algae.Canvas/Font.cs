using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommaExcess.Algae;
using CommaExcess.Algae.Graphics;

using Stream = System.IO.Stream;

namespace CommaExcess.Hologine.Graphics
{
	/// <summary>
	/// A font. What else is there to say?
	/// </summary>
	public class Font : IDisposable
	{
		// Glyphs loaded by the font.
		Dictionary<char, FontGlyph> glyphs = new Dictionary<char, FontGlyph>();

		// Kerning values.
		Dictionary<Tuple<char, char>, float> kerning = new Dictionary<Tuple<char, char>, float>();

		// The font builder.
		FontBuilder builder;

		/// <summary>
		/// Gets the font size.
		/// </summary>
		public int FontSize
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the glyph that represents the provided character.
		/// </summary>
		/// <param name="index">The provided character.</param>
		/// <returns>The glyph, if found; null otherwise.</returns>
		public FontGlyph this[char index]
		{
			get
			{
				FontGlyph glyph;

				// If it's not added, try and load it from the font.
				if (!glyphs.TryGetValue(index, out glyph))
				{
					glyph = builder.BuildGlyph(index);

					// Urgh.
					if (glyph != null)
						glyphs.Add(index, glyph);
				}

				return glyph;
			}
		}

		/// <summary>
		/// Constructs an empty font.
		/// </summary>
		public Font()
		{
			// Nothing.
		}

		/// <summary>
		/// Gets the kerning value.
		/// </summary>
		/// <param name="a">The left character.</param>
		/// <param name="b">The right character.</param>
		/// <returns>The kerning value.</returns>
		public float GetKerning(char a, char b)
		{
			Tuple<char, char> t = new Tuple<char, char>(a, b);
			float k;

			if (!kerning.TryGetValue(t, out k))
			{
				k = builder.GetKerning(a, b);

				kerning.Add(t, k);
			}

			return k;
		}

		/// <summary>
		/// Measures the width of the provided text.
		/// </summary>
		/// <param name="text">The text to measure.</param>
		/// <returns>The width. Keep in mind this will be in unit space. Multiply by font size to get the real value.</returns>
		public float Measure(string text)
		{
			float width = 0.0f;

			for (int i = 0; i < text.Length; i++)
			{
				FontGlyph glyph = this[text[i]];
				width += glyph.Advance;

				if (i < text.Length - 1)
					width += GetKerning(text[i], text[i + 1]);
			}

			return width;
		}

		/// <summary>
		/// Loads the font. Keep in mind the stream is managed by the font.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="fontSize">The font size.</param>
		public void Load(Stream stream, int fontSize = 64)
		{
			Reset();

			builder = new FontBuilder(stream, fontSize);
			FontSize = fontSize;

			// The stream isn't used anymore.
			// FontBuilder copies the entire file into memory.
			// TODO: Is that a waste of memory?
			stream.Dispose();
		}

		void Reset()
		{
			if (builder != null)
			{
				builder.Dispose();
				builder = null;
			}

			foreach (var glyph in glyphs)
			{
				Path path = glyph.Value.Path;

				if (path != null)
					path.Dispose();
			}
		}

		/// <summary>
		/// Disposes of all resources allocated by the font.
		/// </summary>
		public void Dispose()
		{
			Reset();
		}
	}
}
