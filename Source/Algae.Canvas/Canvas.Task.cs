using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CommaExcess.Algae.Graphics
{
	public partial class Canvas
	{
		abstract class Task
		{
			public abstract void PerformTask();
		}

		class DisposingTask : Task
		{
			public override void PerformTask()
			{
				// Nothing.
			}
		}

		class DoneTask : Task
		{
			public override void PerformTask()
			{
				// Nothing.
			}
		}

		class TransformPathTask : Task
		{
			PathDrawable path;
			int lookup;
			uint index;

			public TransformPathTask(PathDrawable path, int lookup, uint index)
			{
				this.path = path;
				this.lookup = lookup;
				this.index = index;
			}

			public override void PerformTask()
			{
				var vertices = path.PathObject.CachedMesh.TransformVertices(path.GlobalTransform, lookup);
				var indices = path.PathObject.CachedMesh.TransformIndices(index);

				path.CachedPath = new CachedPath(vertices, indices);
			}
		}
	}
}
