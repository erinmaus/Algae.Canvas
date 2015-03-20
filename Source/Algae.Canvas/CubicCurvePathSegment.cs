using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	public class CubicCurvePathSegment : PathSegment
	{
		public override PathSegmentType SegmentType
		{
			get { return PathSegmentType.CubicCurve; }
		}

		public CubicCurvePathSegment(Vector2 control1, Vector2 control2, Vector2 position, bool isRelative = false)
			: base(3, isRelative)
		{
			this[0] = control1;
			this[1] = control2;
			this[2] = position;
		}
	}
}
