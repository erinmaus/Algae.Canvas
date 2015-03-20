using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	public partial class Canvas
	{
		class GroupDrawable : Drawable, IWalkable, IEnumerable<Drawable>
		{
			List<Drawable> drawables = new List<Drawable>();

			public void Walk(CanvasSceneWalker walker)
			{
				walker.AddLayer();

				foreach (var drawable in drawables)
				{
					(drawable as IWalkable).Walk(walker);
				}

				walker.RemoveLayer();
			}

			public void Draw(Canvas canvas)
			{
				var translucent = Draw(canvas, Color.White, Matrix.Identity, (d, p) => d.HasAlpha || p.HasAlpha || IsTranslucent(p.GlobalColor));
				var opaque = Draw(canvas, Color.White, Matrix.Identity, (d, p) => !d.HasAlpha && !p.HasAlpha && !IsTranslucent(p.GlobalColor));

				canvas.translucentDrawables.AddRange(translucent);
				canvas.opaqueDrawables.AddRange(opaque);
			}

			IEnumerable<Drawable> Draw(Canvas canvas, Color currentColor, Matrix currentTransform, Func<Drawable, GroupDrawable, bool> selector)
			{
				currentColor = currentColor * Color;
				currentTransform = currentTransform * Transform;

				List<Drawable> selectedDrawables = new List<Drawable>();
				IEnumerable<Drawable> d = drawables;

				foreach (var drawable in drawables)
				{
					if (drawable is GroupDrawable)
					{
						var items = (drawable as GroupDrawable).Draw(canvas, currentColor, currentTransform, selector);

						selectedDrawables.AddRange(items);
					}
					else if (selector(drawable, this))
					{
						if (!(drawable is ClipDrawable))
						{
							drawable.Color = currentColor * drawable.Color;
							drawable.Transform = currentTransform * drawable.Transform;
						}

						selectedDrawables.Add(drawable);
					}
				}

				if (!HasAlpha)
					return selectedDrawables;
				else
					return Enumerable.Reverse(selectedDrawables);
			}

			void Draw(Canvas canvas, IEnumerable<Drawable> drawables, Color currentColor, Matrix currentTransform)
			{
				foreach (var drawable in drawables)
				{
					if (drawable is GroupDrawable)
					{
						(drawable as GroupDrawable).Draw(canvas, currentColor, currentTransform);
					}
					else
					{
						if (!(drawable is ClipDrawable))
						{
							drawable.Color = currentColor * drawable.Color;
							drawable.Transform = currentTransform * drawable.Transform;
						}

						if (drawable.HasAlpha)
							canvas.translucentDrawables.Add(drawable);
						else
							canvas.opaqueDrawables.Add(drawable);
					}
				}
			}

			void Draw(Canvas canvas, Color currentColor, Matrix currentTransform)
			{
				currentColor = currentColor * Color;
				currentTransform = currentTransform * Transform;

				List<Drawable> translucent = new List<Drawable>();
				List<Drawable> opaque = new List<Drawable>();

				foreach (var drawable in drawables)
				{
					if (drawable.HasAlpha)
					{
						if (drawable is ClipDrawable)
							translucent.Add(drawable);
						else
							translucent.Insert(0, drawable);
					}
					else
					{
						if (drawable is ClipDrawable)
							opaque.Add(drawable);
						else
							opaque.Insert(0, drawable);
					}
				}

				Draw(canvas, opaque, currentColor, currentTransform);
				Draw(canvas, translucent, currentColor, currentTransform);
			}

			public void CalculateDepths(Canvas canvas)
			{
				foreach (var drawable in drawables)
				{
					if (drawable is PathDrawable)
					{
						drawable.Depth = canvas.walker.GetDepth(drawable.GlobalOrder);

						PathDrawable path = drawable as PathDrawable;
						canvas.depths.Set(path.PathIndex, drawable.Depth);
						path.Foobar = canvas.foobars++;
					}
					else if (drawable is ClipDrawable)
					{
						(drawable as ClipDrawable).CalculateDepths(canvas);
					}
					else if (drawable is GroupDrawable)
					{
						(drawable as GroupDrawable).CalculateDepths(canvas);
					}
				}
			}

			public void AddDrawable(Drawable drawable)
			{
				if (drawable is PathDrawable)
				{
					drawable.GlobalTransform = GlobalTransform * Transform * drawable.Transform;
					drawable.GlobalColor = GlobalColor * Color * drawable.Color;
				}

				drawables.Add(drawable);
			}

			public override Drawable Clone()
			{
				GroupDrawable g = new GroupDrawable() { Transform = Transform, GlobalTransform = GlobalTransform };

				foreach (var drawable in drawables)
				{
					if (!(drawable is PathDrawable))
						g.AddDrawable(drawable.Clone());
					else
						g.AddDrawable(drawable);
				}

				return g;
			}

			public IEnumerator<Drawable> GetEnumerator()
			{
				return drawables.GetEnumerator();
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}
	}
}
