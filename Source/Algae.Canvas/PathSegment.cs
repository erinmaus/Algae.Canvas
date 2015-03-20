using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	public enum PathSegmentType
	{
		Anchor,

		Line,

		QuadraticCurve,

		CubicCurve,

		End
	}

	public abstract class PathSegment : IEnumerable<Vector2>
	{
		public bool IsRelative
		{
			get;
			private set;
		}

		public abstract PathSegmentType SegmentType
		{
			get;
		}

		Vector2[] points;

		public Vector2 this[int index]
		{
			get { return points[index]; }
			protected set { points[index] = value; }
		}

		public int Count
		{
			get { return points.Length; }
		}

		public PathSegment(int count, bool isRelative)
		{
			points = new Vector2[count];
			IsRelative = isRelative;
		}

		public void MakeRelative(Vector2 previousPosition)
		{
			if (!IsRelative)
			{
				for (int i = 0; i < points.Length; i++)
				{
					points[i] -= previousPosition;
				}

				IsRelative = true;
			}
		}

		public void MakeAbsolute(Vector2 previousPosition)
		{
			if (IsRelative)
			{
				for (int i = 0; i < points.Length; i++)
				{
					points[i] += previousPosition;
				}

				IsRelative = false;
			}
		}

		public IEnumerator<Vector2> GetEnumerator()
		{
			return points.AsEnumerable<Vector2>().GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
