using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	public partial class Canvas
	{
		class PathDrawable : Drawable, IWalkable
		{
			public Path PathObject;
			public CachedPath CachedPath;
			public int PathIndex;
			public bool CachedVertices;
			public bool GeneratedCachedPath;

			public void Walk(CanvasSceneWalker walker)
			{
				GlobalOrder = walker.AddItem();
				GroupOrder = walker.GetGroup();
				LayerOrder = walker.GetLayer();
			}

			public override Drawable Clone()
			{
				return new PathDrawable() { PathObject = PathObject, CachedPath = CachedPath, Transform = Transform };
			}
		}
	}
}
