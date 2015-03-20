using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Graphics.OpenGL;

namespace CommaExcess.Algae.Graphics
{
	/// <summary>
	/// Defines a state change.
	/// </summary>
	abstract class GL3StateChange
	{
		/// <summary>
		/// Initializes the state change.
		/// </summary>
		/// <param name="state">The material definition.</param>
		public abstract void Initialize(MaterialState state);

		/// <summary>
		/// Applies a state change.
		/// </summary>
		public abstract void Apply();
	}

	/// <summary>
	/// glBlendColor
	/// </summary>
	class GL3BlendColorState : GL3StateChange
	{
		Color color;

		public override void Initialize(MaterialState state)
		{
			color.Red = Single.Parse(state.Arguments["red"], System.Globalization.CultureInfo.InvariantCulture);
			color.Green = Single.Parse(state.Arguments["green"], System.Globalization.CultureInfo.InvariantCulture);
			color.Blue = Single.Parse(state.Arguments["blue"], System.Globalization.CultureInfo.InvariantCulture);
			color.Alpha = Single.Parse(state.Arguments["alpha"], System.Globalization.CultureInfo.InvariantCulture);
		}

		public override void Apply()
		{
			GL.BlendColor(color.Red, color.Green, color.Blue, color.Alpha);
		}
	}

	/// <summary>
	/// glBlendEquation
	/// </summary>
	class GL3BlendEquation : GL3StateChange
	{
		BlendEquationMode mode;

		public override void Initialize(MaterialState state)
		{
			string value = state.Arguments["value"];

			if (!Enum.TryParse(value, out mode))
				mode = BlendEquationMode.FuncAdd;
		}

		public override void Apply()
		{
			GL.BlendEquation(mode);
		}
	}

	/// <summary>
	/// glBlendFunc
	/// </summary>
	class GL3BlendFuncState : GL3StateChange
	{
		BlendingFactorSrc source = BlendingFactorSrc.One;
		BlendingFactorDest destination = BlendingFactorDest.Zero;

		public override void Initialize(MaterialState state)
		{
			string s = state.Arguments["src"];
			string d = state.Arguments["dst"];

			if (!Enum.TryParse(s, out source))
				source = BlendingFactorSrc.One;

			if (!Enum.TryParse(d, out destination))
				destination = BlendingFactorDest.Zero;
		}

		public override void Apply()
		{
			GL.BlendFunc(source, destination);
		}
	}

	/// <summary>
	/// glColorMask
	/// </summary>
	class GL3ColorMaskState : GL3StateChange
	{
		bool red, green, blue, alpha;

		public override void Initialize(MaterialState state)
		{
			if (!System.Boolean.TryParse(state.Arguments["red"], out red))
				red = true;

			if (!System.Boolean.TryParse(state.Arguments["green"], out green))
				green = true;

			if (!System.Boolean.TryParse(state.Arguments["blue"], out blue))
				blue = true;

			if (!System.Boolean.TryParse(state.Arguments["red"], out red))
				alpha = true;
		}

		public override void Apply()
		{
			GL.ColorMask(red, green, blue, alpha);
		}
	}

	/// <summary>
	/// glCullFace
	/// </summary>
	class GL3CullFaceState : GL3StateChange
	{
		CullFaceMode mode;

		public override void Initialize(MaterialState state)
		{
			string value = state.Arguments["value"];

			if (!Enum.TryParse(value, out mode))
				mode = CullFaceMode.Back;
		}

		public override void Apply()
		{
			GL.CullFace(mode);
		}
	}

	/// <summary>
	/// glDepthFunc
	/// </summary>
	class GL3DepthFuncState : GL3StateChange
	{
		DepthFunction mode;

		public override void Initialize(MaterialState state)
		{
			string value = state.Arguments["value"];

			if (!Enum.TryParse(value, out mode))
				mode = DepthFunction.Less;
		}

		public override void Apply()
		{
			GL.DepthFunc(mode);
		}
	}

	/// <summary>
	/// glDepthMask
	/// </summary>
	class GL3DepthMaskState : GL3StateChange
	{
		bool mode;

		public override void Initialize(MaterialState state)
		{
			if (!System.Boolean.TryParse(state.Arguments["value"], out mode))
				mode = true;
		}

		public override void Apply()
		{
			GL.DepthMask(mode);
		}
	}

	/// <summary>
	/// glDepthRange
	/// </summary>
	class GL3DepthRangeState : GL3StateChange
	{
		float near, far;

		public override void Initialize(MaterialState state)
		{
			near = Single.Parse(state.Arguments["near"], System.Globalization.CultureInfo.InvariantCulture);
			far = Single.Parse(state.Arguments["far"], System.Globalization.CultureInfo.InvariantCulture);
		}

		public override void Apply()
		{
			GL.DepthRange(near, far);
		}
	}

	/// <summary>
	/// glStencilFunc
	/// </summary>
	class GL3StencilFuncState : GL3StateChange
	{
		OpenTK.Graphics.OpenGL.StencilFunction mode;
		int r; // ref
		uint mask;

