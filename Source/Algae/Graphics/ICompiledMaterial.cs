using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// Defines a compiled material pass.
	/// </summary>
	interface ICompiledMaterialPass
	{
		/// <summary>
		/// Begins rendering the material pass.
		/// </summary>
		void Begin();

		/// <summary>
		/// Applies any changes.
		/// </summary>
		void Apply();

		/// <summary>
		/// Sets a parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="value">The value.</param>
		void SetValue(string parameter, float value);

		/// <summary>
		/// Sets a parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="value">The value.</param>
		void SetValue(string parameter, int value);

		/// <summary>
		/// Sets a parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="value">The value.</param>
		void SetValue(string parameter, Vector2 value);

		/// <summary>
		/// Sets a parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="value">The value.</param>
		void SetValue(string parameter, Vector3 value);

		/// <summary>
		/// Sets a parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="value">The value.</param>
		void SetValue(string parameter, Vector4 value);

		/// <summary>
		/// Sets a parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="value">The value.</param>
		void SetValue(string parameter, Matrix value);

		/// <summary>
		/// Seta a parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="texture">The value.</param>
		void SetValue(string parameter, Texture texture);

		/// <summary>
		/// Ends the material pass.
		/// </summary>
		void End();
	}

	/// <summary>
	/// Defines a compiled material.
	/// </summary>
	interface ICompiledMaterial : IEnumerable<ICompiledMaterialPass>, IDisposable
	{
		/// <summary>
		/// Gets the pass at the provided index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The pass.</returns>
		ICompiledMaterialPass this[int index]
		{
			get;
		}

		/// <summary>
		/// Gets the total amount of passes.
		/// </summary>
		int Count
		{
			get;
		}

		/// <summary>
		/// Uses the material. Call this before drawing a pass.
		/// </summary>
		void Use();
	}
}
