using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	public class QuadraticCurvePathSegment : PathSegment
	{
		public override PathSegmentType SegmentType
		{
			get { return PathSegmentType.QuadraticCurve; }
		}

		public QuadraticCurvePathSegment(Vector2 control, Vector2 position, bool isRelative = false)
			: base(2, isRelative)
		{
			this[0] = control;
			this[1] = position;
		}
	}
}
