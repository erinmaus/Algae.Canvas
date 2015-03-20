using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TriangleNet.Geometry;
using ITriangleMesh = TriangleNet.Meshing.IMesh;

namespace CommaExcess.Algae.Graphics
{
	public struct PathVertex
	{
		public Vector2 Position;

		public Vector2 Coefficient;

		public int Sign;

		public int Index;

		public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration
		(
			new VertexElement(VertexElementType.Single, 2, 0, VertexElementContext.Position),
			new VertexElement(VertexElementType.Single, 2, 8, VertexElementContext.Custom),
			new VertexElement(VertexElementType.Integer, 1, 16, VertexElementContext.Custom),
			new VertexElement(VertexElementType.Integer, 1, 20, VertexElementContext.Custom)
		);
	}

	/// <summary>
	/// Defines the fill rule for a path.
	/// </summary>
	public enum CanvasPathFillRule
	{
		/// <summary>
		/// Default. Consider the boundary as the first contour. Shapes that match its winding are filled, shapes that don't are solid.
		/// </summary>
		NonZero,

		/// <summary>
		/// Contours that are clockwise are filled; contours that are counter-clockwise are considered holes.
		/// </summary>
		FontNonZero,

		/// <summary>
		/// Unimplemented.
		/// </summary>
		EvenOdd
	}

	public class Path : IEnumerable<PathSegment>, IDisposable
	{
		List<PathSegment> segments = new List<PathSegment>();

		public CanvasPathFillRule FillRule
		{
			get;
			set;
		}

		public int Count
		{
			get { return segments.Count; }
		}

		public PathSegment this[int index]
		{
			get { return segments[index]; }
		}

		public bool IsFinished
		{
			get
			{
				if (Count > 0 && this[Count - 1].SegmentType == PathSegmentType.End)
					return true;

				return false;
			}
		}

		public CachedPath CachedMesh
		{
			get;
			private set;
		}

		Mesh mesh;
		public Mesh Mesh
		{
			get { return mesh; }
			set
			{
				if (mesh != null)
					mesh.Dispose();

				mesh = value;
			}
		}

		Vector2 lastPosition;

		public Path()
		{
			// Nothing.
		}

		void AddSegment(PathSegment segment)
		{
			if (IsFinished)
				throw new InvalidOperationException("Path is finished.");

			segment.MakeAbsolute(lastPosition);
			segments.Add(segment);

			if (segment.SegmentType != PathSegmentType.End)
				lastPosition = segment[segment.Count - 1];
		}

		public void MoveTo(Vector2 position, bool isRelative = false)
		{
			AddSegment(new AnchorPathSegment(position, isRelative));
		}

		public void LineTo(Vector2 position, bool isRelative = false)
		{
			AddSegment(new LinePathSegment(position, isRelative));
		}

		public void QuadraticCurveTo(Vector2 control, Vector2 position, bool isRelative = false)
		{
			AddSegment(new QuadraticCurvePathSegment(control, position, isRelative));
		}

		public void CubicCurveTo(Vector2 control1, Vector2 control2, Vector2 position, bool isRelative = false)
		{
			if (isRelative)
			{
				control1 += lastPosition;
				control2 += lastPosition;
				position += lastPosition;
			}

			Vector2 pa = Vector2.Lerp(lastPosition, control1, 3.0f / 4.0f);
			Vector2 pb = Vector2.Lerp(position, control2, 3.0f / 4.0f);
			Vector2 d = (position - lastPosition) / 16.0f;

			Vector2 c1 = Vector2.Lerp(lastPosition, control1, 3.0f / 8.0f);
			Vector2 c2 = Vector2.Lerp(pa, pb, 3.0f / 8.0f) - d;
			Vector2 c3 = Vector2.Lerp(pb, pa, 3.0f / 8.0f) + d;
			Vector2 c4 = Vector2.Lerp(position, control2, 3.0f / 8.0f);

			Vector2 a1 = Vector2.Lerp(c1, c2, 0.5f);
			Vector2 a2 = Vector2.Lerp(pa, pb, 0.5f);
			Vector2 a3 = Vector2.Lerp(c3, c4, 0.5f);
			
			QuadraticCurveTo(c1, a1);
			QuadraticCurveTo(c2, a2);
			QuadraticCurveTo(c3, a3);
			QuadraticCurveTo(c4, position);
		}

		public void End()
		{
			AddSegment(new EndPathSegment());
		}

		public void Reset()
		{
			segments.Clear();

			if (Mesh != null)
			{
				Mesh.Dispose();
				Mesh = null;
			}
		}

		public bool Compile()
		{
			if (IsFinished && CachedMesh == null)
				CachedMesh = PathCompiler.Compile(this);

			return CachedMesh != null;
		}

		public void Dispose()
		{
			Mesh = null;
		}

		public IEnumerator<PathSegment> GetEnumerator()
		{
			return segments.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
