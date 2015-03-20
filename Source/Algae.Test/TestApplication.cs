using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

using CommaExcess.Algae.Graphics;
using CommaExcess.Algae.Graphics.Lvg;
using CommaExcess.Algae.Platform;

namespace CommaExcess.Algae.Test
{
	class SimpleCanvasMaterial : ICanvasMaterial
	{
		CompiledMaterial material;

		public SimpleCanvasMaterial(CompiledMaterial material)
		{
			this.material = material;
		}

		public void Use()
		{
			material.Use();
			material[0].Begin();
		}

		public void Prepare(Mesh mesh)
		{
			mesh.MapElements(material.Definition);
		}

		public void SetProjection(Matrix matrix)
		{
			material[0].SetValue("algae_Projection", matrix);
		}

		public void SetColorTexture(Texture2D texture)
		{
			material[0].SetValue("algae_ColorTexture", texture);
		}

		public void SetDepthTexture(Texture2D texture)
		{
			material[0].SetValue("algae_DepthTexture", texture);
		}

		public void SetIndexRowLength(int length)
		{
			material[0].SetValue("algae_IndexRowLength", length);
		}
	}

	class TestApplication : Application
	{
		Canvas canvas;
		SimpleCanvasMaterial material;

		Font font;
		BufferedText text;
		SimpleBufferedTextBuilder textBuilder;

		LvgImage image;

		public TestApplication(string filename)
		{
			try
			{
				using (Stream stream = File.OpenRead(filename))
				{
					image = LvgImage.Load(stream);
				}
			}
			finally
			{
				// Nothing.
			}

			Display.Settings = new DisplaySettings(1280, 720, false, 16);
		}

		public override void LoadContent()
		{
			base.LoadContent();

			using (Stream stream = File.OpenRead("content/material.xml"))
			{
				material = new SimpleCanvasMaterial(new CompiledMaterial(Renderer, MaterialDefinition.Load(stream, Renderer.Name, Renderer.Tag)));
			}

			font = new Font();
			using (Stream stream = File.OpenRead("content/font.ttf"))
			{
				font.Load(stream, 64);
			}

			text = new BufferedText(font);
			textBuilder = new SimpleBufferedTextBuilder();

			if (image == null)
			{
				textBuilder.Prepare(text);
				textBuilder.Color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
				textBuilder.BufferText(text, "Could not load image.");
			}

			canvas = new Canvas(Renderer, material, 2);
		}

		public override void UnloadContent()
		{
			canvas.Dispose();

			base.UnloadContent();
		}

		Stopwatch stopwatch = new Stopwatch();
		float elapsedFrameTime, elapsedDrawTime;
		float maxDrawTime = Single.MinValue, minDrawTime = Single.MaxValue;
		float averageDrawTime;
		bool hasDrawn, hasAverage;

		public override void Update()
		{
			elapsedFrameTime += UpdateInterval;

			if (elapsedFrameTime > 1.0f)
			{
				elapsedFrameTime -= 1.0f;
				averageDrawTime = elapsedDrawTime * UpdateInterval;
				elapsedDrawTime = 0.0f;
				hasAverage = true;
			}

			else if (hasDrawn && image != null)
			{
				textBuilder.Reset();
				textBuilder.Prepare(text);
				textBuilder.FontSize = 16.0f;

				textBuilder.Color = Color.White;
				textBuilder.BufferText(text, "left click to move, right click to zoom");

				textBuilder.NextLine(0.0f);
				textBuilder.BufferText(text, String.Format("max draw time = {0} ms, min draw time = {1} ms", maxDrawTime, minDrawTime));

				if (hasAverage)
				{
					textBuilder.NextLine(0.0f);
					textBuilder.BufferText(text, String.Format("average draw time = {0} ms", averageDrawTime));
				}
			}

			base.Update();
		}

		enum MovementMode
		{
			None,
			Pan,
			Zoom
		}

		MovementMode currentMovementMode;
		Vector2 pan;
		float zoom = 1.0f;

		public override void OnMouseButtonDown(MouseEventArgs e)
		{
			if (currentMovementMode == MovementMode.None)
			{
				if (e.Button == 1)
					currentMovementMode = MovementMode.Pan;
				else if (e.Button == 2)
					currentMovementMode = MovementMode.Zoom;
			}

			base.OnMouseButtonDown(e);
		}

		public override void OnMouseButtonUp(MouseEventArgs e)
		{
			if ((currentMovementMode == MovementMode.Pan && e.Button == 1) ||
				(currentMovementMode == MovementMode.Zoom && e.Button == 2))
			{
				currentMovementMode = MovementMode.None;
			}

			base.OnMouseButtonUp(e);
		}

		public override void OnMouseMove(MouseEventArgs e)
		{
			if (currentMovementMode == MovementMode.Pan)
			{
				pan += new Vector2(e.Difference.X, e.Difference.Y) * (1.0f / zoom);
			}
			else if (currentMovementMode == MovementMode.Zoom)
			{
				zoom = MathHelper.Clamp(zoom - e.Difference.Y / 32.0f, 0.25f, 30.0f);
			}

			base.OnMouseMove(e);
		}

		public override void OnClose(EventArgs e)
		{
			Exit();

			base.OnClose(e);
		}

		void DrawImage()
		{
			if (image != null)
			{
				Vector3 halfScreen = new Vector3(640.0f, 360.0f, 0.0f);
				Matrix scale = Matrix.Scale(new Vector3(zoom, zoom, 1.0f));
				Matrix translation = Matrix.Translation(new Vector3(pan.X, pan.Y, 0.0f));

				canvas.StartGroup(Color.White, Matrix.Translation(halfScreen) * scale * translation * Matrix.Translation(-halfScreen));
				canvas.StartClip(Matrix.Identity, new BoundingRectangle(Vector2.Zero, new Vector2(1280, 720)));
				canvas.UseClip();

				image.Draw(canvas);

				canvas.FinishClip();
				canvas.FinishGroup();
			}
		}

		void DrawStats()
		{
			text.Draw(canvas);
		}

		public override void Draw()
		{
			Renderer.ApplyView(new View()
			{
				CullEnabled = false,
				DepthEnabled = true,
				StencilEnabled = true,
				Viewport = new Viewport(0, 0, Display.Width, Display.Height),
				RenderTarget = null
			});

			Renderer.Clear(Color.Black);
			Renderer.ClearDepth();
			Renderer.ClearStencil();

			stopwatch.Reset();
			stopwatch.Start();

			canvas.Prepare(Matrix.Orthographic(0, Display.Width, 0, Display.Height, 0.0f, 1.0f));
			DrawImage();
			DrawStats();
			canvas.Finish();

			Renderer.Finish();
			stopwatch.Stop();

			hasDrawn = true;
			maxDrawTime = Math.Max(stopwatch.ElapsedMilliseconds, maxDrawTime);
			minDrawTime = Math.Min(stopwatch.ElapsedMilliseconds, minDrawTime);
			elapsedDrawTime += stopwatch.ElapsedMilliseconds;

			base.Draw();
		}
	}
}
