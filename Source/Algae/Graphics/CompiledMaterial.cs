using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// Defines a compiled material pass.
	/// </summary>
	public class CompiledMaterialPass
	{
		ICompiledMaterialPass pass;

		/// <summary>
		/// Wraps the provided pass.
		/// </summary>
		/// <param name="pass">The pass to wrap.</param>
		internal CompiledMaterialPass(ICompiledMaterialPass pass)
		{
			this.pass = pass;
		}

		/// <summary>
		/// Begins rendering the material pass.
		/// </summary>
		public void Begin()
		{
			pass.Begin();
		}

		/// <summary>
		/// Applies any changes.
		/// </summary>
		public void Apply()
		{
			pass.Apply();
		}

		/// <summary>
		/// Sets a parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="value">The value.</param>
		public void SetValue(string parameter, float value)
		{
			pass.SetValue(parameter, value);
		}

		/// <summary>
		/// Sets a parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="value">The value.</param>
		public void SetValue(string parameter, int value)
		{
			pass.SetValue(parameter, value);
		}

		/// <summary>
		/// Sets a parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="value">The value.</param>
		public void SetValue(string parameter, Vector2 value)
		{
			pass.SetValue(parameter, value);
		}

		/// <summary>
		/// Sets a parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="value">The value.</param>
		public void SetValue(string parameter, Vector3 value)
		{
			pass.SetValue(parameter, value);
		}

		/// <summary>
		/// Sets a parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="value">The value.</param>
		public void SetValue(string parameter, Vector4 value)
		{
			pass.SetValue(parameter, value);
		}

		/// <summary>
		/// Sets a parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="value">The value.</param>
		public void SetValue(string parameter, Matrix value)
		{
			pass.SetValue(parameter, value);
		}

		/// <summary>
		/// Sets a parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="value">The value.</param>
		public void SetValue(string parameter, Texture value)
		{
			pass.SetValue(parameter, value);
		}

		/// <summary>
		/// Ends the material pass.
		/// </summary>
		public void End()
		{
			pass.End();
		}
	}

	/// <summary>
	/// Defines a compiled material.
	/// </summary>
	public class CompiledMaterial : IEnumerable<CompiledMaterialPass>, IDisposable
	{
		List<CompiledMaterialPass> passes = new List<CompiledMaterialPass>();
		ICompiledMaterial material;

		/// <summary>
		/// Gets the definition that this compiled material represents.
		/// </summary>
		public MaterialDefinition Definition
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the pass at the provided index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The pass.</returns>
		public CompiledMaterialPass this[int index]
		{
			get { return passes[index]; }
		}

		/// <summary>
		/// Gets the total amount of passes.
		/// </summary>
		public int Count
		{
			get { return passes.Count; }
		}

		/// <summary>
		/// Creates a compiled material.
		/// </summary>
		/// <param name="renderer">The renderer.</param>
		/// <param name="definition">The material definition.</param>
		public CompiledMaterial(Renderer renderer, MaterialDefinition definition)
		{
			material = renderer.CompileMaterial(definition);
			
			// Store a copy of the definition.
			Definition = definition;

			// Create cached copies of CompiledMaterialPass.
			passes.AddRange(material.Select(p => new CompiledMaterialPass(p)));
		}

		/// <summary>
		/// Prepares the material for use. Call this before drawing.
		/// </summary>
		public void Use()
		{
			material.Use();
		}

		/// <summary>
		/// Implementation of IEnumerable.
		/// </summary>
		public IEnumerator<CompiledMaterialPass> GetEnumerator()
		{
			return passes.GetEnumerator();
		}

		/// <summary>
		/// Implementation of IEnumerable.
		/// </summary>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return passes.GetEnumerator();
		}

		/// <summary>
		/// Implementation of IDisposable.
		/// </summary>
		public void Dispose()
		{
			material.Dispose();
		}
	}
}
