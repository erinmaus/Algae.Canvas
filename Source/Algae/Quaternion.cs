using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae
{
	/// <summary>
	/// A quaternion.
	/// </summary>
	public struct Quaternion
	{
		/// <summary>
		/// The X component.
		/// </summary>
		public float X;

		/// <summary>
		/// The Y component.
		/// </summary>
		public float Y;

		/// <summary>
		/// The Z component.
		/// </summary>
		public float Z;

		/// <summary>
		/// The W component.
		/// </summary>
		public float W;

		static readonly Quaternion identity = new Quaternion(0, 0, 0, 1);

		/// <summary>
		/// The identity quaternion.
		/// </summary>
		public static Quaternion Identity
		{
			get { return identity; }
		}

		/// <summary>
		/// Constructs a quaternion from a scalar component.
		/// </summary>
		/// <param name="scalar">The scalar component.</param>
		public Quaternion(float scalar)
		{
			X = scalar;
			Y = scalar;
			Z = scalar;
			W = scalar;
		}

		/// <summary>
		/// Constructs a quaternion from the provided components.
		/// </summary>
		/// <param name="x">The X component.</param>
		/// <param name="y">The Y component.</param>
		/// <param name="z">The Z component.</param>
		/// <param name="w">The W component.</param>
		public Quaternion(float x, float y, float z, float w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}

		/// <summary>
		/// Gets the squared length of the quaternion.
		/// </summary>
		/// <returns>The squared length.</returns>
		public float LengthSquared()
		{
			return X * X + Y * Y + Z * Z + W * W;
		}

		/// <summary>
		/// Gets the length of the quaternion.
		/// </summary>
		/// <returns>The length.</returns>
		public float Length()
		{
			return (float)Math.Sqrt(LengthSquared());
		}

		/// <summary>
		/// Combines two quaternions.
		/// </summary>
		/// <param name="left">The left quaternion.</param>
		/// <param name="right">The right quaternion.</param>
		public static Quaternion Multiply(Quaternion left, Quaternion right)
		{
			Quaternion quaternion;

			Multiply(ref left, ref right, out quaternion);

			return quaternion;
		}

		/// <summary>
		/// Combines two quaternions.
		/// </summary>
		/// <param name="left">The left quaternion.</param>
		/// <param name="right">The right quaternion.</param>
		/// <param name="result">The result.</param>
		public static void Multiply(ref Quaternion left, ref Quaternion right, out Quaternion result)
		{
			result = new Quaternion
			(
				(right.X * left.W + left.X * right.W + right.Y * left.Z) - (right.Z * left.Y),
				(right.Y * left.W + left.Y * right.W + right.Z * left.X) - (right.X * left.Z),
				(right.Z * left.W + left.Z * right.W + right.X * left.Y) - (right.Y * left.X),
				right.W * left.W - right.X * left.X - right.Y * left.Y - right.Z * left.Z
			);
		}

		/// <summary>
		/// Combines two quaternions.
		/// </summary>
		/// <param name="left">The left quaternion.</param>
		/// <param name="right">The right quaternion.</param>
		public static Quaternion operator *(Quaternion left, Quaternion right)
		{
			return Multiply(left, right);
		}

		/// <summary>
		/// Calculates the conjugate of the quaternion.
		/// </summary>
		/// <param name="quaternion">The quaternion.</param>
		/// <returns>The conjugate.</returns>
		public static Quaternion Conjugate(Quaternion quaternion)
		{
			quaternion.X = -quaternion.X;
			quaternion.Y = -quaternion.Y;
			quaternion.Z = -quaternion.Z;

			return quaternion;
		}

		/// <summary>
		/// Normalizes a quaternion.
		/// </summary>
		/// <param name="quaternion">The quaternion to invert.</param>
		/// <returns>The normalized quaternion.</returns>
		public static Quaternion Normalize(Quaternion quaternion)
		{
			float length = quaternion.Length();

			quaternion.X /= length;
			quaternion.Y /= length;
			quaternion.Z /= length;
			quaternion.W /= length;

			return quaternion;
		}

		/// <summary>
		/// Calculates the inverse of the quaternion.
		/// </summary>
		/// <param name="quaternion">The quaternion to invert.</param>
		/// <returns>The inverted quaternion.</returns>
		public static Quaternion Invert(Quaternion quaternion)
		{
			float lengthSquared = quaternion.LengthSquared();
			Quaternion conjugate = Quaternion.Conjugate(quaternion);

			quaternion.X /= lengthSquared;
			quaternion.Y /= lengthSquared;
			quaternion.Z /= lengthSquared;
			quaternion.W /= lengthSquared;

			return quaternion;
		}

		/// <summary>
		/// Calculates the dot product of two quaternions.
		/// </summary>
		/// <param name="a">The first quaternion.</param>
		/// <param name="b">The second quaternion.</param>
		/// <returns>The dot product.</returns>
		public static float Dot(Quaternion a, Quaternion b)
		{
			return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
		}

		/// <summary>
		/// Linearly interpolates two quaternions.
		/// </summary>
		/// <param name="a">The starting quaternion.</param>
		/// <param name="b">The ending quaternion.</param>
		/// <param name="delta">The inteprolation factor.</param>
		/// <returns>The inteprolated quaternion.</returns>
		public static Quaternion Lerp(Quaternion a, Quaternion b, float delta)
		{
			Quaternion result = new Quaternion
			(
				MathHelper.Lerp(a.X, b.X, delta),
				MathHelper.Lerp(a.Y, b.Y, delta),
				MathHelper.Lerp(a.Z, b.Z, delta),
				MathHelper.Lerp(a.W, b.W, delta)
			);

			return Quaternion.Normalize(result);
		}

		/// <summary>
		/// Negates a quaternion.
		/// </summary>
		/// <param name="quaternion">The quaternion to negate.</param>
		/// <returns>The negated quaternion.</returns>
		public static Quaternion operator -(Quaternion quaternion)
		{
			return new Quaternion(-quaternion.X, -quaternion.Y, -quaternion.Z, -quaternion.W);
		}

		/// <summary>
		/// Spherically interpolates two quaternions.
		/// </summary>
		/// <param name="a">The starting quaternion.</param>
		/// <param name="b">The ending quaternion.</param>
		/// <param name="delta">The interpolation factor.</param>
		/// <returns>The interpolated quaternion.</returns>
		public static Quaternion Slerp(Quaternion a, Quaternion b, float delta)
		{
			float cosom = Quaternion.Dot(a, b);

			if (cosom < 0)
			{
				cosom = -cosom;
				b = -b;
			}

			if (1.0f - cosom > Single.Epsilon)
			{
				float angle = (float)Math.Acos(cosom);
				float v1 = (float)Math.Sin(angle * (1.0f - delta));
				float v2 = (float)Math.Sin(angle * delta);
				float v3 = (float)Math.Sin(angle);

				return new Quaternion
				(
					(a.X * v1 + b.X * v2) / v3,
					(a.Y * v1 + b.Y * v2) / v3,
					(a.Z * v1 + b.Z * v2) / v3,
					(a.W * v1 + b.W * v2) / v3
				);
			}
			else
				return Quaternion.Lerp(a, b, delta);
		}

		/// <summary>
		/// Converts the quaternion to a rotation matrix.
		/// </summary>
		/// <param name="quaternion">The quaternion.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix ToMatrix(Quaternion quaternion)
		{
			float x = quaternion.X;
			float y = quaternion.Y;
			float z = quaternion.Z;
			float w = quaternion.W;

			return new Matrix
			(
				1.0f - 2.0f * y * y - 2.0f * z * z, 2.0f * x * y - 2.0f * z * w, 2.0f * x * z + 2.0f * y * w, 0.0f,
				2.0f * x * y + 2.0f * z * w, 1.0f - 2.0f * x * x - 2.0f * z * z, 2.0f * y * z - 2.0f * x * w, 0.0f,
				2.0f * x * z - 2.0f * y * w, 2.0f * y * z + 2.0f * x * w, 1.0f - 2.0f * x * x - 2.0f * y * y, 0.0f,
				0.0f, 0.0f, 0.0f, 1.0f
			);
		}

		/// <summary>
		/// Constructs a quaternion from an axis and angle.
		/// </summary>
		/// <param name="axis">The axis.</param>
		/// <param name="angle">The angle.</param>
		/// <returns>The quaternion.</returns>
		public static Quaternion FromAxisAngle(Vector3 axis, float angle)
		{
			float w = (float)Math.Cos(angle * 0.5f);
			Vector3 v = axis * (float)Math.Sin(angle * 0.5f);

			return new Quaternion(v.X, v.Y, v.Z, w);
		}

		public static Quaternion FromPitchYawRoll(float pitch, float yaw, float roll)
		{
			float cr = (float)Math.Cos(roll * 0.5f);
			float cp = (float)Math.Cos(pitch * 0.5f);
			float cy = (float)Math.Cos(yaw * 0.5f);
			float sr = (float)Math.Sin(roll * 0.5f);
			float sp = (float)Math.Sin(pitch * 0.5f);
			float sy = (float)Math.Sin(yaw * 0.5f);

			return new Quaternion
			(
				sr * cp * cy - cr * sp * sy,
				cr * sp * cy + sr * cp * sy,
				cr * cp * sy - sr * sp * cy,
				cr * cp * cy + sr * sp * sy
			);
		}

		public static Quaternion LookAt(Vector3 forward, Vector3 up)
		{
			Vector3 h = Vector3.Normalize(forward + up);

			return new Quaternion
			(
				up.Y * h.Z - up.Z * h.Y,
				up.Z * h.X - up.X * h.Z,
				up.X * h.Y - up.Y * h.X,
				Vector3.Dot(up, h)
			);
		}
	}
}
