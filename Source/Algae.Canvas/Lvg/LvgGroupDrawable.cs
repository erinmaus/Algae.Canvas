using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CommaExcess.Algae.Graphics.Lvg
{
	public class LvgGroupDrawable : LvgDrawable
	{
		List<LvgDrawable> children = new List<LvgDrawable>();

		internal LvgGroupDrawable(Matrix parent, XElement element, LvgImage image)
			: base(element)
		{
			XAttribute idAttribute = element.Attribute("id");

			if (idAttribute != null)
				image.AddGroup(idAttribute.Value, this);

			foreach (var child in element.Elements())
			{
				if (child.Name == "g")
				{
					children.Add(new LvgGroupDrawable(parent * LocalTransform, child, image));
				}
				else if (child.Name == "p")
				{
					children.Add(new LvgPathDrawable(parent * LocalTransform, child));
				}
			}

			GlobalTransform = parent;
		}

		public override void Draw(Canvas canvas)
		{
			canvas.StartGroup(Fill, LocalTransform);

			foreach (var child in children)
			{
				child.Draw(canvas);
			}

			canvas.FinishGroup();
		}

		public override void Dispose()
		{
			foreach (var child in children)
			{
				child.Dispose();
			}
		}
	}
}
