using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	public class CachedPath
	{
		PathVertex[] vertices;
		uint[] indices;

		public CachedPath(PathVertex[] vertices, uint[] indices)
		{
			this.vertices = vertices;
			this.indices = indices;
		}

		public Mesh GenerateMesh(Renderer renderer)
		{
			Mesh mesh = new Mesh(renderer, PathVertex.VertexDeclaration);
			mesh.BufferVertexData(vertices);
			mesh.BufferIndexData(indices, 4);

			return mesh;
		}

		public PathVertex[] GetVertices()
		{
			return vertices;
		}

		public uint[] GetIndices()
		{
			return indices;
		}

		public PathVertex[] TransformVertices(Matrix m, int index)
		{
			PathVertex[] transformedVertices = new PathVertex[vertices.Length];

			for (int i = 0; i < transformedVertices.Length; i++)
			{
				transformedVertices[i].Position = Vector2.Transform(vertices[i].Position, m);
				transformedVertices[i].Coefficient = vertices[i].Coefficient;
				transformedVertices[i].Sign = vertices[i].Sign;
				transformedVertices[i].Index = index;
			}

			return transformedVertices;
		}

		public uint[] TransformIndices(uint baseIndex)
		{
			uint[] transformedIndices = new uint[indices.Length];

			for (int i = 0; i < transformedIndices.Length; i++)
			{
				transformedIndices[i] = indices[i] + baseIndex;
			}

			return transformedIndices;
		}
	}
}
