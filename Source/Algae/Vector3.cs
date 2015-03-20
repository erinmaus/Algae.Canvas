using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae
{
	/// <summary>
	/// A three-dimensional vector.
	/// </summary>
	public struct Vector3
	{
		/// <summary>
		/// The first coordinate.
		/// </summary>
		public float X;

		/// <summary>
		/// The second coordinate.
		/// </summary>
		public float Y;

		/// <summary>
		/// The third coordinate.
		/// </summary>
		public float Z;

		static readonly Vector3 unitX = new Vector3(1.0f, 0.0f, 0.0f);

		/// <summary>
		/// Represents (1, 0, 0).
		/// </summary>
		public static Vector3 UnitX
		{
			get { return unitX; }
		}

		static readonly Vector3 unitY = new Vector3(0.0f, 1.0f, 0.0f);

		/// <summary>
		/// Represents (0, 1, 0).
		/// </summary>
		public static Vector3 UnitY
		{
			get { return unitY; }
		}

		static readonly Vector3 unitZ = new Vector3(0.0f, 0.0f, 1.0f);

		/// <summary>
		/// Represents (0, 0, 1).
		/// </summary>
		public static Vector3 UnitZ
		{
			get { return unitZ; }
		}

		static readonly Vector3 zero = new Vector3(0.0f, 0.0f, 0.0f);

		/// <summary>
		/// Represents (0, 0, 0).
		/// </summary>
		public static Vector3 Zero
		{
			get { return zero; }
		}

		static readonly Vector3 one = new Vector3(1.0f, 1.0f, 1.0f);

		/// <summary>
		/// Represents (1, 1, 1).
		/// </summary>
		public static Vector3 One
		{
			get { return one; }
		}

		/// <summary>
		/// Constructs a vector from three components.
		/// </summary>
		/// <param name="x">The first component.</param>
		/// <param name="y">The second component.</param>
		/// <param name="z">The third component.</param>
		public Vector3(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		/// <summary>
		/// Constructs a vector from a scalar.
		/// </summary>
		/// <param name="scalar">The value to set all coordinates to.</param>
		public Vector3(float scalar)
		{
			X = scalar;
			Y = scalar;
			Z = scalar;
		}

		/// <summary>
		/// Constructs a three-dimensional vector from a two-dimensional one.
		/// </summary>
		/// <param name="v">The two-dimensional vector.</param>
		public Vector3(Vector2 v)
		{
			X = v.X;
			Y = v.Y;
			Z = 0.0f;
		}

		/// <summary>
		/// Calculates the length of the vector, squared.
		/// </summary>
		/// <returns>The squared length.</returns>
		public float LengthSquared()
		{
			return X * X + Y * Y + Z * Z;
		}

		/// <summary>
		/// Calculates the length of the vector.
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
		public static Vector3 Add(Vector3 left, Vector3 right)
		{
			Vector3 result;

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
		public static void Add(ref Vector3 left, ref Vector3 right, out Vector3 result)
		{
			result = new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}

		/// <summary>
		/// Adds two vectors together.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <returns>The sum.</returns>
		public static Vector3 operator +(Vector3 left, Vector3 right)
		{
			return Add(left, right);
		}

		/// <summary>
		/// Subtracts two vectors.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <returns>The difference.</returns>
		public static Vector3 Subtract(Vector3 left, Vector3 right)
		{
			Vector3 result;

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
		public static void Subtract(ref Vector3 left, ref Vector3 right, out Vector3 result)
		{
			result = new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}

		/// <summary>
		/// Subtracts two vectors.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <returns>The difference.</returns>
		public static Vector3 operator -(Vector3 left, Vector3 right)
		{
			return Subtract(left, right);
		}

		/// <summary>
		/// Multiplies a vector by a scalar.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The scalar.</param>
		/// <returns>The product.</returns>
		public static Vector3 Multiply(Vector3 left, float right)
		{
			return Multiply(left, new Vector3(right));
		}

		/// <summary>
		/// Multiplies two vectors.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <returns>The product.</returns>
		public static Vector3 Multiply(Vector3 left, Vector3 right)
		{
			Vector3 result;

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
		public static void Multiply(ref Vector3 left, ref Vector3 right, out Vector3 result)
		{
			result = new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}

		/// <summary>
		/// Multiplies two vectors.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <returns>The product.</returns>
		public static Vector3 operator *(Vector3 left, Vector3 right)
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
		public static Vector3 operator *(Vector3 left, float right)
		{
			return Multiply(left, right);
		}

		/// <summary>
		/// Divides a vector by a scalar.
		/// </summary>
		/// <param name="left">The vector.</param>
		/// <param name="right">The scalar.</param>
		/// <returns>The quotient.</returns>
		public static Vector3 Divide(Vector3 left, float right)
		{
			return Divide(left, new Vector3(right));
		}

		/// <summary>
		/// Divides two vectors.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <returns>The quotient.</returns>
		public static Vector3 Divide(Vector3 left, Vector3 right)
		{
			Vector3 result;

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
		public static void Divide(ref Vector3 left, ref Vector3 right, out Vector3 result)
		{
			result = new Vector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
		}

		/// <summary>
		/// Divides two vectors.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <returns>The quotient.</returns>
		public static Vector3 operator /(Vector3 left, Vector3 right)
		{
			return Divide(left, right);
		}

		/// <summary>
		/// Divides two vectors.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <returns>The quotient.</returns>
		public static Vector3 operator /(Vector3 left, float right)
		{
			return Divide(left, right);
		}

		/// <summary>
		/// Calculates the normal of the provided vector.
		/// </summary>
		/// <param name="vector">The vector.</param>
		/// <returns>The normal.</returns>
		public static Vector3 Normalize(Vector3 vector)
		{
			float length = vector.Length();

			if (length != 0)
				return vector / vector.Length();
			else
				return vector;
		}

		/// <summary>
		/// Calculates the dot product of the provided vectors.
		/// </summary>
		/// <param name="a">The first vector.</param>
		/// <param name="b">The second vector.</param>
		/// <returns>The dot product.</returns>
		public static float Dot(Vector3 a, Vector3 b)
		{
			return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
		}

		/// <summary>
		/// Calculates the cross product of the provided vectors.
		/// </summary>
		/// <param name="left">The left vector.</param>
		/// <param name="right">The right vector.</param>
		/// <returns>The cross product.</returns>
		public static Vector3 Cross(Vector3 left, Vector3 right)
		{
			return new Vector3
			(
				left.Y * right.Z - left.Z * right.Y, 
				left.Z * right.X - left.X * right.Z, 
				left.X * right.Y - left.Y * right.X
			);
		}

		/// <summary>
		/// Projects one vector by another.
		/// </summary>
		/// <param name="a">The first vector.</param>
		/// <param name="b">The second vector.</param>
		/// <returns>The result of the projection.</returns>
		public static Vector3 Project(Vector3 a, Vector3 b)
		{
			return b * Vector3.Dot(a, b);
		}

		/// <summary>
		/// Transforms a vector by a matrix.
		/// </summary>
		/// <param name="vector">The vector.</param>
		/// <param name="matrix">The matrix.</param>
		/// <returns>The result of the transformation.</returns>
		public static Vector3 Transform(Vector3 vector, Matrix matrix)
		{
			return new Vector3
			(
				vector.X * matrix.M11 + vector.Y * matrix.M12 + vector.Z * matrix.M13 + matrix.M14,
				vector.X * matrix.M21 + vector.Y * matrix.M22 + vector.Z * matrix.M23 + matrix.M24,
				vector.X * matrix.M31 + vector.Y * matrix.M32 + vector.Z * matrix.M33 + matrix.M34
			);
		}

		/// <summary>
		/// Performs a perspective transformation.
		/// </summary>
		/// <param name="vector">The vector.</param>
		/// <param name="matrix">The matrix.</param>
		/// <returns>The result of the transformation.</returns>
		public static Vector3 TransformPerspective(Vector3 vector, Matrix matrix)
		{
			Vector4 v = Vector4.Transform(new Vector4(vector), matrix);
			float w = 1.0f / v.W;

			return new Vector3(v.X * w, v.Y * w, v.Z * w);
		}

		/// <summary>
		/// Transforms a vector by a quaternion.
		/// </summary>
		/// <param name="vector">The vector to transform.</param>
		/// <param name="quaternion">The normalized quaternion.</param>
		/// <returns>The transformed vector.</returns>
		public static Vector3 Transform(Vector3 vector, Quaternion quaternion)
		{
			Vector3 u = new Vector3(quaternion.X, quaternion.Y, quaternion.Z);
			float s = quaternion.W;
			Vector3 sv = new Vector3(s * s);

			return u * Vector3.Dot(u, vector) * 2.0f + (sv * Vector3.Dot(u, u)) * vector + Vector3.Cross(u, vector)* 2.0f * s;
		}

		/// <summary>
		/// Transforms a normal by a matrix.
		/// </summary>
		/// <param name="vector">The normal.</param>
		/// <param name="matrix">The matrix.</param>
		/// <returns>The result of the transformation.</returns>
		public static Vector3 TransformNormal(Vector3 vector, Matrix matrix)
		{
			return new Vector3
			(
				vector.X * matrix.M11 + vector.Y * matrix.M12 + vector.Z * matrix.M13,
				vector.X * matrix.M21 + vector.Y * matrix.M22 + vector.Z * matrix.M23,
				vector.X * matrix.M31 + vector.Y * matrix.M32 + vector.Z * matrix.M33
			);
		}

		/// <summary>
		/// Linearly interpolates two vectors.
		/// </summary>
		/// <param name="from">The start.</param>
		/// <param name="to">The end.</param>
		/// <param name="mu">The delta.</param>
		/// <returns>The interpolated vector.</returns>
		public static Vector3 Lerp(Vector3 from, Vector3 to, float mu)
		{
			return from + (to - from) * mu;
		}

		/// <summary>
		/// Calculates the distance between two vectors.
		/// </summary>
		/// <param name="from">The starting point.</param>
		/// <param name="to">The end point.</param>
		/// <returns>The distance.</returns>
		public static float Distance(Vector3 from, Vector3 to)
		{
			return (from - to).Length();
		}

		/// <summary>
		/// Negates a vector.
		/// </summary>
		/// <param name="vector">The vector to negate.</param>
		/// <returns>The negated value.</returns>
		public static Vector3 operator -(Vector3 vector)
		{
			return new Vector3(-vector.X, -vector.Y, -vector.Z);
		}

		/// <summary>
		/// Converts the vector to a string.
		/// </summary>
		/// <returns>The string.</returns>
		public override string ToString()
		{
			return String.Format("({0}, {1}, {2})", X, Y, Z);
		}

		/// <summary>
		/// Compares two vectors for equality.
		/// </summary>
		/// <param name="a">The first vector.</param>
		/// <param name="b">The second vector.</param>
		/// <returns>True if the vectors are equal, false otherwise.</returns>
		public static bool operator ==(Vector3 a, Vector3 b)
		{
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
		}

		/// <summary>
		/// Compares two vectors for inequality.
		/// </summary>
		/// <param name="a">The first vector.</param>
		/// <param name="b">The second vector.</param>
		/// <returns>True if the vectors are not equal, false otherwise.</returns>
		public static bool operator !=(Vector3 a, Vector3 b)
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
			if (obj is Vector3)
			{
				Vector3 b = (Vector3)obj;

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
			return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
		}
	}
}
