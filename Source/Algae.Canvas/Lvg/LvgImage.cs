using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CommaExcess.Algae.Graphics.Lvg
{
	public class LvgImage : IDisposable
	{
		LvgGroupDrawable root;
		Dictionary<string, LvgGroupDrawable> groups = new Dictionary<string, LvgGroupDrawable>();

		LvgImage(XElement element)
		{
			root = new LvgGroupDrawable(Matrix.Identity, element, this);
		}

		internal void AddGroup(string id, LvgGroupDrawable group)
		{
			if (!groups.ContainsKey(id))
				groups.Add(id, group);
		}

		public LvgDrawable GetGroup(string name)
		{
			LvgGroupDrawable group;

			groups.TryGetValue(name, out group);

			return group;
		}

		public void Draw(Canvas canvas)
		{
			root.Draw(canvas);
		}

		public void Draw(Canvas canvas, Matrix transform, Color color)
		{
			root.Draw(canvas, transform, color);
		}

		public static LvgImage Load(Stream stream)
		{
			XDocument document = XDocument.Load(stream);

			return new LvgImage(document.Root);
		}

		public void Dispose()
		{
			root.Dispose();
		}
	}
}
