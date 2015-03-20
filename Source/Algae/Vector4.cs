using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae
{
	/// <summary>
	/// A four dimensional vector.
	/// </summary>
	public struct Vector4
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

		/// <summary>
		/// The fourth coordinate.
		/// </summary>
		public float W;

		/// <summary>
		/// Constructs a vector from four components.
		/// </summary>
		/// <param name="x">The first coordinate.</param>
		/// <param name="y">The second coordinate.</param>
		/// <param name="z">The third coordinate.</param>
		/// <param name="w">The forth coordinate.</param>
		public Vector4(float x, float y, float z, float w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}

		/// <summary>
		/// Constructs a four-dimensional vector from a three-dimensional one.
		/// </summary>
		/// <param name="vector">The vector.</param>
		public Vector4(Vector3 vector)
		{
			X = vector.X;
			Y = vector.Y;
			Z = vector.Z;
			W = 1.0f;
		}

		/// <summary>
		/// Constructs a vector from a color.
		/// </summary>
		/// <param name="color">The color.</param>
		public Vector4(Color color)
		{
			X = color.Red;
			Y = color.Green;
			Z = color.Blue;
			W = color.Alpha;
		}

		/// <summary>
		/// Transforms a vector by a matrix.
		/// </summary>
		/// <param name="vec">The vector.</param>
		/// <param name="mat">The matrix.</param>
		/// <returns></returns>
		public static Vector4 Transform(Vector4 vec, Matrix mat)
		{
			return new Vector4
			(
				vec.X * mat.M11 + vec.Y * mat.M12 + vec.Z * mat.M13 + vec.W * mat.M14,
				vec.X * mat.M21 + vec.Y * mat.M22 + vec.Z * mat.M23 + vec.W * mat.M24,
				vec.X * mat.M31 + vec.Y * mat.M32 + vec.Z * mat.M33 + vec.W * mat.M34,
				vec.X * mat.M41 + vec.Y * mat.M42 + vec.Z * mat.M43 + vec.W * mat.M44
			);
		}

		/// <summary>
		/// Compares two vectors for equality.
		/// </summary>
		/// <param name="a">The first vector.</param>
		/// <param name="b">The second vector.</param>
		/// <returns>True if the vectors are equal, false otherwise.</returns>
		public static bool operator ==(Vector4 a, Vector4 b)
		{
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;
		}

		/// <summary>
		/// Compares two vectors for inequality.
		/// </summary>
		/// <param name="a">The first vector.</param>
		/// <param name="b">The second vector.</param>
		/// <returns>True if the vectors are not equal, false otherwise.</returns>
		public static bool operator !=(Vector4 a, Vector4 b)
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
			if (obj is Vector4)
			{
				Vector4 b = (Vector4)obj;

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
			return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode();
		}

		/// <summary>
		/// Converts the vector to a string.
		/// </summary>
		/// <returns>The string.</returns>
		public override string ToString()
		{
			return String.Format("({0}, {1}, {2}, {3})", X, Y, Z, W);
		}

		public static Vector4 Normalize(Vector4 vector)
		{
			// TODO: Add (and then use) GetLength() and the division operator.
			float n = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z + vector.W * vector.W);

			return new Vector4(vector.X / n, vector.Y / n, vector.Z / n, vector.W / n);
		}
	}
}
