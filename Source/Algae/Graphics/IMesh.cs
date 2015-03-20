using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// Mesh render mode.
	/// </summary>
	public enum MeshRenderMode
	{
		/// <summary>
		/// The mesh should be rendered as a group of triangles.
		/// </summary>
		Triangles,

		/// <summary>
		/// The mesh should be rendered as a group of points.
		/// </summary>
		Points,

		/// <summary>
		/// The mesh should be rendered as a group of lines.
		/// </summary>
		Lines
	}

	/// <summary>
	/// Defines a mesh, or a composition of geometry.
	/// </summary>
	interface IMesh : IDisposable
	{
		/// <summary>
		/// Gets the vertex declaration that defines this mesh.
		/// </summary>
		VertexDeclaration VertexDeclaration
		{
			get;
		}

		/// <summary>
		/// Gets or sets a value indicating if the mesh is updated often.
		/// </summary>
		bool IsDynamic
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the number of vertices stored in the mesh.
		/// </summary>
		int VertexCount
		{
			get;
		}

		/// <summary>
		/// Gets the number of indices stored in the mesh.
		/// </summary>
		int IndexCount
		{
			get;
		}

		/// <summary>
		/// Gets the size of an index stored by the mesh.
		/// </summary>
		int IndexComponentSize
		{
			get;
		}

		/// <summary>
		/// Buffers index data.
		/// </summary>
		/// <typeparam name="T">The type of the index data.</typeparam>
		/// <param name="data">An array of unsigned index data (1, 2, or 4 bytes in size).</param>
		/// <param name="size">The size of the index data.</param>
		void BufferIndexData<T>(T[] data, int size) where T : struct;

		/// <summary>
		/// Buffers vertex data.
		/// </summary>
		/// <typeparam name="T">The type of the vertex data.</typeparam>
		/// <param name="data">An array of the vertex data.</param>
		void BufferVertexData<T>(T[] data) where T : struct;

		/// <summary>
		/// Maps the mesh's vertex elements to the provided material.
		/// </summary>
		/// <param name="material">The material.</param>
		int MapElements(MaterialDefinition material);

		/// <summary>
		/// Uses a previously defined mapping.
		/// </summary>
		/// <param name="mapping">The mapping to use.</param>
		void UseMapping(int mapping);

		/// <summary>
		/// Destroys the provided vertex element mapping.
		/// </summary>
		/// <param name="mapping">The mapping to destroy.</param>
		/// <remarks>This value was previously returned by MapElements.</remarks>
		void DestroyMapping(int mapping);

		/// <summary>
		/// Renders the mesh.
		/// </summary>
		/// <param name="mode">The mode.</param>
		/// <param name="count">The amount of indices to render.</param>
		/// <param name="offset">The offset from the beginning of the index data.</param>
		void Render(MeshRenderMode mode, int count, int offset = 0);
	}
}
