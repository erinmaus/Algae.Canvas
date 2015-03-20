using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace CommaExcess.Algae.Graphics.Lvg
{
	public class LvgPathDrawable : LvgDrawable
	{
		Path path;

		internal LvgPathDrawable(Matrix parent, XElement element)
			: base(element)
		{
			path = ParsePath(element.Attribute("d").Value);
			GlobalTransform = parent;
		}

		protected static Path ParsePath(string value)
		{
			var commands = Regex.Split(value, @"(?=[mMlLcCqQtzZ])").Where(c => !String.IsNullOrWhiteSpace(c));
			Path path = new Path();

			foreach (string command in commands)
			{
				float[] values =
					command.Substring(1).Split(' ')
					.Where(v => !String.IsNullOrWhiteSpace(v))
					.Select(v => Single.Parse(v, System.Globalization.CultureInfo.InvariantCulture))
					.ToArray();

				bool isRelative = Char.IsLower(command, 0);

				switch (command[0])
				{
					case 'm':
					case 'M':
						path.MoveTo(new Vector2(values[0], values[1]), isRelative);
						break;
					case 'l':
					case 'L':
						path.LineTo(new Vector2(values[0], values[1]), isRelative);
						break;
					case 'q':
					case 'Q':
						path.QuadraticCurveTo(new Vector2(values[0], values[1]), new Vector2(values[2], values[3]), isRelative);
						break;
					case 'c':
					case 'C':
						path.CubicCurveTo(new Vector2(values[0], values[1]), new Vector2(values[2], values[3]), new Vector2(values[4], values[5]), isRelative);
						break;
					case 'z':
					case 'Z':
						path.End();
						break;
				}
			}

			return path;
		}

		public override void Draw(Canvas canvas)
		{
			canvas.Paint(path, Fill, LocalTransform);
		}

		public override void Dispose()
		{
			if (path.Mesh != null)
				path.Dispose();
		}
	}
}
