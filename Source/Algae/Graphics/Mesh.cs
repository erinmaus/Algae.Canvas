using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// Defines a mesh, or a composition of geometry.
	/// </summary>
	public class Mesh : IDisposable
	{
		IMesh mesh;

		/// <summary>
		/// Gets the vertex declaration that defines this mesh.
		/// </summary>
		public VertexDeclaration VertexDeclaration
		{
			get { return mesh.VertexDeclaration; }
		}

		/// <summary>
		/// Gets the number of vertices stored in the mesh.
		/// </summary>
		public int VertexCount
		{
			get { return mesh.VertexCount; }
		}

		/// <summary>
		/// Gets the number of indices stored in the mesh.
		/// </summary>
		public int IndexCount
		{
			get { return mesh.IndexCount; }
		}

		/// <summary>
		/// Gets the size of an index stored by the mesh.
		/// </summary>
		public int IndexComponentSize
		{
			get { return mesh.IndexComponentSize; }
		}

		/// <summary>
		/// Gets or sets a value indicating if the mesh is updated often.
		/// </summary>
		public bool IsDynamic
		{
			get { return mesh.IsDynamic; }
			set { mesh.IsDynamic = value; }
		}

		bool cacheBuffers = true;

		/// <summary>
		/// Gets or sets if the buffers should be cached.
		/// </summary>
		public bool CacheBuffers
		{
			get { return cacheBuffers; }
			set
			{
				cacheBuffers = value;

				if (!cacheBuffers)
				{
					cachedIndexData = null;
					cachedVertexData = null;
				}
			}
		}

		Array cachedVertexData, cachedIndexData;

		/// <summary>
		/// Creates a mesh.
		/// </summary>
		/// <param name="renderer">The renderer.</param>
		/// <param name="vertexDeclaration">The vertex declaration.</param>
		public Mesh(Renderer renderer, VertexDeclaration vertexDeclaration)
		{
			mesh = renderer.CreateMesh(vertexDeclaration);
		}

		/// <summary>
		/// Buffers index data.
		/// </summary>
		/// <typeparam name="T">The type of the index data.</typeparam>
		/// <param name="data">An array of unsigned index data (1, 2, or 4 bytes in size).</param>
		/// <param name="size">The size of the index data.</param>
		public void BufferIndexData<T>(T[] data, int size) where T : struct
		{
			mesh.BufferIndexData(data, size);

			if (CacheBuffers)
				cachedIndexData = data;
		}

		/// <summary>
		/// Gets cached index data.
		/// </summary>
		/// <typeparam name="T">The type of the index data.</typeparam>
		/// <returns>The cached index data.</returns>
		public T[] GetCachedIndexData<T>() where T : struct
		{
			return cachedIndexData as T[];
		}

		/// <summary>
		/// Buffers vertex data.
		/// </summary>
		/// <typeparam name="T">The type of the vertex data.</typeparam>
		/// <param name="data">An array of the vertex data.</param>
		public void BufferVertexData<T>(T[] data) where T : struct
		{
			mesh.BufferVertexData(data);

			if (CacheBuffers)
				cachedVertexData = data;
		}

		/// <summary>
		/// Gets the cached vertex data.
		/// </summary>
		/// <typeparam name="T">The type of the vertex data.</typeparam>
		/// <returns>The cached vertex data.</returns>
		public T[] GetCachedVertexData<T>() where T : struct
		{
			return cachedVertexData as T[];
		}

		/// <summary>
		/// Maps the mesh's vertex elements to the provided material.
		/// </summary>
		/// <param name="definition">The material.</param>
		/// <returns>A mesh-specific mapping handle.</returns>
		public int MapElements(MaterialDefinition definition)
		{
			return mesh.MapElements(definition);
		}

		/// <summary>
		/// Uses a previously defined mapping.
		/// </summary>
		/// <param name="mapping">The mapping to use.</param>
		public void UseMapping(int mapping)
		{
			mesh.UseMapping(mapping);
		}

		/// <summary>
		/// Destroys the provided vertex element mapping.
		/// </summary>
		/// <param name="mapping">The mapping to destroy.</param>
		/// <remarks>This value was previously returned by MapElements.</remarks>
		public void DestroyMapping(int mapping)
		{
			mesh.DestroyMapping(mapping);
		}

		/// <summary>
		/// Renders the mesh.
		/// </summary>
		/// <param name="mode">The mode.</param>
		/// <param name="count">The amount of indices to render.</param>
		/// <param name="offset">The offset from the beginning of the index data.</param>
		public void Render(MeshRenderMode mode, int count, int offset = 0)
		{
			mesh.Render(mode, count, offset);
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
