using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae
{
	/// <summary>
	/// A four-dimensional matrix.
	/// </summary>
	public struct Matrix
	{
		/// <summary>
		/// The element at (1, 1).
		/// </summary>
		public float M11;

		/// <summary>
		/// The element at (1, 2).
		/// </summary>
		public float M12;

		/// <summary>
		/// The element at (1, 3).
		/// </summary>
		public float M13;

		/// <summary>
		/// The element at (1, 4).
		/// </summary>
		public float M14;


		/// <summary>
		/// The element at (2, 1).
		/// </summary>
		public float M21;

		/// <summary>
		/// The element at (2, 2).
		/// </summary>
		public float M22;

		/// <summary>
		/// The element at (2, 3).
		/// </summary>
		public float M23;

		/// <summary>
		/// The element at (2, 4).
		/// </summary>
		public float M24;

		/// <summary>
		/// The element at (3, 1).
		/// </summary>
		public float M31;

		/// <summary>
		/// The element at (3, 2).
		/// </summary>
		public float M32;

		/// <summary>
		/// The element at (3, 3).
		/// </summary>
		public float M33;

		/// <summary>
		/// The element at (3, 4).
		/// </summary>
		public float M34;

		/// <summary>
		/// The element at (4, 1).
		/// </summary>
		public float M41;

		/// <summary>
		/// The element at (4, 2).
		/// </summary>
		public float M42;

		/// <summary>
		/// The element at (4, 3).
		/// </summary>
		public float M43;

		/// <summary>
		/// The element at (4, 4).
		/// </summary>
		public float M44;

		static readonly Matrix identity = new Matrix
		(
			1.0f, 0.0f, 0.0f, 0.0f,
			0.0f, 1.0f, 0.0f, 0.0f,
			0.0f, 0.0f, 1.0f, 0.0f,
			0.0f, 0.0f, 0.0f, 1.0f
		);

		/// <summary>
		/// Gets the identity matrix.
		/// </summary>
		public static Matrix Identity
		{
			get { return identity; }
		}

		/// <summary>
		/// Creates a matrix from a collection of values.
		/// </summary>
		/// <param name="m11">The element at (1, 1).</param>
		/// <param name="m12">The element at (1, 2).</param>
		/// <param name="m13">The element at (1, 3).</param>
		/// <param name="m14">The element at (1, 4).</param>
		/// <param name="m21">The element at (2, 1).</param>
		/// <param name="m22">The element at (2, 2).</param>
		/// <param name="m23">The element at (2, 3).</param>
		/// <param name="m24">The element at (2, 4).</param>
		/// <param name="m31">The element at (3, 1).</param>
		/// <param name="m32">The element at (3, 2).</param>
		/// <param name="m33">The element at (3, 3).</param>
		/// <param name="m34">The element at (3, 4).</param>
		/// <param name="m41">The element at (4, 1).</param>
		/// <param name="m42">The element at (4, 2).</param>
		/// <param name="m43">The element at (4, 3).</param>
		/// <param name="m44">The element at (4, 4).</param>
		public Matrix(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
		{
			M11 = m11;
			M12 = m12;
			M13 = m13;
			M14 = m14;

			M21 = m21;
			M22 = m22;
			M23 = m23;
			M24 = m24;

			M31 = m31;
			M32 = m32;
			M33 = m33;
			M34 = m34;

			M41 = m41;
			M42 = m42;
			M43 = m43;
			M44 = m44;
		}

		/// <summary>
		/// Calculates the determinant.
		/// </summary>
		/// <returns>The determinant.</returns>
		public float Determinant()
		{
			float d1 = (M33 * M44) - (M34 * M43);
			float d2 = (M32 * M44) - (M34 * M42);
			float d3 = (M32 * M43) - (M33 * M42);
			float d4 = (M31 * M44) - (M34 * M41);
			float d5 = (M31 * M43) - (M33 * M41);
			float d6 = (M31 * M42) - (M32 * M41);

			return ((((M11 * (((M22 * d1) - (M23 * d2)) + (M24 * d3))) - (M12 * (((M21 * d1) -
				(M23 * d4)) + (M24 * d5)))) + (M13 * (((M21 * d2) - (M22 * d4)) +
				(M24 * d6)))) - (M14 * (((M21 * d3) - (M22 * d5)) + (M23 * d6))));
		}

		/// <summary>
		/// Creates a scale matrix.
		/// </summary>
		/// <param name="scale">The value to scale by.</param>
		/// <returns>The scale matrix.</returns>
		public static Matrix Scale(Vector3 scale)
		{
			return new Matrix
			(
				scale.X, 0.0f, 0.0f, 0.0f,
				0.0f, scale.Y, 0.0f, 0.0f,
				0.0f, 0.0f, scale.Z, 0.0f,
				0.0f, 0.0f, 0.0f, 1.0f
			);
		}

		/// <summary>
		/// Creates a rotation matrix from the provided axis.
		/// </summary>
		/// <param name="axis">The axis of rotation.</param>
		/// <param name="angle">The angle to rotate by.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix Rotation(Vector3 axis, float angle)
		{
			float cos = (float)Math.Cos(-angle);
			float sin = (float)Math.Sin(-angle);
			float t = 1.0f - cos;

			return new Matrix
			(
				t * axis.X * axis.X + cos, t * axis.X * axis.Y - sin * axis.Z, t * axis.X * axis.Z + sin * axis.Y, 0.0f,
				t * axis.X * axis.Y + sin * axis.Z, t * axis.Y * axis.Y + cos, t * axis.Y * axis.Z - sin * axis.X, 0.0f,
				t * axis.X * axis.Z - sin * axis.Y, t * axis.Y * axis.Z + sin * axis.X, t * axis.Z * axis.Z + cos, 0.0f,
				0.0f, 0.0f, 0.0f, 1.0f
			);
		}

		/// <summary>
		/// Creates a translation matrix.
		/// </summary>
		/// <param name="translation">The translation value.</param>
		/// <returns>The translation matrix.</returns>
		public static Matrix Translation(Vector3 translation)
		{
			return new Matrix
			(
				1.0f, 0.0f, 0.0f, translation.X,
				0.0f, 1.0f, 0.0f, translation.Y,
				0.0f, 0.0f, 1.0f, translation.Z,
				0.0f, 0.0f, 0.0f, 1.0f
			);
		}

		/// <summary>
		/// Creates an orthographic projection matrix.
		/// </summary>
		/// <param name="left">The left plane.</param>
		/// <param name="right">The right plane.</param>
		/// <param name="bottom">The bottom plane.</param>
		/// <param name="top">The top plane.</param>
		/// <param name="near">The near plane.</param>
		/// <param name="far">The far plane.</param>
		/// <returns>The orthographic projection matrix.</returns>
		public static Matrix Orthographic(float left, float right, float bottom, float top, float near, float far)
		{
			float rightLeft = 1.0f / (right - left);
			float topBottom = 1.0f / (top - bottom);
			float farNear = 1.0f / (far - near);

			return new Matrix
			(
				2.0f * rightLeft, 0.0f, 0.0f, -(right + left) * rightLeft,
				0.0f, 2.0f * topBottom, 0.0f, -(top + bottom) * topBottom, 
				0.0f, 0.0f, -2.0f * farNear, -(far + near) * farNear,
				0.0f, 0.0f, 0.0f, 1.0f
			);
		}

		/// <summary>
		/// Creates a perspective matrix.
		/// </summary>
		/// <param name="fov">The field of view.</param>
		/// <param name="aspectRatio">The aspect ratio.</param>
		/// <param name="near">The near plane.</param>
		/// <param name="far">The far plane.</param>
		/// <returns>The perspective projection matrix.</returns>
		public static Matrix Perspective(float fov, float aspectRatio, float near, float far)
		{
			float f = (float)Math.Tan(Math.PI / 2 - fov / 2.0f);
			float a = f / aspectRatio;
			float b = (far + near) / (near - far);
			float c = (2.0f * far * near) / (near - far);

			return new Matrix
			(
				a, 0.0f, 0.0f, 0.0f,
				0.0f, f, 0.0f, 0.0f,
				0.0f, 0.0f, b, c,
				0.0f, 0.0f, -1.0f, 0.0f
			);
		}

		/// <summary>
		/// Manually constructs a perspective matrix.
		/// </summary>
		/// <param name="left">The left plane.</param>
		/// <param name="right">The right plane.</param>
		/// <param name="bottom">The bottom plane.</param>
		/// <param name="top">The top plane.</param>
		/// <param name="near">The near plane.</param>
		/// <param name="far">The far plane.</param>
		/// <returns>The perspective projection matrix.</returns>
		public static Matrix Frustum(float left, float right, float bottom, float top, float near, float far)
		{
			float x = (2.0f * near) / (right - left);
			float y = (2.0f * near) / (top - bottom);
			float a = (right + left) / (right - left);
			float b = (top + bottom) / (top - bottom);
			float c = -(far + near) / (far - near);
			float d = -(2.0f * far * near) / (far - near);

			return new Matrix
			(
				x, 0, 0, 0,
				0, y, 0, 0,
				a, b, c, -1,
				0, 0, d, 0
			);
		}

		/// <summary>
		///	Creates a matrix that looks towards the target.
		/// </summary>
		/// <param name="eye">The position of the eye.</param>
		/// <param name="target">The target.</param>
		/// <param name="up">The upwards direction.</param>
		/// <returns>The look at matrix.</returns>
		public static Matrix LookAt(Vector3 eye, Vector3 target, Vector3 up)
		{
			Vector3 f = Vector3.Normalize(target - eye);
			Vector3 s = Vector3.Normalize(Vector3.Cross(f, up));
			Vector3 u = Vector3.Normalize(Vector3.Cross(s, f));

			Matrix m = new Matrix
			(
				s.X, s.Y, s.Z, 0.0f,
				u.X, u.Y, u.Z, 0.0f,
				-f.X, -f.Y, -f.Z, 0.0f,
				0.0f, 0.0f, 0.0f, 1.0f
			);

			return m * Matrix.Translation(-eye);
		}

		/// <summary>
		/// Multiplies a matrix by another.
		/// </summary>
		/// <param name="left">The left matrix.</param>
		/// <param name="right">The right matrix.</param>
		/// <returns>The result of the operation.</returns>
		public static Matrix Multiply(Matrix left, Matrix right)
		{
			Matrix result;

			Multiply(ref left, ref right, out result);

			return result;
		}

		/// <summary>
		/// Multiplies a matrix by another, storing the result.
		/// </summary>
		/// <param name="left">The left matrix.</param>
		/// <param name="right">The right matrix.</param>
		/// <param name="result">The resulting multiplication.</param>
		public static void Multiply(ref Matrix left, ref Matrix right, out Matrix result)
		{
			result.M11 = left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31 + left.M14 * right.M41;
			result.M12 = left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32 + left.M14 * right.M42;
			result.M13 = left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33 + left.M14 * right.M43;
			result.M14 = left.M11 * right.M14 + left.M12 * right.M24 + left.M13 * right.M34 + left.M14 * right.M44;
			result.M21 = left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31 + left.M24 * right.M41;
			result.M22 = left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32 + left.M24 * right.M42;
			result.M23 = left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33 + left.M24 * right.M43;
			result.M24 = left.M21 * right.M14 + left.M22 * right.M24 + left.M23 * right.M34 + left.M24 * right.M44;
			result.M31 = left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31 + left.M34 * right.M41;
			result.M32 = left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32 + left.M34 * right.M42;
			result.M33 = left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33 + left.M34 * right.M43;
			result.M34 = left.M31 * right.M14 + left.M32 * right.M24 + left.M33 * right.M34 + left.M34 * right.M44;
			result.M41 = left.M41 * right.M11 + left.M42 * right.M21 + left.M43 * right.M31 + left.M44 * right.M41;
			result.M42 = left.M41 * right.M12 + left.M42 * right.M22 + left.M43 * right.M32 + left.M44 * right.M42;
			result.M43 = left.M41 * right.M13 + left.M42 * right.M23 + left.M43 * right.M33 + left.M44 * right.M43;
			result.M44 = left.M41 * right.M14 + left.M42 * right.M24 + left.M43 * right.M34 + left.M44 * right.M44;
		}

		/// <summary>
		/// Multiplies a matrix by another.
		/// </summary>
		/// <param name="left">The left matrix.</param>
		/// <param name="right">The right matrix.</param>
		/// <returns>The result of the operation.</returns>
		public static Matrix operator *(Matrix left, Matrix right)
		{
			return Multiply(left, right);
		}

		/// <summary>
		/// Inverts a matrix.
		/// </summary>
		/// <param name="matrix">The matrix to invert.</param>
		/// <returns>The inverted matrix.</returns>
		public static Matrix Invert(Matrix matrix)
		{
			Matrix result;

			Invert(ref matrix, out result);

			return result;
		}

		/// <summary>
		/// Inverts a matrix, storing the result.
		/// </summary>
		/// <param name="matrix">The matrix.</param>
		/// <param name="result">The result.</param>
		public static void Invert(ref Matrix matrix, out Matrix result)
		{
			float d = 1.0f / matrix.Determinant();

			result.M11 = (matrix.M22 * matrix.M33 * matrix.M44 + matrix.M23 * matrix.M34 * matrix.M42 + matrix.M24 * matrix.M32 * matrix.M43 - matrix.M22 * matrix.M34 * matrix.M43 - matrix.M23 * matrix.M32 * matrix.M44 - matrix.M24 * matrix.M33 * matrix.M42) * d;
			result.M12 = (matrix.M12 * matrix.M34 * matrix.M43 + matrix.M13 * matrix.M32 * matrix.M44 + matrix.M14 * matrix.M33 * matrix.M42 - matrix.M12 * matrix.M33 * matrix.M44 - matrix.M13 * matrix.M34 * matrix.M42 - matrix.M14 * matrix.M32 * matrix.M43) * d;
			result.M13 = (matrix.M12 * matrix.M23 * matrix.M44 + matrix.M13 * matrix.M24 * matrix.M42 + matrix.M14 * matrix.M22 * matrix.M43 - matrix.M12 * matrix.M24 * matrix.M43 - matrix.M13 * matrix.M22 * matrix.M44 - matrix.M14 * matrix.M23 * matrix.M42) * d;
			result.M14 = (matrix.M12 * matrix.M24 * matrix.M33 + matrix.M13 * matrix.M22 * matrix.M34 + matrix.M14 * matrix.M23 * matrix.M32 - matrix.M12 * matrix.M23 * matrix.M34 - matrix.M13 * matrix.M24 * matrix.M32 - matrix.M14 * matrix.M22 * matrix.M33) * d;

			result.M21 = (matrix.M21 * matrix.M34 * matrix.M43 + matrix.M23 * matrix.M31 * matrix.M44 + matrix.M24 * matrix.M33 * matrix.M41 - matrix.M21 * matrix.M33 * matrix.M44 - matrix.M23 * matrix.M34 * matrix.M41 - matrix.M24 * matrix.M31 * matrix.M43) * d;
			result.M22 = (matrix.M11 * matrix.M33 * matrix.M44 + matrix.M13 * matrix.M34 * matrix.M41 + matrix.M14 * matrix.M31 * matrix.M43 - matrix.M11 * matrix.M34 * matrix.M43 - matrix.M13 * matrix.M31 * matrix.M44 - matrix.M14 * matrix.M33 * matrix.M41) * d;
			result.M23 = (matrix.M11 * matrix.M24 * matrix.M43 + matrix.M13 * matrix.M21 * matrix.M44 + matrix.M14 * matrix.M23 * matrix.M41 - matrix.M11 * matrix.M23 * matrix.M44 - matrix.M13 * matrix.M24 * matrix.M41 - matrix.M14 * matrix.M21 * matrix.M43) * d;
			result.M24 = (matrix.M11 * matrix.M23 * matrix.M34 + matrix.M13 * matrix.M24 * matrix.M31 + matrix.M14 * matrix.M21 * matrix.M33 - matrix.M11 * matrix.M24 * matrix.M33 - matrix.M13 * matrix.M21 * matrix.M34 - matrix.M14 * matrix.M23 * matrix.M31) * d;

			result.M31 = (matrix.M21 * matrix.M32 * matrix.M44 + matrix.M22 * matrix.M34 * matrix.M41 + matrix.M24 * matrix.M31 * matrix.M42 - matrix.M21 * matrix.M34 * matrix.M42 - matrix.M22 * matrix.M31 * matrix.M44 - matrix.M24 * matrix.M32 * matrix.M41) * d;
			result.M32 = (matrix.M11 * matrix.M34 * matrix.M42 + matrix.M12 * matrix.M31 * matrix.M44 + matrix.M14 * matrix.M32 * matrix.M41 - matrix.M11 * matrix.M32 * matrix.M44 - matrix.M12 * matrix.M34 * matrix.M41 - matrix.M14 * matrix.M31 * matrix.M42) * d;
			result.M33 = (matrix.M11 * matrix.M22 * matrix.M44 + matrix.M12 * matrix.M24 * matrix.M41 + matrix.M14 * matrix.M21 * matrix.M42 - matrix.M11 * matrix.M24 * matrix.M42 - matrix.M12 * matrix.M21 * matrix.M44 - matrix.M14 * matrix.M22 * matrix.M41) * d;
			result.M34 = (matrix.M11 * matrix.M24 * matrix.M32 + matrix.M12 * matrix.M21 * matrix.M34 + matrix.M14 * matrix.M22 * matrix.M31 - matrix.M11 * matrix.M22 * matrix.M34 - matrix.M12 * matrix.M24 * matrix.M31 - matrix.M14 * matrix.M21 * matrix.M32) * d;

			result.M41 = (matrix.M21 * matrix.M33 * matrix.M42 + matrix.M22 * matrix.M31 * matrix.M43 + matrix.M23 * matrix.M32 * matrix.M41 - matrix.M21 * matrix.M32 * matrix.M43 - matrix.M22 * matrix.M33 * matrix.M41 - matrix.M23 * matrix.M31 * matrix.M42) * d;
			result.M42 = (matrix.M11 * matrix.M32 * matrix.M43 + matrix.M12 * matrix.M33 * matrix.M41 + matrix.M13 * matrix.M31 * matrix.M42 - matrix.M11 * matrix.M33 * matrix.M42 - matrix.M12 * matrix.M31 * matrix.M43 - matrix.M13 * matrix.M32 * matrix.M41) * d;
			result.M43 = (matrix.M11 * matrix.M23 * matrix.M42 + matrix.M12 * matrix.M21 * matrix.M43 + matrix.M13 * matrix.M22 * matrix.M41 - matrix.M11 * matrix.M22 * matrix.M43 - matrix.M12 * matrix.M23 * matrix.M41 - matrix.M13 * matrix.M21 * matrix.M42) * d;
			result.M44 = (matrix.M11 * matrix.M22 * matrix.M33 + matrix.M12 * matrix.M23 * matrix.M31 + matrix.M13 * matrix.M21 * matrix.M32 - matrix.M11 * matrix.M23 * matrix.M32 - matrix.M12 * matrix.M21 * matrix.M33 - matrix.M13 * matrix.M22 * matrix.M31) * d;
		}

		/// <summary>
		/// Performs an efficient invert for affine matrices.
		/// </summary>
		/// <param name="matrix">The matrix to invert.</param>
		/// <returns>The inverted matrix.</returns>
		public static Matrix AffineInvert(Matrix matrix)
		{
			Matrix result;

			AffineInvert(ref matrix, out result);

			return result;
		}

		/// <summary>
		/// Performs an efficient invert for affine matrices.
		/// </summary>
		/// <param name="matrix">The matrix to invert.</param>
		/// <param name="result">The result.</param>
		public static void AffineInvert(ref Matrix matrix, out Matrix result)
		{
			float det =
				matrix.M11 * matrix.M22 * matrix.M33 +
				matrix.M12 * matrix.M23 * matrix.M31 +
				matrix.M13 * matrix.M21 * matrix.M32 -
				matrix.M31 * matrix.M22 * matrix.M13 -
				matrix.M32 * matrix.M23 * matrix.M11 -
				matrix.M33 * matrix.M21 * matrix.M12;
			float inv = 1.0f / det;

			result = Identity;

			result.M11 = (matrix.M33 * matrix.M22 - matrix.M32 * matrix.M23) * inv;
			result.M12 = -(matrix.M33 * matrix.M12 - matrix.M32 * matrix.M13) * inv;
			result.M13 = (matrix.M23 * matrix.M12 - matrix.M22 * matrix.M13) * inv;

			result.M21 = -(matrix.M33 * matrix.M21 - matrix.M31 * matrix.M23) * inv;
			result.M22 = (matrix.M33 * matrix.M11 - matrix.M31 * matrix.M13) * inv;
			result.M23 = -(matrix.M23 * matrix.M11 - matrix.M21 * matrix.M13) * inv;

			result.M31 = (matrix.M32 * matrix.M21 - matrix.M31 * matrix.M23) * inv;
			result.M32 = -(matrix.M32 * matrix.M11 - matrix.M31 * matrix.M12) * inv;
			result.M33 = (matrix.M22 * matrix.M11 - matrix.M21 * matrix.M12) * inv;

			Vector3 translation = new Vector3(matrix.M14, matrix.M24, matrix.M34);

			result.M14 = -(translation.X * result.M11 + translation.Y * result.M12 + translation.Z * result.M13);
			result.M24 = -(translation.X * result.M21 + translation.Y * result.M22 + translation.Z * result.M23);
			result.M34 = -(translation.X * result.M31 + translation.Y * result.M32 + translation.Z * result.M33);
		}

		/// <summary>
		/// Transposes a matrix.
		/// </summary>
		/// <param name="matrix">The matrix to transpose.</param>
		/// <returns>The transposed matrix.</returns>
		public static Matrix Transpose(Matrix matrix)
		{
			return new Matrix
			(
				matrix.M11, matrix.M21, matrix.M31, matrix.M41,
				matrix.M12, matrix.M22, matrix.M32, matrix.M42,
				matrix.M13, matrix.M23, matrix.M33, matrix.M43,
				matrix.M14, matrix.M24, matrix.M34, matrix.M44
			);
		}

		/// <summary>
		/// Converts the matrix into a useful string representation.
		/// </summary>
		/// <returns>The string.</returns>
		public override string ToString()
		{
			return String.Format("(({0}, {1}, {2}, {3}), ({4}, {5}, {6}, {7}), ({8}, {9}, {10}, {11}), ({12}, {13}, {14}, {15}))",
				M11, M12, M13, M14, M21, M22, M23, M24, M31, M32, M33, M34, M41, M42, M43, M44);
		}
	}
}
