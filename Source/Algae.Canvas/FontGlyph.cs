using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommaExcess.Algae;
using CommaExcess.Algae.Graphics;

namespace CommaExcess.Hologine.Graphics
{
	/// <summary>
	/// A character in a font.
	/// </summary>
	public class FontGlyph
	{
		/// <summary>
		/// Gets the character this glyph represents.
		/// </summary>
		public char Character
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the width of the glyph, in pixels.
		/// </summary>
		public int Width
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the height of the glyph, in pixels.
		/// </summary>
		public int Height
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the advance of the glyph, relative to the font size.
		/// </summary>
		public float Advance
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the path.
		/// </summary>
		public Path Path
		{
			get;
			private set;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="c">The character this glyph represents.</param>
		/// <param name="path">The path that represents the glyph.</param>
		/// <param name="advance">The advance.</param>
		public FontGlyph(char c, Path path, float advance)
		{
			Character = c;
			Path = path;
			Advance = advance;
		}
	}
}
