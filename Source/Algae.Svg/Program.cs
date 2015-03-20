using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

using Svg;
using Svg.Pathing;

using SvgMatrix = System.Drawing.Drawing2D.Matrix;

namespace CommaExcess.Algae.Svg
{
	class Program
	{
		static string ParseMatrix(SvgMatrix m)
		{
			float a = m.Elements[0];
			float b = m.Elements[1];
			float c = m.Elements[2];
			float d = m.Elements[3];
			float e = m.Elements[4];
			float f = m.Elements[5];

			return String.Format("{0:N4} {1:N4} {2:N4} {3:N4} {4:N4} {5:N4}", a, b, c, d, e, f);
		}

		static string ParsePaint(SvgPaintServer paint)
		{
			int r, g, b;

			if (paint is SvgColourServer && paint != SvgColourServer.None && paint != SvgColourServer.Inherit && paint != SvgColourServer.NotSet)
			{
				SvgColourServer color = paint as SvgColourServer;
				r = color.Colour.R;
				g = color.Colour.G;
				b = color.Colour.B;
			}
			else
			{
				r = 255;
				g = 255;
				b = 255;
			}

			return String.Format("{0:X2}{1:X2}{2:X2}", r, g, b);
		}

		static string FormatFill(SvgPaintServer paint, float opacity)
		{
			return String.Format("{0} {1:N4}", ParsePaint(paint), opacity);
		}

		static void EmitPath(SvgPath path, XElement parentElement)
		{
			string transform = ParseMatrix(path.Transforms.GetMatrix());
			string fill = FormatFill(path.Fill, path.Opacity * path.FillOpacity);

			XElement pathElement = new XElement("p");
			pathElement.Add(new XAttribute("transform", transform));
			pathElement.Add(new XAttribute("fill", fill));

			StringBuilder d = new StringBuilder();
			foreach (var command in path.PathData)
			{
				if (command is SvgArcSegment)
					throw new Exception("arc command not supported");

				d.AppendFormat("{0} ", command.ToString());
			}
			pathElement.Add(new XAttribute("d", d.ToString().Trim()));

			parentElement.Add(pathElement);
		}

		static int identifierCount = 0;
		static void EmitGroup(SvgGroup group, XElement parentElement)
		{
			string transform = ParseMatrix(group.Transforms.GetMatrix());
			string fill = String.Format("FFFFFF {0:N4}", group.Opacity * group.FillOpacity);

			string id;
			if (String.IsNullOrEmpty(group.ID))
			{
				id = String.Format("lvg-group-{0}", identifierCount++);
			}
			else
			{
				id = group.ID;
			}

			XElement groupElement = new XElement("g");
			groupElement.Add(new XAttribute("id", id));
			groupElement.Add(new XAttribute("transform", transform));
			groupElement.Add(new XAttribute("fill", fill));

			foreach (var element in group.Children)
			{
				if (element is SvgGroup)
				{
					EmitGroup(element as SvgGroup, groupElement);
				}
				else if (element is SvgPath)
				{
					EmitPath(element as SvgPath, groupElement);
				}
			}

			parentElement.Add(groupElement);
		}

		static void Main(string[] args)
		{
			if (args.Length < 3)
			{
				Console.WriteLine("arguments: <group id> <input.svg> <output.lvg>");
				return;
			}

			try
			{
				SvgDocument document = SvgDocument.Open(args[1]);
				XDocument lvgDocument = new XDocument(new XElement("lvg"));

				SvgGroup group = document.GetElementById<SvgGroup>(args[0]);
				EmitGroup(group, lvgDocument.Root);

				lvgDocument.Save(args[2]);
			}
			catch (Exception e)
			{
				Console.Write(e.ToString());
			}
		}
	}
}