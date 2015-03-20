using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae
{
	/// <summary>
	/// An enumeration representing the value of a frustum test.
	/// </summary>
	public enum FrustumResult
	{
		/// <summary>
		/// The object is inside the frustum.
		/// </summary>
		Inside,

		/// <summary>
		/// The object is outside the frustum.
		/// </summary>
		Outside,
		
		/// <summary>
		/// The object is intersecting 
		/// </summary>
		Intersecting
	}

	/// <summary>
	/// An immutable structure that represents a bounding frustum.
	/// </summary>
	public struct Frustum
	{
		Plane left;

		/// <summary>
		/// The left plane.
		/// </summary>
		public Plane Left
		{
			get { return left; }
		}

		Plane right;

		/// <summary>
		/// The right plane.
		/// </summary>
		public Plane Right
		{
			get { return right; }
		}

		Plane bottom;
		/// <summary>
		/// The bottom plane.
		/// </summary>
		public Plane Bottom
		{
			get { return bottom; }
		}

		Plane top;

		/// <summary>
		/// The top plane.
		/// </summary>
		public Plane Top
		{
			get { return top; }
		}

		Plane near;

		/// <summary>
		/// The near plane.
		/// </summary>
		public Plane Near
		{
			get { return near; }
		}

		Plane far;

		/// <summary>
		/// The far plane.
		/// </summary>
		public Plane Far
		{
			get { return far; }
		}

		/// <summary>
		/// Creates a frustum from six planes.
		/// </summary>
		/// <param name="left">The left plane.</param>
		/// <param name="right">The right plane.</param>
		/// <param name="top">The top plane.</param>
		/// <param name="bottom">The bottom plane.</param>
		/// <param name="near">The near plane.</param>
		/// <param name="far">The far plane.</param>
		public Frustum(Plane left, Plane right, Plane top, Plane bottom, Plane near, Plane far)
		{
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
			this.near = near;
			this.far = far;
		}

		/// <summary>
		/// Constructs a frustum from a matrix.
		/// </summary>
		/// <param name="matrix">A camera matrix</param>
		public Frustum(Matrix matrix)
		{
			Vector3 leftNormal = new Vector3(matrix.M41 + matrix.M11, matrix.M42 + matrix.M12, matrix.M43 + matrix.M13);
			float leftLength = leftNormal.Length();
			float leftD = matrix.M44 + matrix.M14;
			left = new Plane(leftNormal / leftLength, leftD / leftLength);

			Vector3 rightNormal = new Vector3(matrix.M41 - matrix.M11, matrix.M42 - matrix.M12, matrix.M43 - matrix.M13);
			float rightLength = rightNormal.Length();
			float rightD = matrix.M44 - matrix.M14;
			right = new Plane(rightNormal / rightLength, rightD / rightLength);

			Vector3 bottomNormal = new Vector3(matrix.M41 + matrix.M21, matrix.M42 + matrix.M22, matrix.M43 + matrix.M23);
			float bottomLength = bottomNormal.Length();
			float bottomD = matrix.M44 + matrix.M24;
			bottom = new Plane(bottomNormal / bottomLength, bottomD / bottomLength);

			Vector3 topNormal = new Vector3(matrix.M41 - matrix.M21, matrix.M42 - matrix.M22, matrix.M43 - matrix.M23);
			float topLength = topNormal.Length();
			float topD = matrix.M44 - matrix.M24;
			top = new Plane(topNormal / topLength, topD / topLength);

			Vector3 nearNormal = new Vector3(matrix.M41 + matrix.M31, matrix.M42 + matrix.M32, matrix.M43 + matrix.M33);
			float nearLength = nearNormal.Length();
			float nearD = matrix.M44 + matrix.M34;
			near = new Plane(nearNormal / nearLength, nearD / nearLength);

			Vector3 farNormal = new Vector3(matrix.M41 - matrix.M31, matrix.M42 - matrix.M32, matrix.M43 - matrix.M33);
			float farLength = farNormal.Length();
			float farD = matrix.M44 - matrix.M34;
			far = new Plane(farNormal / farLength, farD / farLength);
		}

		/// <summary>
		/// Tests a point and returns the result.
		/// </summary>
		/// <param name="point">The point to test.</param>
		/// <returns>The result of the test.</returns>
		public FrustumResult TestPoint(Vector3 point)
		{
			float l = left.Distance(point), r = right.Distance(point);
			float b = bottom.Distance(point), t = top.Distance(point);
			float n = near.Distance(point), f = far.Distance(point);

			if (l < 0 || r < 0 || b < 0 || t < 0 || n < 0 || f < 0)
			    return FrustumResult.Outside;

			return FrustumResult.Inside;
		}

		/// <summary>
		/// Tests a sphere and returns the result.
		/// </summary>
		/// <param name="center">The center point of the sphere to test.</param>
		/// <param name="radius">The radius of the sphere.</param>
		/// <returns>The result of the test.</returns>
		public FrustumResult TestSphere(Vector3 center, float radius)
		{
			float l = left.Distance(center), r = right.Distance(center);
			float b = bottom.Distance(center), t = top.Distance(center);
			float n = near.Distance(center), f = far.Distance(center);

			if (l < -radius || r < -radius || b < -radius || t < -radius || n < -radius || f < -radius)
				return FrustumResult.Outside;

			return FrustumResult.Inside;
		}

		/// <summary>
		/// Tests a bounding box and returns the result.
		/// </summary>
		/// <param name="box">The bounding box to test.</param>
		/// <returns>The result of the test.</returns>
		public FrustumResult TestBoundingBox(BoundingBox box)
		{
			FrustumResult l = box.TestPlane(left), r = box.TestPlane(right);
			FrustumResult b = box.TestPlane(bottom), t = box.TestPlane(top);
			FrustumResult n = box.TestPlane(near), f = box.TestPlane(far);

			if (l == FrustumResult.Outside || r == FrustumResult.Outside
				|| b == FrustumResult.Outside || t == FrustumResult.Outside
				|| n == FrustumResult.Outside || f == FrustumResult.Outside)
			{
				return FrustumResult.Outside;
			}

			return FrustumResult.Inside;
		}
	}
}
