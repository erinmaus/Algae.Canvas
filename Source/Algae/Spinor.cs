using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae
{
	/// <summary>
	/// Represents a rotation in 2D.
	/// </summary>
	public struct Spinor
	{
		/// <summary>
		/// The first dimension.
		/// </summary>
		public float X;

		/// <summary>
		/// The second dimension.
		/// </summary>
		public float Y;

		/// <summary>
		/// Constructs a spinor from an angle.
		/// </summary>
		/// <param name="angle">The angle, in radians.</param>
		public Spinor(float angle)
		{
			X = (float)Math.Cos(angle * 0.5f);
			Y = (float)Math.Sin(angle * 0.5f);
		}

		/// <summary>
		/// Constructs a spinor from its parts.
		/// </summary>
		/// <param name="x">The first part.</param>
		/// <param name="y">The second part.</param>
		public Spinor(float x, float y)
		{
			X = x;
			Y = y;
		}

		/// <summary>
		/// Calculates the length squared.
		/// </summary>
		/// <returns>The length squared.</returns>
		public float LengthSquared()
		{
			return X * X + Y * Y;
		}

		/// <summary>
		/// Calculates the length.
		/// </summary>
		/// <returns>The length.</returns>
		public float Length()
		{
			return (float)Math.Sqrt(LengthSquared());
		}

		/// <summary>
		/// Converts the spinor to an angle.
		/// </summary>
		/// <returns>The angle, in radians.</returns>
		public float ToAngle()
		{
			return (float)Math.Atan2(Y, X) * 2.0f;
		}

		/// <summary>
		/// Multiplies two spinors.
		/// </summary>
		/// <param name="a">The left spinor.</param>
		/// <param name="b">The right spinor.</param>
		/// <returns>The result.</returns>
		public static Spinor operator *(Spinor a, Spinor b)
		{
			return new Spinor(a.X * b.X - a.Y * b.Y, a.X * b.Y + a.Y * b.X);
		}

		/// <summary>
		/// Negates a spinor.
		/// </summary>
		/// <param name="spinor">The spinor to negate.</param>
		/// <returns>The negated spinor.</returns>
		public static Spinor operator -(Spinor spinor)
		{
			Spinor ret = new Spinor(spinor.X, -spinor.Y);
			float length = ret.Length();

			ret.X *= length;
			ret.Y *= length;

			return ret;
		}

		/// <summary>
		/// Linearly interpolates a spinor.
		/// </summary>
		/// <param name="from">The start spinor.</param>
		/// <param name="to">The end spinor.</param>
		/// <param name="mu">The delta value.</param>
		/// <returns>The interpolated spinor.</returns>
		public static Spinor Lerp(Spinor from, Spinor to, float mu)
		{
			return new Spinor(from.X + (to.X - from.X) * mu, from.Y + (to.Y - from.Y) * mu);
		}

		/// <summary>
		/// Spherically interpolates a spinor.
		/// </summary>
		/// <param name="from">The start spinor.</param>
		/// <param name="to">The end spinor.</param>
		/// <param name="mu">The delta value.</param>
		/// <returns>The interpolated spinor.</returns>
		public static Spinor Slerp(Spinor from, Spinor to, float mu)
		{
			float cosom = from.X * to.X + from.Y * to.Y;

			float tr, ti;
			if (cosom < 0.0f)
			{
				cosom = -cosom;
				tr = -to.X;
				ti = -to.Y;
			}
			else
			{
				tr = to.X;
				ti = to.Y;
			}

			float s1, s2;
			if (1.0f - cosom > Single.Epsilon)
			{
				float omega = (float)Math.Acos(cosom);
				float sinom = (float)Math.Sqrt(1.0f - cosom * cosom);

				s1 = (float)Math.Sin((1.0f - mu) * omega) / sinom;
				s2 = (float)Math.Sin(mu * omega) / sinom;
			}
			else
			{
				s1 = 1.0f - mu;
				s2 = mu;
			}

			return new Spinor(s1 * from.X + s2 * tr, s1 * from.Y + s2 * ti);
		}
	}
}
