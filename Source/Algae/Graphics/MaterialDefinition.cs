using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// Defines a state change.
	/// </summary>
	public class MaterialState
	{
		/// <summary>
		/// Gets the block this state belongs to (e.g., stencil or depth).
		/// </summary>
		public string Block
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the name of the state change (e.g., func or op).
		/// </summary>
		public string Name
		{
			get;
			private set;
		}

		Dictionary<string, string> arguments = new Dictionary<string, string>();

		/// <summary>
		/// Gets the dictionary of arguments.
		/// </summary>
		public Dictionary<string, string> Arguments
		{
			get { return arguments; }
		}

		/// <summary>
		/// Constructs a state change.
		/// </summary>
		/// <param name="block">The block.</param>
		/// <param name="name">The name.</param>
		public MaterialState(string block, string name)
		{
			Block = block;
			Name = name;
		}
	}

	/// <summary>
	/// Defines a material parameter.
	/// </summary>
	public class MaterialParameter
	{
		/// <summary>
		/// Gets the name. This generally is an identifier used in the shader.
		/// </summary>
		public string Name
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the type (e.g., Vector4).
		/// </summary>
		public string Type
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the value or argument.
		/// </summary>
		public string Value
		{
			get;
			set;
		}

		/// <summary>
		/// Constructs a material parameter.
		/// </summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="type">The type of the parameter.</param>
		/// <param name="value">The value of the parameter.</param>
		public MaterialParameter(string name, string type, string value = null)
		{
			Name = name;
			Type = type;
			Value = value;
		}
	}

	/// <summary>
	/// A pass, used to render an object.
	/// </summary>
	public class MaterialPass
	{
		List<MaterialState> state = new List<MaterialState>();

		/// <summary>
		/// Gets the state changes.
		/// </summary>
		public List<MaterialState> State
		{
			get { return state; }
		}

		List<MaterialParameter> parameters = new List<MaterialParameter>();

		/// <summary>
		/// Gets the parameters.
		/// </summary>
		public List<MaterialParameter> Parameters
		{
			get { return parameters; }
		}
	}

	/// <summary>
	/// A vertex element used in a vertex shader (or similar).
	/// </summary>
	public struct MaterialVertexElement
	{
		/// <summary>
		/// The name, used in a shader (or similar), of this material vertex element.
		/// </summary>
		public string Name;

		/// <summary>
		/// The context of the element. This members determines how a mesh is adapted
		/// to use the vertex element.
		/// </summary>
		public string Context;

		/// <summary>
		/// The preferred index of the vertex element.
		/// </summary>
		public int Index;
	}

	/// <summary>
	/// A material fragment output, used by a fragment shader (or similar).
	/// </summary>
	public struct MaterialFragmentOutput
	{
		/// <summary>
		/// The name of the output.
		/// </summary>
		public string Name;

		/// <summary>
		/// The index of the output.
		/// </summary>
		public int Index;
	}

	/// <summary>
	/// Defines a material.
	/// </summary>
	public class MaterialDefinition : IEnumerable<MaterialPass>
	{
		List<MaterialVertexElement> vertexElements = new List<MaterialVertexElement>();

		/// <summary>
		/// Gets a list of vertex elements.
		/// </summary>
		public List<MaterialVertexElement> VertexElements
		{
			get { return vertexElements; }
		}

		List<MaterialFragmentOutput> fragmentOutputs = new List<MaterialFragmentOutput>();

		/// <summary>
		/// Gets a list of fragment outputs.
		/// </summary>
		public List<MaterialFragmentOutput> FragmentOutputs
		{
			get { return fragmentOutputs; }
		}

		Dictionary<string, string> shaders = new Dictionary<string, string>();

		/// <summary>
		/// Gets the dictionary of shaders.
		/// </summary>
		public Dictionary<string, string> Shaders
		{
			get { return shaders; }
		}

		// Internal list of passes.
		List<MaterialPass> passes = new List<MaterialPass>();

		/// <summary>
		/// Gets the total amount of material passes.
		/// </summary>
		public int Count
		{
			get { return passes.Count; }
		}

		/// <summary>
		/// Gets the material pass at the provided index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The material pass at the provided index.</returns>
		public MaterialPass this[int index]
		{
			get { return passes[index]; }
		}

		/// <summary>
		/// Constructs a material definition.
		/// </summary>
		public MaterialDefinition()
		{
			// Nothing.
		}

		/// <summary>
		/// Adds and returns a material pass.
		/// </summary>
		/// <returns>The material pass.</returns>
		public MaterialPass AddPass()
		{
			MaterialPass pass = new MaterialPass();

			passes.Add(pass);

			return pass;
		}

		/// <summary>
		/// Removes the provided material pass.
		/// </summary>
		/// <param name="pass">The pass to remove.</param>
		public void RemovePass(MaterialPass pass)
		{
			passes.Remove(pass);
		}

		/// <summary>
		/// Implementation of IEnumerable.
		/// </summary>
		public IEnumerator<MaterialPass> GetEnumerator()
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
		/// Loads a material from a stream.
		/// </summary>
		/// <param name="stream">The stream to load the material from.</param>
		/// <param name="renderer">The renderer's name.</param>
		/// <param name="tag">The renderer's tag.</param>
		/// <returns>The material definition.</returns>
		public static MaterialDefinition Load(Stream stream, string renderer, string tag)
		{
			MaterialDefinition definition = new MaterialDefinition();
			XDocument document = XDocument.Load(stream);

			// Get the target that matches the renderer-tag pair.
			var target = document
				.Element("targets")
				.Elements("target")
				.Where(t => t.Attribute("renderer") != null && t.Attribute("renderer").Value == renderer)
				.Where(t => t.Attribute("tag") != null && t.Attribute("tag").Value == tag)
				.FirstOrDefault();

			// Build a list of vertex elements.
			var vertexElements = target
				.Element("vertex-elements")
				.Elements("vertex-element");

			foreach (var vertexElement in vertexElements)
			{
				definition.VertexElements.Add(new MaterialVertexElement()
				{
					Name = vertexElement.Attribute("name").Value,
					Context = vertexElement.Attribute("context").Value,
					Index = Int32.Parse(vertexElement.Attribute("index").Value, System.Globalization.CultureInfo.InvariantCulture)
				});
			}

			// Build a list of fragment outputs.
			var fragmentOutputs = target
				.Element("fragment-outputs")
				.Elements("fragment-output");

			foreach (var fragmentOutput in fragmentOutputs)
			{
				definition.FragmentOutputs.Add(new MaterialFragmentOutput()
				{
					Name = fragmentOutput.Attribute("name").Value,
					Index = Int32.Parse(fragmentOutput.Attribute("index").Value, System.Globalization.CultureInfo.InvariantCulture)
				});
			}

			// Collect the shaders.
			var shaders = target
				.Element("shaders")
				.Elements("shader");

			foreach (var shader in shaders)
			{
				definition.Shaders.Add(shader.Attribute("name").Value, shader.Value);
			}

			// Build the passes.
			var passes = target
				.Element("passes")
				.Elements("pass");

			foreach (var pass in passes)
			{
				MaterialPass p = definition.AddPass();

				// Build the state changes for the pass.
				var states = pass
					.Element("state")
					.Elements("change");

				foreach (var state in states)
				{
					MaterialState s = new MaterialState(state.Attribute("block").Value, state.Attribute("name").Value);

					// Iterate over all the attributes except for 'block' and 'name'.
					var arguments = state.Attributes();

					foreach (var argument in arguments)
					{
						// Ignore 'block' and 'name'.
						if (argument.Name.LocalName != "block" && argument.Name.LocalName != "name")
							s.Arguments.Add(argument.Name.LocalName, argument.Value);
					}

					p.State.Add(s);
				}

				// Build the parameters for the pass.
				var parameters = pass
					.Element("params")
					.Elements("param");

				foreach (var parameter in parameters)
				{
					MaterialParameter param = new MaterialParameter(parameter.Attribute("name").Value, parameter.Attribute("type").Value);

					// Only add a value if the parameter actually has one.
					if (parameter.Attribute("value") != null)
						param.Value = parameter.Attribute("value").Value;

					p.Parameters.Add(param);
				}
			}

			return definition;
		}
	}
}
