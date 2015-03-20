using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// Defines a quad vertex.
	/// </summary>
	public struct QuadVertex
	{
		/// <summary>
		/// The position of the quad vertex.
		/// </summary>
		public Vector3 Position;

		/// <summary>
		/// The texture coordinate of the quad vertex.
		/// </summary>
		public Vector2 Texture;

		/// <summary>
		/// The vertex declaration of the vertex.
		/// </summary>
		public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration
		(
			new VertexElement(VertexElementType.Single, 3, 0, VertexElementContext.Position),
			new VertexElement(VertexElementType.Single, 2, 12, VertexElementContext.Texture)
		);
	}

	/// <summary>
	/// A simple mesh.
	/// </summary>
	public class Quad : IDisposable
	{
		Mesh mesh;

		/// <summary>
		/// Constructs a simple quad.
		/// </summary>
		/// <param name="renderer">The renderer.</param>
		public Quad(Renderer renderer)
		{
			QuadVertex[] vertices = new QuadVertex[]
			{
				new QuadVertex() { Position = new Vector3(-1.0f, 1.0f, -1.0f), Texture = new Vector2(0.0f, 1.0f) },
				new QuadVertex() { Position = new Vector3(1.0f, 1.0f, -1.0f), Texture = new Vector2(1.0f, 1.0f) },
				new QuadVertex() { Position = new Vector3(-1.0f, -1.0f, -1.0f), Texture = new Vector2(0.0f, 0.0f) },
				new QuadVertex() { Position = new Vector3(1.0f, -1.0f, -1.0f), Texture = new Vector2(1.0f, 0.0f) }
			};

			uint[] indices = new uint[]
			{
				0, 1, 2,
				1, 3, 2
			};

			mesh = new Mesh(renderer, QuadVertex.VertexDeclaration);
			mesh.BufferVertexData(vertices);
			mesh.BufferIndexData(indices, 4);
		}

		/// <summary>
		/// Maps the elements of the quad.
		/// </summary>
		/// <param name="definition">The material definition.</param>
		public void MapElements(MaterialDefinition definition)
		{
			mesh.MapElements(definition);
		}

		/// <summary>
		/// Renders the quad.
		/// </summary>
		public void Render()
		{
			mesh.Render(MeshRenderMode.Triangles, mesh.IndexCount);
		}

		/// <summary>
		/// Implementation of IDisposable.
		/// </summary>
		public void Dispose()
		{
			mesh.Dispose();
		}
	}
}
