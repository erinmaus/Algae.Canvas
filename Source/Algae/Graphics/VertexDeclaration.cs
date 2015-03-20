using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// An enumeration of the possible types a vertex element can be.
	/// </summary>
	public enum VertexElementType
	{
		/// <summary>
		/// The vertex element is a single-precision floating point number.
		/// </summary>
		Single,

		/// <summary>
		/// The vertex element is a 32-bit integer.
		/// </summary>
		Integer
	}

	/// <summary>
	/// An enumeration of the possible contexts a vertex element.
	/// </summary>
	public enum VertexElementContext
	{
		/// <summary>
		/// Specifies an invalid context.
		/// </summary>
		None,

		/// <summary>
		/// Specifies a position.
		/// </summary>
		Position,

		/// <summary>
		/// Specifies a normal.
		/// </summary>
		Normal,

		/// <summary>
		/// Specifies a tangent.
		/// </summary>
		Tangent,

		/// <summary>
		/// Specifies a binormal.
		/// </summary>
		Binormal,

		/// <summary>
		/// Specifies a color.
		/// </summary>
		Color,

		/// <summary>
		/// Specifies a texture.
		/// </summary>
		Texture,

		/// <summary>
		/// Specifiers a custom attribute.
		/// </summary>
		Custom
	}

	/// <summary>
	/// Defines an attribute.
	/// </summary>
	public struct VertexElement
	{
		/// <summary>
		/// The type of the vertex element data members.
		/// </summary>
		public VertexElementType Type;

		/// <summary>
		/// The total number of components.
		/// </summary>
		public int Components;

		/// <summary>
		/// The offset from beginning of the vertex to this element.
		/// </summary>
		public int Offset;

		/// <summary>
		/// The context of the vertex element.
		/// </summary>
		public VertexElementContext Context;

		/// <summary>
		/// Constructs a vertex element.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="components">The components.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="context">The context.</param>
		public VertexElement(VertexElementType type, int components, int offset, VertexElementContext context)
		{
			Type = type;
			Components = components;
			Offset = offset;
			Context = context;
		}
	};

	/// <summary>
	/// A vertex declaration.
	/// </summary>
	public class VertexDeclaration : IEnumerable<VertexElement>
	{
		List<VertexElement> elements = new List<VertexElement>();

		/// <summary>
		/// Gets the amount of elements.
		/// </summary>
		public int Count
		{
			get { return elements.Count; }
		}

		/// <summary>
		/// Gets the size of the vertex.
		/// </summary>
		public int VertexSize
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the vertex element at the provided index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The vertex element.</returns>
		public VertexElement this[int index]
		{
			get { return elements[index]; }
		}

		/// <summary>
		/// Constructs an instance of the vertex declaration.
		/// </summary>
		/// <param name="e">The list of elements.</param>
		public VertexDeclaration(params VertexElement[] e)
		{
			elements.AddRange(e);

			// Calculate the size of the vertex.
			// Assumes the data is tightly packed and 4-bytes per component.
			int size = 0;
			foreach (VertexElement element in elements)
				size += 4 * element.Components;

			VertexSize = size;
		}

		/// <summary>
		/// Implementation of IEnumerable.
		/// </summary>
		public IEnumerator<VertexElement> GetEnumerator()
		{
			return elements.GetEnumerator();
		}

		/// <summary>
		/// Implementation of IEnumerable.
		/// </summary>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return elements.GetEnumerator();
		}
	}
}
