using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	public class LinePathSegment : PathSegment
	{
		public override PathSegmentType SegmentType
		{
			get { return PathSegmentType.Line; }
		}

		public LinePathSegment(Vector2 position, bool isRelative = false)
			: base(1, isRelative)
		{
			this[0] = position;
		}
	}
}
