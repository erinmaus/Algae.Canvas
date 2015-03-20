using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae
{
	/// <summary>
	/// A structure that represents a bounding rectangle.
	/// </summary>
	public struct BoundingRectangle
	{
		/// <summary>
		/// The minimum extent of the bounding rectangle.
		/// </summary>
		public Vector2 Min;

		/// <summary>
		/// The maximum extent of the bounding rectangle.
		/// </summary>
		public Vector2 Max;

		static readonly BoundingRectangle empty = new BoundingRectangle
		(
			Vector2.Zero,
			Vector2.Zero
		);

		/// <summary>
		/// An empty bounding rectangle.
		/// </summary>
		public static BoundingRectangle Empty
		{
			get { return empty; }
		}

		/// <summary>
		/// Gets the position of the bounding rectangle on the X axis.
		/// </summary>
		public float X
		{
			get { return Min.X; }
		}

		/// <summary>
		/// Gets the position of the bounding rectangle on the Y axis.
		/// </summary>
		public float Y
		{
			get { return Min.Y; }
		}

		/// <summary>
		/// Gets the width of the bounding rectangle.
		/// </summary>
		public float Width
		{
			get { return Max.X - Min.X; }
		}

		/// <summary>
		/// Gets the height of the bounding rectangle.
		/// </summary>
		public float Height
		{
			get { return Max.Y - Min.Y; }
		}

		/// <summary>
		/// Gets the top left point of the bounding rectangle.
		/// </summary>
		public Vector2 TopLeft
		{
			get { return new Vector2(Min.X, Max.Y);  }
		}

		/// <summary>
		/// Gets the top right point of the bounding rectangle.
		/// </summary>
		public Vector2 TopRight
		{
			get { return new Vector2(Max.X, Max.Y); }
		}

		/// <summary>
		/// Gets the bottom left point of the bounding rectangle.
		/// </summary>
		public Vector2 BottomLeft
		{
			get { return new Vector2(Min.X, Min.Y);  }
		}

		/// <summary>
		/// Gets the bottom right point of the bounding rectangle.
		/// </summary>
		public Vector2 BottomRight
		{
			get { return new Vector2(Max.X, Max.Y); }
		}

		/// <summary>
		/// Constructs a bounding rectangle.
		/// </summary>
		/// <param name="min">The minimum extent of the bounding rectangle.</param>
		/// <param name="max">The maximum extent of the bounding rectangle.</param>
		public BoundingRectangle(Vector2 min, Vector2 max)
		{
			Min = min;
			Max = max;
		}

		/// <summary>
		/// Expands a bounding rectangle.
		/// </summary>
		/// <param name="rectangle">The bounding rectangle to expand.</param>
		/// <param name="point">The point to (possibly) expand the rectangle.</param>
		/// <returns>The expanded bounding rectangle.</returns>
		public static BoundingRectangle Expand(BoundingRectangle rectangle, Vector2 point)
		{
			return BoundingRectangle.FromPoints(rectangle.Min, rectangle.Max, point);
		}

		/// <summary>
		/// Returns a value indicating if two bounding rectangles intersect.
		/// </summary>
		/// <param name="a">The first bounding rectangle.</param>
		/// <param name="b">The second bounding rectangle.</param>
		/// <returns>True if they intersect, false otherwise.</returns>
		public static bool Intersects(BoundingRectangle a, BoundingRectangle b)
		{
			return
				a.Min.X < b.Max.X && a.Max.X > b.Min.X &&
				a.Min.Y < b.Max.Y && a.Max.Y > b.Min.Y;
		}

		/// <summary>
		/// Creates a bounding rectangle from a position and size.
		/// </summary>
		/// <param name="position">The position of the bounding rectangle.</param>
		/// <param name="width">The width of the bounding rectangle.</param>
		/// <param name="height">The height of the bounding rectangle.</param>
		/// <returns></returns>
		public static BoundingRectangle FromPositionSize(Vector2 position, float width, float height)
		{
			return new BoundingRectangle(position, new Vector2(width, height) + position);
		}

		/// <summary>
		/// Creates a bounding rectangle that minimally encompasses the provided bounding rectangles.
		/// </summary>
		/// <param name="boundingRectangles">The bounding rectangles to check.</param>
		/// <returns>The encompassing bounding rectangle.</returns>
		public static BoundingRectangle FromBoundingRectangles(params BoundingRectangle[] boundingRectangles)
		{
			BoundingRectangle box = new BoundingRectangle();
			box.Min = new Vector2(Single.MaxValue);
			box.Max = new Vector2(Single.MinValue);

			for (int i = 0; i < boundingRectangles.Length; i++)
			{
				box.Min.X = Math.Min(boundingRectangles[i].Min.X, box.Min.X);
				box.Min.Y = Math.Min(boundingRectangles[i].Min.Y, box.Min.Y);

				box.Max.X = Math.Max(boundingRectangles[i].Max.X, box.Max.X);
				box.Max.Y = Math.Max(boundingRectangles[i].Max.Y, box.Max.Y);
			}

			return box;
		}

		/// <summary>
		/// Creates a bounding rectangle that minimally encompasses the provided points.
		/// </summary>
		/// <param name="points">The points to check.</param>
		/// <returns>The encompassing bounding rectangle.</returns>
		public static BoundingRectangle FromPoints(params Vector2[] points)
		{
			BoundingRectangle box = new BoundingRectangle();
			box.Min = new Vector2(Single.MaxValue);
			box.Max = new Vector2(Single.MinValue);

			for (int i = 0; i < points.Length; i++)
			{
				box.Min.X = Math.Min(points[i].X, box.Min.X);
				box.Min.Y = Math.Min(points[i].Y, box.Min.Y);

				box.Max.X = Math.Max(points[i].X, box.Max.X);
				box.Max.Y = Math.Max(points[i].Y, box.Max.Y);
			}

			return box;
		}

		/// <summary>
		/// Transforms a bounding rectangle by the provided matrix.
		/// </summary>
		/// <param name="matrix">The matrix.</param>
		/// <param name="bounds">The bounds.</param>
		/// <returns>The containing bounding rectangle.</returns>
		public static BoundingRectangle Transform(Matrix matrix, BoundingRectangle bounds)
		{
			return BoundingRectangle.FromPoints
			(
				Vector2.Transform(bounds.TopLeft, matrix),
				Vector2.Transform(bounds.TopRight, matrix),
				Vector2.Transform(bounds.BottomLeft, matrix),
				Vector2.Transform(bounds.BottomRight, matrix)
			);
		}

		/// <summary>
		/// Tests if a bounding rectangle contains the provided point completely.
		/// </summary>
		/// <param name="bounds">The bounds.</param>
		/// <param name="point">The point.</param>
		/// <returns>A value indicating if the test succeeded.</returns>
		public static bool ContainsPoint(BoundingRectangle bounds, Vector2 point)
		{
			return point.X > bounds.Min.X && point.X < bounds.Max.X && point.Y > bounds.Min.Y && point.Y < bounds.Max.Y;
		}
	}
}
