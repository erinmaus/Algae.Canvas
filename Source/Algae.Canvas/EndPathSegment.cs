using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	public class EndPathSegment : PathSegment
	{
		public override PathSegmentType SegmentType
		{
			get { return PathSegmentType.End; }
		}

		public EndPathSegment()
			: base(0, false)
		{
		}
	}
}
