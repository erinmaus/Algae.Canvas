using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CommaExcess.Algae.Graphics.Lvg
{
	public abstract class LvgDrawable : IDisposable
	{
		public Color Fill
		{
			get;
			protected set;
		}

		public Matrix LocalTransform
		{
			get;
			protected set;
		}

		public Matrix GlobalTransform
		{
			get;
			protected set;
		}

		public BoundingRectangle Bounds
		{
			get;
			protected set;
		}

		protected LvgDrawable(XElement element)
		{
			var fill = element.Attribute("fill");

			if (fill == null)
				Fill = Color.White;
			else
				Fill = ParseColor(fill.Value);

			var transform = element.Attribute("transform");

			if (transform == null)
				LocalTransform = Matrix.Identity;
			else
				LocalTransform = ParseMatrix(transform.Value);

			var bounds = element.Attribute("bounds");

			if (bounds == null)
				Bounds = BoundingRectangle.Empty;
			else
				Bounds = ParseBounds(bounds.Value);

			GlobalTransform = Matrix.Identity;
		}

		public void Draw(Canvas canvas, Matrix transform, Color color)
		{
			canvas.StartGroup(color, transform);

			Draw(canvas);

			canvas.FinishGroup();
		}

		public abstract void Draw(Canvas canvas);

		protected static BoundingRectangle ParseBounds(string value)
		{
			string[] v = value.Split(' ');

			return new BoundingRectangle
			(
				new Vector2(Single.Parse(v[0], System.Globalization.CultureInfo.InvariantCulture), Single.Parse(v[1], System.Globalization.CultureInfo.InvariantCulture)),
				new Vector2(Single.Parse(v[2], System.Globalization.CultureInfo.InvariantCulture), Single.Parse(v[3], System.Globalization.CultureInfo.InvariantCulture))
			);
		}

		protected static Color ParseColor(string value)
		{
			string[] v = value.Split(' ');

			return new Color(Color.ParseCss(v[0]), Single.Parse(v[1], System.Globalization.CultureInfo.InvariantCulture));
		}

		protected static Matrix ParseMatrix(string value)
		{
			string[] v = value.Split(' ');
			float[] m = new float[6];

			for (int i = 0; i < 6; i++)
			{
				m[i] = Single.Parse(v[i], System.Globalization.CultureInfo.InvariantCulture);
			}

			return new Matrix
			(
				m[0], m[2], 0.0f, m[4],
				m[1], m[3], 0.0f, m[5],
				0.0f, 0.0f, 1.0f, 0.0f,
				0.0f, 0.0f, 0.0f, 1.0f
			);
		}

		public abstract void Dispose();
	}
}
