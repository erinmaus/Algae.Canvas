using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	public class AnchorPathSegment : PathSegment
	{
		public override PathSegmentType SegmentType
		{
			get { return PathSegmentType.Anchor; }
		}

		public AnchorPathSegment(Vector2 anchor, bool isRelative = false)
			: base(1, isRelative)
		{
			this[0] = anchor;
		}
	}
}
