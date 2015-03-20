using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TriangleNet.Geometry;
using ITriangleMesh = TriangleNet.Meshing.IMesh;

namespace CommaExcess.Algae.Graphics
{
	static class PathCompiler
	{
		static void ExtractContours(List<List<PathSegment>> contours, Path path)
		{
			List<PathSegment> currentContour = null;
			int currentSegment = 0;

			while (currentSegment < path.Count)
			{
				if (path[currentSegment].SegmentType == PathSegmentType.Anchor)
				{
					if (currentContour != null)
						contours.Add(currentContour);

					currentContour = new List<PathSegment>();
				}

				if (path[currentSegment].SegmentType != PathSegmentType.End)
					currentContour.Add(path[currentSegment]);

				currentSegment++;
			}

			contours.Add(currentContour);
		}

		static int GetContourWinding(List<PathSegment> contour)
		{
			float accum = 0.0f;
			Vector2[] points = contour.SelectMany(s => s).ToArray();

			for (int i = 0; i < points.Length; i++)
			{
				Vector2 a = points[i];
				Vector2 b = points[(i + 1) % points.Length];

				accum += (b.X - a.X) * (b.Y + a.Y);
			}

			return Math.Sign(accum);
		}

		static void ExtractShape(List<Vector2> shape, List<PathSegment> contour)
		{
			shape.AddRange(contour.Where(s => s.Count > 0).Select(s => s[s.Count - 1]));
		}

		static bool IsInsideShape(List<Vector2> shape, Vector2 point)
		{
			int count = 0;

			for (int i = 0; i < shape.Count; i++)
			{
				Vector2 p1 = shape[i];
				Vector2 p2 = shape[(i + 1) % shape.Count];

				if (point.Y > Math.Min(p1.Y, p2.Y) && point.Y <= Math.Max(p1.Y, p2.Y) && point.X <= Math.Max(p1.X, p2.X) && p1.Y != p2.Y)
				{
					float intersection = (point.Y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y) + p1.X;

					if (p1.X == p2.X || point.X <= intersection)
						count++;
				}
			}

			return count % 2 == 1;
		}

		static void GetContourVertices(List<Vertex> vertices, List<PathVertex> exterior, List<PathSegment> contour, bool isHole)
		{
			List<Vector2> shape = new List<Vector2>();
			ExtractShape(shape, contour);

			Vector2 lastPosition = Vector2.Zero;
			int currentSegmentIndex = 0;
			while (currentSegmentIndex < contour.Count)
			{
				PathSegment currentSegment = contour[currentSegmentIndex++];

				switch (currentSegment.SegmentType)
				{
					case PathSegmentType.Anchor:
					case PathSegmentType.Line:
						vertices.Add(new Vertex(currentSegment[0].X, currentSegment[0].Y));
						break;
					case PathSegmentType.QuadraticCurve:
						{
							float va = (currentSegment[1].X - lastPosition.X) * (currentSegment[0].Y - lastPosition.Y);
							float vb = (currentSegment[1].Y - lastPosition.Y) * (currentSegment[0].X - lastPosition.X);
							int sign = Math.Sign(MathHelper.Round(va - vb));

							// Now add exterior, unless it's a line.
							if (sign != 0)
							{
								int r;
								bool inside = IsInsideShape(shape, currentSegment[0]);

								if (inside)
								{
									if (isHole)
									{
										r = 1;
									}
									else
									{
										vertices.Add(new Vertex(currentSegment[0].X, currentSegment[0].Y));
										r = -1;
									}
								}
								else
								{
									if (isHole)
									{
										vertices.Add(new Vertex(currentSegment[0].X, currentSegment[0].Y));
										r = -1;
									}
									else
									{
										r = 1;
									}
								}

								exterior.Add(new PathVertex() { Position = lastPosition, Coefficient = new Vector2(0.0f, 0.0f), Sign = r });
								exterior.Add(new PathVertex() { Position = currentSegment[0], Coefficient = new Vector2(0.5f, 0.0f), Sign = r });
								exterior.Add(new PathVertex() { Position = currentSegment[1], Coefficient = new Vector2(1.0f, 1.0f), Sign = r });
							}

							vertices.Add(new Vertex(currentSegment[1].X, currentSegment[1].Y));
						}
						break;
				}

				lastPosition = currentSegment[currentSegment.Count - 1];
			}
		}

		static CachedPath GenerateMesh(Polygon polygon, List<PathVertex> exteriorVertices)
		{
			PathVertex[] vertices;
			int vertexCount;
			int verticesIndex = 0;
			int indexCount;
			int indicesIndex = 0;
			ITriangleMesh triangleMesh = null;
			uint[] indices;

			if (polygon.Count >= 3)
			{
				triangleMesh = polygon.Triangulate();

				vertexCount = triangleMesh.Vertices.Count + exteriorVertices.Count;
				vertices = new PathVertex[vertexCount];
				foreach (var vertex in triangleMesh.Vertices)
				{
					vertices[verticesIndex++] = new PathVertex() { Position = new Vector2((float)vertex.X, (float)vertex.Y), Coefficient = Vector2.One, Sign = 0 };
				}

				indexCount = triangleMesh.Triangles.Count * 3 + exteriorVertices.Count * 3;
				indices = new uint[indexCount];
				foreach (var triangle in triangleMesh.Triangles)
				{
					indices[indicesIndex++] = (uint)triangle.P0;
					indices[indicesIndex++] = (uint)triangle.P1;
					indices[indicesIndex++] = (uint)triangle.P2;
				}
			}
			else
			{
				vertexCount = exteriorVertices.Count;
				vertices = new PathVertex[vertexCount];
				indexCount = exteriorVertices.Count * 3;
				indices = new uint[indexCount];
			}

			for (int i = 0; i < exteriorVertices.Count; i++)
			{
				vertices[verticesIndex + i] = exteriorVertices[i];
			}

			for (int i = 0; i < exteriorVertices.Count; i++)
			{
				indices[indicesIndex + i] = (uint)(verticesIndex + i);
			}

			CachedPath mesh = new CachedPath(vertices, indices);
			return mesh;
		}

		static bool IsHole(int winding, int boundary, CanvasPathFillRule rule)
		{
			if (rule == CanvasPathFillRule.FontNonZero)
				return winding < 0;
			else // EvenOdd is unsupported so use default (NonZero).
				return winding != boundary;
		}

		public static CachedPath Compile(Path path)
		{
			List<List<PathSegment>> contours = new List<List<PathSegment>>();
			ExtractContours(contours, path);

			List<PathVertex> exteriorVertices = new List<PathVertex>();
			Polygon polygon = new Polygon();

			int boundaryWinding = GetContourWinding(contours[0]); 
			for (int i = 0; i < contours.Count; i++)
			{
				List<Vertex> contourVertices = new List<Vertex>();
				int winding = (i == 0) ? boundaryWinding : GetContourWinding(contours[i]);
				bool isHole = IsHole(winding, boundaryWinding, path.FillRule);

				GetContourVertices(contourVertices, exteriorVertices, contours[i], isHole);

				if (contourVertices.Count > 0)
				{
					polygon.AddContour(contourVertices, 0, isHole, false);
				}
			}

			return GenerateMesh(polygon, exteriorVertices);
		}
	}
}
