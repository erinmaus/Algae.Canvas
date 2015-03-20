using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae
{
	/// <summary>
	/// A structure that represents a bounding box.
	/// </summary>
	public struct BoundingBox
	{
		/// <summary>
		/// The maximum extent of the bounding box.
		/// </summary>
		public Vector3 Max;

		/// <summary>
		/// The minimum extent of the bounding box.
		/// </summary>
		public Vector3 Min;

		/// <summary>
		/// Gets the center of the bounding box.
		/// </summary>
		public Vector3 Center
		{
			get { return Min + (Max - Min) * 0.5f; }
		}

		/// <summary>
		/// An empty bounding box.
		/// </summary>
		public static readonly BoundingBox Empty = new BoundingBox
		(
			Vector3.Zero,
			Vector3.Zero
		);

		/// <summary>
		/// A really big bounding box!
		/// </summary>
		public static readonly BoundingBox Big = new BoundingBox
		(
			new Vector3(Single.MaxValue, Single.MaxValue, Single.MaxValue),
			new Vector3(Single.MinValue, Single.MinValue, Single.MinValue)
		);

		/// <summary>
		/// Constructs a bounding box.
		/// </summary>
		/// <param name="max">The maximum extent.</param>
		/// <param name="min">The minimum extent.</param>
		public BoundingBox(Vector3 max, Vector3 min)
		{
			Max = max;
			Min = min;
		}

		/// <summary>
		/// Returns a value indicating if two bounding boxes intersect.
		/// </summary>
		/// <param name="a">The first bounding box.</param>
		/// <param name="b">The second bounding box.</param>
		/// <returns>True if they intersect, false otherwise.</returns>
		public static bool Intersects(BoundingBox a, BoundingBox b)
		{
			return
				!(a.Min.X < b.Max.X && a.Max.X > b.Min.X &&
				a.Min.Y < b.Max.Y && a.Max.Y > b.Min.Y &&
				a.Min.Z < b.Max.Z && a.Max.Z > b.Max.Z);
		}

		/// <summary>
		/// Tests the bounding box against a plane.
		/// </summary>
		/// <param name="plane">The plane to test.</param>
		/// <returns>A value indicating the result.</returns>
		public FrustumResult TestPlane(Plane plane)
		{
			Vector3 p = new Vector3
			(
				plane.Normal.X >= 0 ? Max.X : Min.X,
				plane.Normal.Y >= 0 ? Max.Y : Min.Y,
				plane.Normal.Z >= 0 ? Max.Z : Min.Z
			);

			Vector3 n = new Vector3
			(
				plane.Normal.X >= 0 ? Min.X : Max.X,
				plane.Normal.Y >= 0 ? Min.Y : Max.Y,
				plane.Normal.Z >= 0 ? Min.Z : Max.Z
			);

			if (plane.Distance(p) < 0)
				return FrustumResult.Outside;
			else if (plane.Distance(n) < 0)
				return FrustumResult.Intersecting;

			return FrustumResult.Inside;
		}

		/// <summary>
		/// Calculates a bounding box that contains all the provided points.
		/// </summary>
		/// <param name="points">The points to contain.</param>
		/// <returns>The containing bounding box.</returns>
		public static BoundingBox FromPoints(params Vector3[] points)
		{
			return FromPoints((IEnumerable<Vector3>)points);
		}

		/// <summary>
		/// Calculates a bounding box that contains all the provided points.
		/// </summary>
		/// <param name="points">The points to contain.</param>
		/// <returns>The containing bounding box.</returns>
		public static BoundingBox FromPoints(IEnumerable<Vector3> points)
		{
			BoundingBox box = new BoundingBox();
			box.Max = new Vector3(Single.MinValue);
			box.Min = new Vector3(Single.MaxValue);

			foreach (Vector3 point in points)
			{
				box.Min.X = Math.Min(point.X, box.Min.X);
				box.Min.Y = Math.Min(point.Y, box.Min.Y);
				box.Min.Z = Math.Min(point.Z, box.Min.Z);

				box.Max.X = Math.Max(point.X, box.Max.X);
				box.Max.Y = Math.Max(point.Y, box.Max.Y);
				box.Max.Z = Math.Max(point.Z, box.Max.Z);
			}

			return box;
		}

		/// <summary>
		/// Creates a bounding box that minimally encompasses the provided bounding boxs.
		/// </summary>
		/// <param name="boundingBoxes">The bounding boxs to check.</param>
		/// <returns>The encompassing bounding box.</returns>
		public static BoundingBox FromBoundingBoxes(params BoundingBox[] boundingBoxes)
		{
			BoundingBox box = new BoundingBox();
			box.Min = new Vector3(Single.MaxValue);
			box.Max = new Vector3(Single.MinValue);

			for (int i = 0; i < boundingBoxes.Length; i++)
			{
				box.Min.X = Math.Min(boundingBoxes[i].Min.X, box.Min.X);
				box.Min.Y = Math.Min(boundingBoxes[i].Min.Y, box.Min.Y);
				box.Min.Z = Math.Min(boundingBoxes[i].Min.Z, box.Min.Z);

				box.Max.X = Math.Max(boundingBoxes[i].Max.X, box.Max.X);
				box.Max.Y = Math.Max(boundingBoxes[i].Max.Y, box.Max.Y);
				box.Max.Z = Math.Max(boundingBoxes[i].Max.Z, box.Max.Z);
			}

			return box;
		}

		/// <summary>
		/// Transforms a bounding box according to the provided matrix,
		/// </summary>
		/// <param name="bounds">The affected bounding box.</param>
		/// <param name="matrix">The matrix to transform the bounding box by.</param>
		/// <returns>The containing bounding box.</returns>
		public static BoundingBox Transform(BoundingBox bounds, Matrix matrix)
		{
			return BoundingBox.FromPoints
			(
				// Back
				Vector3.Transform(new Vector3(bounds.Min.X, bounds.Min.Y, bounds.Min.Z), matrix),
				Vector3.Transform(new Vector3(bounds.Max.X, bounds.Min.Y, bounds.Min.Z), matrix),
				Vector3.Transform(new Vector3(bounds.Min.X, bounds.Max.Y, bounds.Min.Z), matrix),
				Vector3.Transform(new Vector3(bounds.Max.X, bounds.Max.Y, bounds.Min.Z), matrix),

				// Front
				Vector3.Transform(new Vector3(bounds.Min.X, bounds.Min.Y, bounds.Max.Z), matrix),
				Vector3.Transform(new Vector3(bounds.Max.X, bounds.Min.Y, bounds.Max.Z), matrix),
				Vector3.Transform(new Vector3(bounds.Min.X, bounds.Max.Y, bounds.Max.Z), matrix),
				Vector3.Transform(new Vector3(bounds.Max.X, bounds.Max.Y, bounds.Max.Z), matrix)
			);
		}

		/// <summary>
		/// Gets each corner of the bounding box.
		/// </summary>
		/// <param name="bounds">The bounding box.</param>
		/// <returns>The eight corners of the bounding box.</returns>
		public static Vector3[] GetPoints(BoundingBox bounds)
		{
			return new Vector3[]
			{
				new Vector3(bounds.Min.X, bounds.Min.Y, bounds.Min.Z),
				new Vector3(bounds.Max.X, bounds.Min.Y, bounds.Min.Z),
				new Vector3(bounds.Min.X, bounds.Max.Y, bounds.Min.Z),
				new Vector3(bounds.Max.X, bounds.Max.Y, bounds.Min.Z),

				new Vector3(bounds.Min.X, bounds.Min.Y, bounds.Max.Z),
				new Vector3(bounds.Max.X, bounds.Min.Y, bounds.Max.Z),
				new Vector3(bounds.Min.X, bounds.Max.Y, bounds.Max.Z),
				new Vector3(bounds.Max.X, bounds.Max.Y, bounds.Max.Z),
			};
		}
	}
}
