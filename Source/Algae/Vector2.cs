using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae
{
	/// <summary>
	/// A two-dimensional vector.
	/// </summary>
	public struct Vector2
	{
		/// <summary>
		/// The first coordinate.
		/// </summary>
		public float X;

		/// <summary>
		/// The second coordinate.
		/// </summary>
		public float Y;

		static readonly Vector2 zero = new Vector2(0.0f, 0.0f);

		/// <summary>
		/// Represents (0, 0).
		/// </summary>
		public static Vector2 Zero
		{
			get { return zero; }
		}

		static readonly Vector2 one = new Vector2(1.0f, 1.0f);

		/// <summary>
		/// Represents (1, 1).
		/// </summary>
		public static Vector2 One
		{
			get { return one; }
		}

		static readonly Vector2 unitX = new Vector2(1.0f, 0.0f);

		/// <summary>
		/// Represents (1, 0).
		/// </summary>
		public static Vector2 UnitX
		{
			get { return unitX; }
		}

		static readonly Vector2 unitY = new Vector2(0.0f, 1.0f);

		/// <summary>
		/// Represents (0, 1).
		/// </summary>
		public static Vector2 UnitY
		{
			get { return unitY; }
		}

		/// <summary>
		/// Creates a vector from two components.
		/// </summary>
		/// <param name="x">The first component.</param>
		/// <param name="y">The second component.</param>
		public Vector2(float x, float y)
		{
			X = x;
			Y = y;
		}

		/// <summary>
		/// Creates a vector from a scalar.
		/// </summary>
		/// <param name="scalar">The scalar value to set both components to.</param>
		public Vector2(float scalar)
		{
			X = scalar;
			Y = scalar;
		}

		/// <summary>
		/// Calculates the length of the vector, squared.
		/// </summary>
		/// <returns>The squared length.</returns>
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
		/// Adds two vectors together.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <returns>The sum.</returns>
		public static Vector2 Add(Vector2 left, Vector2 right)
		{
			Vector2 result;

			Add(ref left, ref right, out result);

			return result;
		}

		/// <summary>
		/// Adds two vectors together.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <param name="result">The result</param>
		/// <returns>The sum.</returns>
		public static void Add(ref Vector2 left, ref Vector2 right, out Vector2 result)
		{
			result = new Vector2(left.X + right.X, left.Y + right.Y);
		}

		/// <summary>
		/// Adds two vectors together.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <returns>The sum.</returns>
		public static Vector2 operator +(Vector2 left, Vector2 right)
		{
			return Add(left, right);
		}

		/// <summary>
		/// Subtracts two vectors.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <returns>The difference.</returns>
		public static Vector2 Subtract(Vector2 left, Vector2 right)
		{
			Vector2 result;

			Subtract(ref left, ref right, out result);

			return result;
		}

		/// <summary>
		/// Subtracts two vectors.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <param name="result">The result.</param>
		/// <returns>The difference.</returns>
		public static void Subtract(ref Vector2 left, ref Vector2 right, out Vector2 result)
		{
			result = new Vector2(left.X - right.X, left.Y - right.Y);
		}

		/// <summary>
		/// Subtracts two vectors.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <returns>The difference.</returns>
		public static Vector2 operator -(Vector2 left, Vector2 right)
		{
			return Subtract(left, right);
		}

		/// <summary>
		/// Multiplies a vector by a scalar.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The scalar..</param>
		/// <returns>The product.</returns>
		public static Vector2 Multiply(Vector2 left, float right)
		{
			return Multiply(left, new Vector2(right));
		}

		/// <summary>
		/// Multiplies two vectors.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <returns>The product.</returns>
		public static Vector2 Multiply(Vector2 left, Vector2 right)
		{
			Vector2 result;

			Multiply(ref left, ref right, out result);

			return result;
		}

		/// <summary>
		/// Multiplies two vectors.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <param name="result">The result.</param>
		/// <returns>The product.</returns>
		public static void Multiply(ref Vector2 left, ref Vector2 right, out Vector2 result)
		{
			result = new Vector2(left.X * right.X, left.Y * right.Y);
		}

		/// <summary>
		/// Multiplies two vectors.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <returns>The product.</returns>
		public static Vector2 operator *(Vector2 left, Vector2 right)
		{
			return Multiply(left, right);
		}

		/// <summary>
		/// Multiplies a vector by a scalar.
		/// </summary>
		/// <param name="left">The vector.</param>
		/// <param name="right">The scalar.</param>
		/// <returns>The product.</returns>
		/// <remarks>
		/// To remain as a counterpart to the division operation, the scalar
		/// must always on the right side of a multiplication operation.
		/// </remarks>
		public static Vector2 operator *(Vector2 left, float right)
		{
			return Multiply(left, right);
		}

		/// <summary>
		/// Divides a vector by a scalar.
		/// </summary>
		/// <param name="left">The vector.</param>
		/// <param name="right">The scalar.</param>
		/// <returns>The quotient.</returns>
		public static Vector2 Divide(Vector2 left, float right)
		{
			return Divide(left, new Vector2(right));
		}

		/// <summary>
		/// Divides two vectors.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <returns>The quotient.</returns>
		public static Vector2 Divide(Vector2 left, Vector2 right)
		{
			Vector2 result;

			Divide(ref left, ref right, out result);

			return result;
		}

		/// <summary>
		/// Divides two vectors.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <param name="result">The result.</param>
		/// <returns>The quotient.</returns>
		public static void Divide(ref Vector2 left, ref Vector2 right, out Vector2 result)
		{
			result = new Vector2(left.X / right.X, left.Y / right.Y);
		}

		/// <summary>
		/// Divides two vectors.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <returns>The quotient.</returns>
		public static Vector2 operator /(Vector2 left, Vector2 right)
		{
			return Divide(left, right);
		}
		
		/// <summary>
		/// Divides a vector by a scalar.
		/// </summary>
		/// <param name="left">The vector.</param>
		/// <param name="right">The scalar.</param>
		/// <returns>The quotient.</returns>
		public static Vector2 operator /(Vector2 left, float right)
		{
			return Divide(left, right);
		}

		/// <summary>
		/// Negates a vector.
		/// </summary>
		/// <param name="vector">The vector to negate.</param>
		/// <returns>The negated value.</returns>
		public static Vector2 operator -(Vector2 vector)
		{
			return new Vector2(-vector.X, -vector.Y);
		}

		/// <summary>
		/// Rotates a vector by an angle.
		/// </summary>
		/// <param name="vector">The vector to rotate.</param>
		/// <param name="angle">The angle of rotation.</param>
		/// <returns>The rotated vector.</returns>
		public static Vector2 Rotate(Vector2 vector, float angle)
		{
			float ca = (float)Math.Cos(angle);
			float sa = (float)Math.Sin(angle);

			return new Vector2(vector.X * ca - vector.Y * sa, vector.Y * ca + vector.X * sa);
		}

		/// <summary>
		/// Transforms a vector by a matrix, where the third component is treated as zero.
		/// </summary>
		/// <param name="vector">The vector.</param>
		/// <param name="matrix">The matrix.</param>
		/// <returns>The result of the transformation.</returns>
		public static Vector2 Transform(Vector2 vector, Matrix matrix)
		{
			return new Vector2
			(
				vector.X * matrix.M11 + vector.Y * matrix.M12 + matrix.M14,
				vector.X * matrix.M21 + vector.Y * matrix.M22 + matrix.M24
			);
		}

		/// <summary>
		/// Transforms a vector by a spinor.
		/// </summary>
		/// <param name="vector">The vector.</param>
		/// <param name="spinor">The spinor.</param>
		/// <returns>The transformed vector.</returns>
		public static Vector2 Transform(Vector2 vector, Spinor spinor)
		{
			Spinor v = new Spinor(vector.X, vector.Y);
			Spinor r = spinor * v * -spinor;

			return new Vector2(r.X, r.Y);
		}

		/// <summary>
		/// Calculates the normal of the provided vector.
		/// </summary>
		/// <param name="vector">The vector.</param>
		/// <returns>The normal.</returns>
		public static Vector2 Normalize(Vector2 vector)
		{
			float length = vector.Length();

			if (length == 0.0f)
				return vector;

			return vector / length;
		}

		/// <summary>
		/// Calculates the dot product of the provided vectors.
		/// </summary>
		/// <param name="a">The first vector.</param>
		/// <param name="b">The second vector.</param>
		/// <returns>The dot product.</returns>
		public static float Dot(Vector2 a, Vector2 b)
		{
			return a.X * b.X + a.Y * b.Y;
		}

		/// <summary>
		/// Calculates the cross product of the provided vectors.
		/// </summary>
		/// <param name="a">The first vector.</param>
		/// <param name="b">The second vector.</param>
		/// <returns>The cross product.</returns>
		/// <remarks>This is the same as (Ax, Ay, 0) x (Bx, By, 0).</remarks>
		public static float Cross(Vector2 a, Vector2 b)
		{
			return a.X * b.Y - a.Y * b.X;
		}

		/// <summary>
		/// Gets a vector that is perpindicular.
		/// </summary>
		/// <param name="vector">The vector.</param>
		/// <returns>The perpindicular vector.</returns>
		public static Vector2 Perpendicular(Vector2 vector)
		{
			return new Vector2(-vector.Y, vector.X);
		}

		/// <summary>
		/// Linearly interpolates two vectors.
		/// </summary>
		/// <param name="from">The start.</param>
		/// <param name="to">The end.</param>
		/// <param name="mu">The delta.</param>
		/// <returns>The interpolated vector.</returns>
		public static Vector2 Lerp(Vector2 from, Vector2 to, float mu)
		{
			return from + (to - from) * mu;
		}

		/// <summary>
		/// Compares two vectors for equality.
		/// </summary>
		/// <param name="a">The first vector.</param>
		/// <param name="b">The second vector.</param>
		/// <returns>True if the vectors are equal, false otherwise.</returns>
		public static bool operator ==(Vector2 a, Vector2 b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		/// <summary>
		/// Compares two vectors for inequality.
		/// </summary>
		/// <param name="a">The first vector.</param>
		/// <param name="b">The second vector.</param>
		/// <returns>True if the vectors are not equal, false otherwise.</returns>
		public static bool operator !=(Vector2 a, Vector2 b)
		{
			return !(a == b);
		}

		/// <summary>
		/// Compares two vectors.
		/// </summary>
		/// <param name="obj">The object to compare against.</param>
		/// <returns>Ttrue if the objects are equal, false otherwise.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector2)
			{
				Vector2 b = (Vector2)obj;

				return this == b;
			}

			return false;
		}

		/// <summary>
		/// Calculates the hash code.
		/// </summary>
		/// <returns>The hash code.</returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode();
		}

		/// <summary>
		/// Converts the vector to a string.
		/// </summary>
		/// <returns>The string.</returns>
		public override string ToString()
		{
			return String.Format("({0}, {1})", X, Y);
		}
	}
}
