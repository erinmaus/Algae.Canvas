using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	public partial class Canvas
	{
		abstract class Drawable
		{
			public Color Color = Color.White;
			public Color GlobalColor = Color.White;
			public Matrix Transform = Matrix.Identity;
			public Matrix GlobalTransform = Matrix.Identity;

			public int GlobalOrder;
			public int GroupOrder;
			public int LayerOrder;
			public float Depth;

			public int Foobar;

			public virtual bool HasAlpha
			{
				get { return IsTranslucent(Color); }
			}

			public static bool IsTranslucent(Color color)
			{
				return (int)(color.Alpha * 255) < 255;
			}

			public abstract Drawable Clone();
		}

	}
}
