using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommaExcess.Algae;

namespace CommaExcess.Hologine.Graphics
{
	/// <summary>
	/// A simple buffered text builder.
	/// </summary>
	public class SimpleBufferedTextBuilder : BufferedTextBuilder
	{
		Vector2 scale = Vector2.One;

		/// <summary>
		/// Gets or sets the scale of the text.
		/// </summary>
		/// <remarks>
		/// The new value only affects text buffered after the change.
		/// </remarks>
		public Vector2 Scale
		{
			get { return scale; }
			set { scale = value; }
		}

		Vector2 position = Vector2.Zero;

		/// <summary>
		/// Gets or sets the current position of the text. This value is updated as text is written.
		/// </summary>
		/// <remarks>
		/// The new value only affects text buffered after the change.
		/// </remarks>
		public Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}

		float rotation = 0.0f;

		/// <summary>
		/// Gets or sets the rotation of the text.
		/// </summary>
		/// <remarks>
		/// The new value only affects text buffered after the change.
		/// </remarks>
		public float Rotation
		{
			get { return rotation; }
			set { rotation = value; }
		}

		float fontSize = 12.0f;

		/// <summary>
		/// Gets or sets the font size, in rendering units (generally pixels).
		/// </summary>
		public float FontSize
		{
			get { return fontSize; }
			set { fontSize = value; }
		}

		float lineHeight = 1.0f;

		/// <summary>
		/// Gets or sets the line height, as a percentage of font size.
		/// </summary>
		public float LineHeight
		{
			get { return lineHeight; }
			set { lineHeight = value; }
		}

		Color color = Color.White;

		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <remarks>
		/// The new value only affects text buffered after the change.
		/// </remarks>
		public Color Color
		{
			get { return color; }
			set { color = value; }
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public SimpleBufferedTextBuilder()
		{
			// Nothing.
		}

		/// <summary>
		/// Inherited from BufferedTextBuilder.
		/// </summary>
		/// <remarks>
		/// This does not reset the line height, font size, or color.
		/// </remarks>
		public override void Reset()
		{
			scale = Vector2.One;
			position = Vector2.Zero;
			rotation = 0.0f;
		}

		Matrix GenerateTransform(float scaleFudge)
		{
			return Matrix.Translation(new Vector3(Position.X, Position.Y, 0.0f)) * Matrix.Scale(new Vector3(Scale.X * scaleFudge, Scale.Y * scaleFudge, 1.0f)) * Matrix.Rotation(Vector3.UnitZ, Rotation);
		}

		/// <summary>
		/// Implementation of BufferedTextBuilder.
		/// </summary>
		public override void BufferText(BufferedText buffer, string text)
		{
			float fontSizeFudgeFactor = FontSize / buffer.Font.FontSize;

			for(int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				FontGlyph glyph = buffer.Font[c];

				if (!Char.IsWhiteSpace(c))
				{
					buffer.EmitGlyph(c, GenerateTransform(fontSizeFudgeFactor), Color);
				}

				// Increase the position.
				position.X += glyph.Advance * fontSizeFudgeFactor;

				// Add kerning.
				if (i < text.Length - 1)
				{
					float kerning = buffer.Font.GetKerning(c, text[i + 1]) * fontSizeFudgeFactor;
					position.X += kerning;
				}
			}
		}

		/// <summary>
		/// Moves on to the next line.
		/// </summary>
		public void NextLine()
		{
			position.Y += LineHeight;
		}

		/// <summary>
		/// Moves on to the next line and resets the row.
		/// </summary>
		/// <param name="x">The new starting position of text.</param>
		public void NextLine(float x)
		{
			position.X = x;
			NextLine();
		}
	}
}
