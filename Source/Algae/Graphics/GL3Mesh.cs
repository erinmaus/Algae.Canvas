using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

using OpenTK.Graphics.OpenGL;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// An OpenGL 3 implementation of a compiled mesh.
	/// </summary>
	class GL3Mesh : IMesh
	{
		int vertexBuffer;
		int indexBuffer;

		// Default vertex attribute data.
		// Used when the mesh lacks a vertex element
		// that the material expects.
		struct VertexDefault
		{
			public int Index;
			public Vector4 Value;
		}

		// Element mapping optimization!
		struct VertexMapping
		{
			public int VertexArray;
			public MaterialDefinition Material;
			public VertexDefault[] Defaults;
		}

		int currentVertexMapping = -1;
		List<VertexMapping> vertexMappings = new List<VertexMapping>();

		// The type of the index data.
		DrawElementsType indexType;

		/// <summary>
		/// Specifies the buffer hint based on prior state.
		/// </summary>
		BufferUsageHint Hint
		{
			get
			{
				BufferUsageHint hint = BufferUsageHint.StaticDraw;

				if (IsDynamic)
					hint = BufferUsageHint.DynamicDraw;

				return hint;
			}
		}

		public VertexDeclaration VertexDeclaration
		{
			get;
			private set;
		}

		public bool IsDynamic
		{
			get;
			set;
		}

		public int VertexCount
		{
			get;
			private set;
		}

		public int IndexCount
		{
			get;
			private set;
		}

		public int IndexComponentSize
		{
			get;
			private set;
		}

		public GL3Mesh(VertexDeclaration vertexDeclaration)
		{
			VertexDeclaration = vertexDeclaration;

			GL.GenBuffers(1, out indexBuffer);
			GL.GenBuffers(1, out vertexBuffer);
		}

		public void BufferIndexData<T>(T[] data, int size) where T : struct
		{
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer);
			GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(data.Length * size), data, Hint);

			IndexComponentSize = size;
			IndexCount = data.Length;

			switch (size)
			{
				case 1:
					indexType = DrawElementsType.UnsignedByte;
					break;
				case 2:
					indexType = DrawElementsType.UnsignedShort;
					break;
				case 4:
					indexType = DrawElementsType.UnsignedInt;
					break;
				default:
					// Uh-oh.
					throw new GraphicsException("Unable to determine index type.", "glBufferData");
			}
		}

		public void BufferVertexData<T>(T[] data) where T : struct
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(data.Length * VertexDeclaration.VertexSize), data, Hint);

			VertexCount = data.Length;
		}

		// Returns the vertex array.
		int GetVertexMapping(MaterialDefinition definition)
		{
			foreach (VertexMapping mapping in vertexMappings)
			{
				if (mapping.Material == definition)
					return mapping.VertexArray;
			}

			return 0;
		}

		// Returns the index of the vertex mapping,
		// or a negative value if the vertex mapping
		// does not belong to this mesh.
		int GetVertexMapping(int array)
		{
			int index = 0;

			foreach (VertexMapping mapping in vertexMappings)
			{
				if (mapping.VertexArray == array)
					return index;

				index++;
			}

			return -1;
		}

		// Utility method to map an individual element.
		bool MapElement(MaterialVertexElement element, out VertexElementContext context)
		{
			// Grab the name (and enum) sans any index.
			string name = Regex.Match(element.Context, "^[a-zA-Z]+").Value;

			if (!Enum.TryParse(name, out context))
				return false;

			// Grab any index value.
			string indexString = Regex.Match(element.Context, "[0-9]+$").Value;
			int index;

			// The index value is optional (defaults to 0).
			if (!Int32.TryParse(indexString, out index))
				index = 0;

			// Search for a matching vertex element in the declaration.
			int hits = -1;

			foreach (VertexElement vertexElement in VertexDeclaration)
			{
				if (vertexElement.Context == context)
				{
					// Found a hit.
					hits++;

					if (hits == index)
					{
						// This is the target, so map it as per the material.
						GL.EnableVertexAttribArray(element.Index);
						GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);

						// Use the vertex declaration type.
						if (vertexElement.Type == VertexElementType.Single)
						{
							GL.VertexAttribPointer(element.Index, vertexElement.Components, VertexAttribPointerType.Float, false, VertexDeclaration.VertexSize, new IntPtr(vertexElement.Offset));
						}
						else if (vertexElement.Type == VertexElementType.Integer)
						{
							GL.VertexAttribIPointer(element.Index, vertexElement.Components, VertexAttribIPointerType.Int, VertexDeclaration.VertexSize, new IntPtr(vertexElement.Offset));
						}
					}
				}
			}

			return false;
		}

		public int MapElements(MaterialDefinition material)
		{
			// If the definition has previously been mapped,
			// return it instead of doing this process over.
			int index = GetVertexMapping(material);

			if (index > 0)
				return index;

			// Else, create the new vertex array.
			int vertexArray;

			// Generate the new vertex array and prepare it.
			GL.GenVertexArrays(1, out vertexArray);
			GL.BindVertexArray(vertexArray);

			// Bind the index buffer.
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer);

			// Store a list of vertex elements that are missing from this mesh.
			// These will be set to defaults prior to rendering.
			List<VertexDefault> defaults = new List<VertexDefault>();

			// Bind the individual vertex elements.
			foreach (MaterialVertexElement element in material.VertexElements)
			{
				VertexElementContext context;

				if (!MapElement(element, out context))
				{
					// Supply a default value based on the type.
					// Only neccessary for color and position...
					switch (context)
					{
						case VertexElementContext.Color:
							defaults.Add(new VertexDefault()
							{
								Index = element.Index,
								Value = new Vector4(1, 1, 1, 1)
							});
							break;
					}
				}
			}

			GL.BindVertexArray(0);

			vertexMappings.Add(new VertexMapping()
			{
				Material = material,
				VertexArray = vertexArray,
				Defaults = defaults.ToArray()
			});

			currentVertexMapping = vertexArray;

			return vertexArray;
		}

		public void UseMapping(int mapping)
		{
			// Make sure the mapping is owned by this mesh.
			int index = GetVertexMapping(mapping);

			if (index > 0)
				currentVertexMapping = mapping;
		}

		public void DestroyMapping(int mapping)
		{
			int index = GetVertexMapping(mapping);

			if (index > 0)
				vertexMappings.RemoveAt(index);
		}

		// Utility method to convert a MeshRenderMode enum to an OpenTK BeginMode enum.
		static BeginMode FromMeshRenderMode(MeshRenderMode mode)
		{
			switch (mode)
			{
				case MeshRenderMode.Triangles:
					return BeginMode.Triangles;

				case MeshRenderMode.Points:
					return BeginMode.Points;

				case MeshRenderMode.Lines:
					return BeginMode.Lines;

				default:
					throw new ArgumentException("Invalid render mode.", "mode");
			}
		}

		public void Render(MeshRenderMode mode, int count, int offset = 0)
		{
			if (currentVertexMapping > 0)
			{
				// Prepare the state.
				GL.BindVertexArray(currentVertexMapping);

				// Set sane defaults for missing attributes.
				int vertexMappingIndex = GetVertexMapping(currentVertexMapping);
				foreach (VertexDefault vertexDefault in vertexMappings[vertexMappingIndex].Defaults)
				{
					GL.VertexAttrib4(vertexDefault.Index, vertexDefault.Value.X, vertexDefault.Value.Y, vertexDefault.Value.Z, vertexDefault.Value.W);
				}

				// Render.
				GL.DrawElements(FromMeshRenderMode(mode), count, indexType, offset * IndexComponentSize);

				// Reset.
				GL.BindVertexArray(0);
			}
		}

		public void Dispose()
		{
			GL.DeleteBuffers(1, ref vertexBuffer);
			GL.DeleteBuffers(1, ref indexBuffer);

			foreach (VertexMapping mapping in vertexMappings)
			{
				int array = mapping.VertexArray;

				GL.DeleteVertexArrays(1, ref array);
			}
		}
	}
}
