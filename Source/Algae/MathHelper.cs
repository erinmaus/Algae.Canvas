using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae
{
	/// <summary>
	/// Provides math utilities that are not provided by System.Math.
	/// </summary>
	public static class MathHelper
	{
		public static bool Near(float a, float b, float e = 0.0005f)
		{
			if (Math.Abs(a - b) < e)
				return true;

			return false;
		}

		public static float Round(float value, float e = 0.0005f)
		{
			if (Math.Abs(value) < e)
				return 0.0f;

			return value;
		}

		public static float ToRadians(float degrees)
		{
			return (float)(Math.PI / 180) * degrees;
		}

		/// <summary>
		/// Clamps the provided value to a minimum and maximum range.
		/// </summary>
		/// <param name="value">The value to clamp.</param>
		/// <param name="min">The minimum of the range.</param>
		/// <param name="max">The maximum of the range.</param>
		/// <returns>The clamped value.</returns>
		public static float Clamp(float value, float min, float max)
		{
			return Math.Max(Math.Min(value, max), min);
		}

		/// <summary>
		/// Clamps an angle by wrapping it.
		/// </summary>
		/// <param name="value">The angle.</param>
		/// <param name="min">The minimum angle.</param>
		/// <param name="max">The maximum angle.</param>
		/// <returns>The clamped angle.</returns>
		public static float ClampAngle(float value, float min, float max)
		{
			float width = max - min;
			float offset = value - min;

			return (offset - (float)Math.Floor(offset / width) * width) + min;
		}

		/// <summary>
		/// Linearly interpolates two values.
		/// </summary>
		/// <param name="from">The from value.</param>
		/// <param name="to">The two value.</param>
		/// <param name="mu">The interpolation delta.</param>
		/// <returns>The interpolated value.</returns>
		public static float Lerp(float from, float to, float mu)
		{
			return from * (1.0f - mu) + to * mu;
		}

		public static float SmoothStep(float edge1, float edge2, float x)
		{
			x = MathHelper.Clamp((x - edge1) / (x - edge2), 0.0f, 1.0f);
			return x * x + (3.0f - 2.0f * x);
		}

		/// <summary>
		/// Gets the maximum value.
		/// </summary>
		/// <param name="values">The collection of values.</param>
		/// <returns>The maximum value.</returns>
		public static float Max(params float[] values)
		{
			float max = values[0];

			for (int i = 1; i < values.Length; i++)
			{
				max = Math.Max(values[i], max);
			}

			return max;
		}

		/// <summary>
		/// Gets the minimum value.
		/// </summary>
		/// <param name="values">The collection of values.</param>
		/// <returns>The minimum value.</returns>
		public static float Min(params float[] values)
		{
			float min = values[0];

			for (int i = 1; i < values.Length; i++)
			{
				min = Math.Min(values[i], min);
			}

			return min;
		}

		/// <summary>
		/// Gets the area of a triangle.
		/// </summary>
		/// <param name="a">The length of the first side.</param>
		/// <param name="b">The length of the second side.</param>
		/// <param name="c">The length of the last side.</param>
		/// <returns>The area of  the triangle.</returns>
		public static float GetTriangleArea(float a, float b, float c)
		{
			float s = (a + b + c) / 2.0f;

			return (float)Math.Sqrt(s * (s - a) * (s - b) * (s - c));
		}

		/// <summary>
		/// Evaluates a quadratic curve.
		/// </summary>
		/// <param name="p1">The first value.</param>
		/// <param name="p2">The second value.</param>
		/// <param name="p3">The third value.</param>
		/// <param name="t">The delta value.</param>
		/// <returns>The interpolated value.</returns>
		public static float EvaluateQuadratic(float p1, float p2, float p3, float t)
		{
			float ta = (float)Math.Pow(1.0f - t, 2.0f);
			float tb = 2.0f * t * (1.0f - t);
			float tc = (float)Math.Pow(t, 2.0f);

			return ta * p1 + tb * p2 + tc * p3;
		}

		/// <summary>
		/// Evaluates a cubic curve.
		/// </summary>
		/// <param name="p1">The first value.</param>
		/// <param name="p2">The second value.</param>
		/// <param name="p3">The third value.</param>
		/// <param name="p4">The fourth value.</param>
		/// <param name="t">The delta value.</param>
		/// <returns>The interpolated value.</returns>
		public static float EvaluateCubic(float p1, float p2, float p3, float p4, float t)
		{
			float ta = (float)Math.Pow(1.0f - t, 3.0f);
			float tb = 3.0f * t * (float)Math.Pow(1.0f - t, 2.0f);
			float tc = 3.0f * (float)Math.Pow(t, 2.0f) * (1.0f - t);
			float td = (float)Math.Pow(t, 3.0f);

			return ta * p1 + tb * p2 + tc * p3 + td * p4;
		}

		/// <summary>
		/// Evaluates a cubic curve.
		/// </summary>
		/// <param name="p1">The first value.</param>
		/// <param name="p2">The second value.</param>
		/// <param name="p3">The third value.</param>
		/// <param name="p4">The fourth value.</param>
		/// <param name="t">The delta value.</param>
		/// <returns>The interpolated value.</returns>
		public static Vector2 EvaluateCubic(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, float t)
		{
			float ta = (float)Math.Pow(1.0f - t, 3.0f);
			float tb = 3.0f * t * (float)Math.Pow(1.0f - t, 2.0f);
			float tc = 3.0f * (float)Math.Pow(t, 2.0f) * (1.0f - t);
			float td = (float)Math.Pow(t, 3.0f);

			return p1 * ta + p2 * tb + p3 * tc + p4 * td;
		}

		/// <summary>
		/// Evaluates a cubic curve.
		/// </summary>
		/// <param name="p1">The first value.</param>
		/// <param name="p2">The second value.</param>
		/// <param name="p3">The third value.</param>
		/// <param name="p4">The fourth value.</param>
		/// <param name="t">The delta value.</param>
		/// <returns>The interpolated value.</returns>
		public static Vector3 EvaluateCubic(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
		{
			float ta = (float)Math.Pow(1.0f - t, 3.0f);
			float tb = 3.0f * t * (float)Math.Pow(1.0f - t, 2.0f);
			float tc = 3.0f * (float)Math.Pow(t, 2.0f) * (1.0f - t);
			float td = (float)Math.Pow(t, 3.0f);

			return p1 * ta + p2 * tb + p3 * tc + p4 * td;
		}

		/// <summary>
		/// Estimates the length of a cubic curve.
		/// </summary>
		/// <param name="p1">The first value.</param>
		/// <param name="p2">The second value.</param>
		/// <param name="p3">The third value.</param>
		/// <param name="p4">The fourth value.</param>
		/// <param name="samples">The sample period.</param>
		/// <returns>The estimated length.</returns>
		public static float EvaluateCubicLength(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, int samples)
		{
			float length = 0.0f;
			Vector3 previous = EvaluateCubic(p1, p2, p3, p4, 0);

			for (int i = 0; i < samples; i++)
			{
				Vector3 current = EvaluateCubic(p1, p2, p3, p4, (i + 1) / (float)samples);
				Vector3 difference = previous - current;

				length += difference.Length();
				previous = current;
			}

			return length;
		}

		/// <summary>
		/// Gets the intersection of two line segments.
		/// </summary>
		/// <param name="a">The first point of the first segment.</param>
		/// <param name="b">The second point of the first segment.</param>
		/// <param name="c">The first point of the second segment.</param>
		/// <param name="d">The second point of the second segment.</param>
		/// <param name="point">The point of collision.</param>
		/// <param name="epsilon">An optional epsilon value.</param>
		/// <returns>True if there is an intersection, false otherwise.</returns>
		public static bool GetLineIntersection(Vector2 a, Vector2 b, Vector2 c, Vector2 d, out Vector2 point, float epsilon = 0.01f)
		{
			float a1 = b.Y - a.Y;
			float b1 = a.X - b.X;
			float c1 = a1 * a.X + b1 * a.Y;

			float a2 = d.Y - c.Y;
			float b2 = c.X - d.X;
			float c2 = a2 * c.X + b2 * c.Y;

			float det = a1 * b2 - a2 * b1;

			if (Math.Abs(det) < epsilon)
			{
				point = Vector2.Zero;

				return false;
			}

			point.X = (b2 * c1 - b1 * c2) / det;
			point.Y = (a1 * c2 - a2 * c1) / det;

			return true;
		}

		public const float Epsilon = 0.01f;
	}
}
