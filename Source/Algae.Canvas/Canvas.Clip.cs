using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	public partial class Canvas
	{
		abstract class ClipDrawable : Drawable, IWalkable
		{
			public GroupDrawable Group;

			public void Walk(CanvasSceneWalker walker)
			{
				GlobalOrder = walker.AddItem();
				GroupOrder = walker.GetGroup();
				LayerOrder = walker.GetLayer();
			}

			public void CalculateDepths(Canvas canvas)
			{
				Depth = canvas.walker.GetDepth(GlobalOrder);
			}

			public abstract void Begin(Canvas canvas);

			public abstract void End(Canvas canvas);
		}

		class BeginClipDrawable : ClipDrawable
		{
			public override void Begin(Canvas canvas)
			{
				canvas.renderer.SetDepthMask(false);
				canvas.renderer.SetColorMask(false, false, false, false);
				canvas.renderer.SetStencilOperation(StencilFunction.Keep, StencilFunction.Increment, StencilFunction.Increment);
				canvas.renderer.SetStencilFunction(BufferFunction.Equal, canvas.clipping, 0xff);
				++canvas.clipping;
			}

			public override void End(Canvas canvas)
			{
				canvas.renderer.SetDepthMask(true);
				canvas.renderer.SetColorMask(true, true, true, true);
				canvas.renderer.SetStencilOperation(StencilFunction.Keep, StencilFunction.Keep, StencilFunction.Keep);
				canvas.renderer.SetStencilFunction(BufferFunction.Equal, canvas.clipping, 0xff);
			}

			public override Drawable Clone()
			{
				return new BeginClipDrawable() { Group = Group };
			}
		}

		class EndClipDrawable : ClipDrawable
		{
			public override void Begin(Canvas canvas)
			{
				canvas.renderer.SetDepthMask(false);
				canvas.renderer.SetColorMask(false, false, false, false);
				canvas.renderer.SetStencilOperation(StencilFunction.Keep, StencilFunction.Decrement, StencilFunction.Decrement);
				canvas.renderer.SetStencilFunction(BufferFunction.Equal, canvas.clipping, 0xff);
				--canvas.clipping;
			}

			public override void End(Canvas canvas)
			{
				canvas.renderer.SetDepthMask(true);
				canvas.renderer.SetColorMask(true, true, true, true);
				canvas.renderer.SetStencilOperation(StencilFunction.Keep, StencilFunction.Keep, StencilFunction.Keep);
				canvas.renderer.SetStencilFunction(BufferFunction.Equal, canvas.clipping, 0xff);
			}

			public override Drawable Clone()
			{
				return new EndClipDrawable() { Group = Group };
			}
		}
	}
}
