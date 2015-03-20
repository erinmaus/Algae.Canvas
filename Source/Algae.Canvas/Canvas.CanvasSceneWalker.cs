using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	public partial class Canvas
	{
		class CanvasSceneWalker
		{
			int items = 0;
			int group = 0;
			int layer = 0;
			int currentLayer = 0;

			public void Prepare()
			{
				Reset();
			}

			public void Reset()
			{
				items = 0;
				group = 0;
				layer = 0;
				currentLayer = 0;
			}

			public int AddItem()
			{
				return ++items;
			}

			public void AddLayer()
			{
				++group;
				++currentLayer;

				layer = Math.Max(currentLayer, layer);
			}

			public void RemoveLayer()
			{
				--currentLayer;
			}

			public int GetGroup()
			{
				return group;
			}

			public int GetLayer()
			{
				return layer;
			}

			public float GetDepth(int depthLevel)
			{
				return 1.0f - ((float)depthLevel / items);
			}
		}

		interface IWalkable
		{
			void Walk(CanvasSceneWalker walker);
		}
	}
}
