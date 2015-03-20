using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	public partial class Canvas
	{
		class DrawAction
		{
			int start, count;
			ClipDrawable clip;

			public DrawAction(int indexStart, int indexCount, ClipDrawable clip = null)
			{
				start = indexStart;
				count = indexCount;
				this.clip = clip;
			}

			public void Draw(Canvas canvas)
			{
				if (clip != null)
					clip.Begin(canvas);

				canvas.mesh.Render(MeshRenderMode.Triangles, count, start);

				if (clip != null)
					clip.End(canvas);
			}
		}
	}
}