		public override void Initialize(MaterialState state)
		{
			string value = state.Arguments["value"];

			if (!Enum.TryParse(value, out mode))
				mode = OpenTK.Graphics.OpenGL.StencilFunction.Always;

			r = Int32.Parse(state.Arguments["ref"], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
			mask = UInt32.Parse(state.Arguments["mask"], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
		}

		public override void Apply()
		{
			GL.StencilFunc(mode, r, mask);
		}
	}

	/// <summary>
	/// glStencilMask
	/// </summary>
	class GL3StencilMaskState : GL3StateChange
	{
		uint mask;

		public override void Initialize(MaterialState state)
		{
			mask = UInt32.Parse(state.Arguments["value"], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
		}

		public override void Apply()
		{
			GL.StencilMask(mask);
		}
	}

	/// <summary>
	/// glStencilOp
	/// </summary>
	class GL3StencilOpState : GL3StateChange
	{
		StencilOp sfail, dfail, dpass;

		public override void Initialize(MaterialState state)
		{
			if (!Enum.TryParse(state.Arguments["sfail"], out sfail))
				sfail = StencilOp.Keep;

			if (!Enum.TryParse(state.Arguments["dfail"], out dfail))
				dfail = StencilOp.Keep;

			if (!Enum.TryParse(state.Arguments["dpass"], out dpass))
				dpass = StencilOp.Keep;
		}

		public override void Apply()
		{
			GL.StencilOp(sfail, dfail, dpass);
		}
	}

	/// <summary>
	/// An OpenGL 3 implementation of a compiled material pass.
	/// </summary>
	class GL3CompiledMaterialPass : ICompiledMaterialPass, IDisposable
	{
		int program;

		List<GL3StateChange> state = new List<GL3StateChange>();

		// Also known as uniforms.
		Dictionary<string, int> values = new Dictionary<string, int>();

		// List of textures to samplers.
		Dictionary<string, int> samplers = new Dictionary<string, int>();

		void BuildStates(MaterialPass pass)
		{
			// TODO: Make this nicer.
			// Ignore invalid state changes as well.
			foreach (MaterialState change in pass.State)
			{
				GL3StateChange s = null;

				if (change.Block == "blend")
				{
					if (change.Name == "color")
					{
						s = new GL3BlendColorState();
					}
					else if (change.Name == "equation")
					{
						s = new GL3BlendEquation();
					}
					else if (change.Name == "func")
					{
						s = new GL3BlendFuncState();
					}
				}
				else if (change.Block == "color")
				{
					if (change.Name == "mask")
					{
						s = new GL3ColorMaskState();
					}
				}
				else if (change.Block == "cull")
				{
					if (change.Name == "face")
					{
						s = new GL3CullFaceState();
					}
				}
				else if (change.Block == "depth")
				{
					if (change.Name == "func")
					{
						s = new GL3DepthFuncState();
					}
					else if (change.Name == "mask")
					{
						s = new GL3DepthMaskState();
					}
					else if (change.Name == "range")
					{
						s = new GL3DepthRangeState();
					}
				}
				else if (change.Block == "stencil")
				{
					if (change.Name == "func")
					{
						s = new GL3StencilFuncState();
					}
					else if (change.Name == "mask")
					{
						s = new GL3StencilMaskState();
					}
					else if (change.Name == "op")
					{
						s = new GL3StencilMaskState();
					}
				}

				// Only add a valid state change.
				if (s != null)
				{
					// Invalid parameters are set to default values.
					s.Initialize(change);

					state.Add(s);
				}
			}
		}

		public GL3CompiledMaterialPass(MaterialDefinition definition, MaterialPass pass, int program)
		{
			// Store program.
			this.program = program;

			// Unique index that represents a texture unit, or sampler.
			// Skip the first because that's reserved for temporary texture operations,
			// such as setting parameters, etc.
			int index = 1;

			// Fetch uniforms.
			// Keep in mind only shaders use the "value" attribute.
			// A compiled material does not store default values.
			foreach (MaterialParameter parameter in pass.Parameters)
			{
				// Shader types are ignored, since they're operated on prior.
				if (parameter.Type == "shader")
					continue;

				int uniform = GL.GetUniformLocation(program, parameter.Name);
				
				// Don't add invalid uniforms.
				// Instead, silently ignore them.
				if (uniform > -1)
				{
					values.Add(parameter.Name, uniform);

					// If it's a texture, add it to the samplers.
					if (parameter.Type == "texture")
					{
						samplers.Add(parameter.Name, index++);
					}
				}
			}

			// Lastly, build states.
			BuildStates(pass);
		}

		public void Begin()
		{
			// Iterate through the states, applying them in order.
			foreach (GL3StateChange change in state)
			{
				change.Apply();
			}
		}

		public void Apply()
		{
			// Nothing.
		}

		public void SetValue(string parameter, float value)
		{
			if (values.ContainsKey(parameter))
				GL.Uniform1(values[parameter], value);
		}

		public void SetValue(string parameter, int value)
		{
			if (values.ContainsKey(parameter))
				GL.Uniform1(values[parameter], value);
		}

		public void SetValue(string parameter, Vector2 value)
		{
			if (values.ContainsKey(parameter))
				GL.Uniform2(values[parameter], value.X, value.Y);
		}

		public void SetValue(string parameter, Vector3 value)
		{
			if (values.ContainsKey(parameter))
				GL.Uniform3(values[parameter], value.X, value.Y, value.Z);
		}

		public void SetValue(string parameter, Vector4 value)
		{
			if (values.ContainsKey(parameter))
				GL.Uniform4(values[parameter], value.X, value.Y, value.Z, value.W);
		}

		public void SetValue(string parameter, Matrix value)
		{
			if (values.ContainsKey(parameter))
				GL.UniformMatrix4(values[parameter], 1, true, ref value.M11);
		}

		public void SetValue(string parameter, Texture texture)
		{
			if (values.ContainsKey(parameter) && samplers.ContainsKey(parameter))
			{
				// Bind the texture to the appropriate sampler.
				texture.Bind(samplers[parameter]);
				
				// Set it, of course.
				GL.Uniform1(values[parameter], samplers[parameter]);
			}
		}

		public void End()
		{
			// Nothing...
			// Optimizations!
		}

		public void Dispose()
		{
			// Nothing.
		}
	}

	/// <summary>
	/// An OpenGL 3 implementation of a compiled material.
	/// </summary>
	class GL3CompiledMaterial : ICompiledMaterial
	{
		List<GL3CompiledMaterialPass> passes = new List<GL3CompiledMaterialPass>();

		GL3Renderer renderer;

		public ICompiledMaterialPass this[int index]
		{
			get { return passes[index]; }
		}

		public int Count
		{
			get { return passes.Count; }
		}

		int vertexShader, fragmentShader, program;

		int BuildShader(string source, ShaderType type)
		{
			// Create shader.
			int shader = GL.CreateShader(type);

			GL.ShaderSource(shader, source);
			GL.CompileShader(shader);

			int status;
			GL.GetShader(shader, ShaderParameter.CompileStatus, out status);	

			// Throw an exception if the shader couldn't be compiled.
			if (status == 0)
			{
				string log = GL.GetShaderInfoLog(shader);
#if DEBUG
				System.Diagnostics.Trace.WriteLine(source);
#endif
				throw new GraphicsException(log, "glCompileShader");
			}

			return shader;
		}
		
		void BuildProgram(string vertexShaderSource, string fragmentShaderSource, MaterialDefinition definition)
		{
			// Build the shaders. Currently, only vertex and fragment shaders are supported.
			vertexShader = BuildShader(vertexShaderSource, ShaderType.VertexShader);
			fragmentShader = BuildShader(fragmentShaderSource, ShaderType.FragmentShader);

			// Build the program.
			program = GL.CreateProgram();

			// Attach the shaders.
			GL.AttachShader(program, vertexShader);
			GL.AttachShader(program, fragmentShader);

			// Bind attributes.
			foreach (MaterialVertexElement element in definition.VertexElements)
			{
				GL.BindAttribLocation(program, element.Index, element.Name);
			}

			// Bind fragment outputs.
			foreach (MaterialFragmentOutput output in definition.FragmentOutputs)
			{
				GL.BindFragDataLocation(program, output.Index, output.Name);
			}

			// Link and verify program.
			GL.LinkProgram(program);

			int status;
			GL.GetProgram(program, ProgramParameter.LinkStatus, out status);

			// Throw an exception if the program couldn't be linked.
			if (status == 0)
			{
				throw new GraphicsException(GL.GetProgramInfoLog(program), "glLinkProgram");
			}
		}

		public GL3CompiledMaterial(GL3Renderer renderer, MaterialDefinition definition)
		{
			this.renderer = renderer;

			BuildProgram(definition.Shaders["Vertex"], definition.Shaders["Fragment"], definition);
			
			// Simple.
			foreach (MaterialPass pass in definition)
			{
				passes.Add(new GL3CompiledMaterialPass(definition, pass, program));
			}
		}

		public void Use()
		{
			// Small optimization: don't bind the program if it was the last.
			if (renderer.CurrentMaterial != this)
				GL.UseProgram(program);
		}

		public void Dispose()
		{
			GL.DetachShader(program, vertexShader);
			GL.DeleteShader(vertexShader);

			GL.DetachShader(program, fragmentShader);
			GL.DeleteShader(fragmentShader);

			GL.DeleteProgram(program);

			// Iterate over passes and destroy.
			foreach (GL3CompiledMaterialPass pass in passes)
			{
				pass.Dispose();
			}
		}

		public IEnumerator<ICompiledMaterialPass> GetEnumerator()
		{
			return passes.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return passes.GetEnumerator();
		}
	}
}
