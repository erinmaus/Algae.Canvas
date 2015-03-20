using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae
{
	/// <summary>
	/// Represents a plane.
	/// </summary>
	public struct Plane
	{
		/// <summary>
		/// The direction of the plane.
		/// </summary>
		public Vector3 Normal;

		/// <summary>
		/// The distance of the plane.
		/// </summary>
		public float D;

		/// <summary>
		/// Constructs a plane from a normal and a distance.
		/// </summary>
		/// <param name="normal">The normal.</param>
		/// <param name="d">The distance.</param>
		public Plane(Vector3 normal, float d)
		{
			Normal = normal;
			D = d;
		}

		/// <summary>
		/// Constructs a plane from three coplanar points.
		/// </summary>
		/// <param name="a">The first point.</param>
		/// <param name="b">The second point.</param>
		/// <param name="c">The third point.</param>
		public Plane(Vector3 a, Vector3 b, Vector3 c)
		{
			Normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
			D = Vector3.Dot(Normal, a);
		}

		/// <summary>
		/// Calculates the distance of a point from a plane.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <returns>The distance.</returns>
		public float Distance(Vector3 point)
		{
			return Vector3.Dot(point, Normal) + D;
		}

		/// <summary>
		/// Projects the point on to the plane.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <returns>The projection.</returns>
		public Vector3 Project(Vector3 point)
		{
			return point - Normal * Distance(point);
		}

		/// <summary>
		/// Returns the point of intersection of three planes.
		/// </summary>
		/// <param name="p1">The first plane.</param>
		/// <param name="p2">The second plane.</param>
		/// <param name="p3">The third plane.</param>
		/// <remarks>The three planes must intersect...</remarks>
		/// <returns>The point of intersection.</returns>
		public static Vector3 Intersection(Plane p1, Plane p2, Plane p3)
		{
			float d = Vector3.Dot(p1.Normal, Vector3.Cross(p2.Normal, p3.Normal));

			return (Vector3.Cross(p2.Normal, p3.Normal) * p1.D + Vector3.Cross(p3.Normal, p1.Normal) * p2.D + Vector3.Cross(p1.Normal, p2.Normal) * p3.D) / d;
		}

		public static bool Intersection(Plane p1, Plane p2, Plane p3, out Vector3 v)
		{
			float d = Vector3.Dot(p1.Normal, Vector3.Cross(p2.Normal, p3.Normal));

			if (Math.Abs(d) < 0.01f)
			{
				v = Vector3.Zero;
				return false;
			}
			
			v = (Vector3.Cross(p2.Normal, p3.Normal) * p1.D + Vector3.Cross(p3.Normal, p1.Normal) * p2.D + Vector3.Cross(p1.Normal, p2.Normal) * p3.D) / d;
			return true;
		}

		public static bool Intersection(Plane plane, Vector3 a, Vector3 b, out Vector3 point)
		{
			Vector3 u = b - a;
			Vector3 w = a - (plane.Normal * plane.D);

			float d = Vector3.Dot(plane.Normal, u);
			float n = -Vector3.Dot(plane.Normal, w);

			if (Math.Abs(d) < 0.001f)
			{
				point = Vector3.Zero;
				return false;
			}

			float s = n / d;

			if (s < 0.0f || s > 1.0f)
			{
				point = Vector3.Zero;
				return false;
			}

			point = a + u * s;

			return true;
		}

		/// <summary>
		/// Converts the plane to a string.
		/// </summary>
		/// <returns>The string value.</returns>
		public override string ToString()
		{
			return String.Format("{0} [{1}]", Normal.ToString(), D);
		}
	}
}
