using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae
{
	/// <summary>
	/// A structure that holds the components for a RGBA color as floating point numbers.
	/// </summary>
	public struct Color
	{
		/// <summary>
		/// Gets or sets the red component.
		/// </summary>
		public float Red;

		/// <summary>
		/// Gets or sets the green component.
		/// </summary>
		public float Green;

		/// <summary>
		/// Gets or sets the blue component.
		/// </summary>
		public float Blue;

		/// <summary>
		/// Gets or sets the alpha component.
		/// </summary>
		public float Alpha;

		/// <summary>
		/// Gets a color with the RGBA values of (1, 1, 1, 1).
		/// </summary>
		public static readonly Color White = new Color(1.0f, 1.0f, 1.0f, 1.0f);

		/// <summary>
		/// Gets a color with the RGBA values of (0, 0, 0, 0).
		/// </summary>
		public static readonly Color Black = new Color(0.0f, 0.0f, 0.0f, 1.0f);

		/// <summary>
		/// Constructs a color from the traditional RGB triplet.
		/// </summary>
		/// <param name="red">The red component.</param>
		/// <param name="green">The green component.</param>
		/// <param name="blue">The blue component.</param>
		public Color(float red, float green, float blue)
			: this(red, green, blue, 1.0f)
		{
		}

		/// <summary>
		/// Constructs a color from the traditional RGB triplet with alpha.
		/// </summary>
		/// <param name="red">The red component.</param>
		/// <param name="green">The green component.</param>
		/// <param name="blue">The blue component.</param>
		/// <param name="alpha">The alpha component.</param>
		public Color(float red, float green, float blue, float alpha)
		{
			Red = red;
			Green = green;
			Blue = blue;
			Alpha = alpha;
		}

		/// <summary>
		/// Constructs a color from a previous color and a different alpha.
		/// </summary>
		/// <param name="color">The base color.</param>
		/// <param name="alpha">The new alpha.</param>
		public Color(Color color, float alpha)
		{
			Red = color.Red;
			Green = color.Green;
			Blue = color.Blue;
			Alpha = alpha;
		}

		/// <summary>
		/// Parses a hex color in the form of RGB or RRGGBB.
		/// </summary>
		/// <param name="color">The color string to parse.</param>
		/// <returns>The parsed color.</returns>
		public static Color ParseCss(string color)
		{
			string r, g, b;

			if (color.Length == 3)
			{
				r = color.Substring(0, 1) + color.Substring(0, 1);
				g = color.Substring(1, 1) + color.Substring(1, 1);
				b = color.Substring(2, 1) + color.Substring(2, 1);
			}
			else
			{
				r = color.Substring(0, 2);
				g = color.Substring(2, 2);
				b = color.Substring(4, 2);
			}

			return new Color
			(
				Int32.Parse(r, System.Globalization.NumberStyles.HexNumber) / 255.0f,
				Int32.Parse(g, System.Globalization.NumberStyles.HexNumber) / 255.0f,
				Int32.Parse(b, System.Globalization.NumberStyles.HexNumber) / 255.0f
			);
		}

		/// <summary>
		/// Linearly interpolates two values.
		/// </summary>
		/// <param name="from">The from value.</param>
		/// <param name="to">The to value.</param>
		/// <param name="mu">The delta.</param>
		/// <returns>The interpolated color.</returns>
		public static Color Lerp(Color from, Color to, float mu)
		{
			return new Color
			(
				MathHelper.Lerp(from.Red, to.Red, mu),
				MathHelper.Lerp(from.Green, to.Green, mu),
				MathHelper.Lerp(from.Blue, to.Blue, mu),
				MathHelper.Lerp(from.Alpha, to.Alpha, mu)
			);
		}

		public static Color operator *(Color left, Color right)
		{
			return new Color(left.Red * right.Red, left.Green * right.Green, left.Blue * right.Blue, left.Alpha * right.Alpha);
		}

		/// <summary>
		/// Converts the color to a Vector3, disregarding the alpha.
		/// </summary>
		/// <returns>The Vector3.</returns>
		public Vector3 ToVector3()
		{
			return new Vector3(Red, Green, Blue);
		}

		/// <summary>
		/// Converts the color to a Vector4.
		/// </summary>
		/// <returns>The Vector4.</returns>
		public Vector4 ToVector4()
		{
			return new Vector4(Red, Green, Blue, Alpha);
		}

		/// <summary>
		/// Prettifies the color.
		/// </summary>
		/// <returns>The color as a string.</returns>
		public override string ToString()
		{
			return String.Format("{0}, {1}, {2}, {3}", Math.Floor(Red * 255), Math.Floor(Green * 255), Math.Floor(Blue * 255), Math.Floor(Alpha * 255));
		}
	}
}
