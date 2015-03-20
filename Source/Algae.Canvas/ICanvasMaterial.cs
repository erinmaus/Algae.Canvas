using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	public interface ICanvasMaterial
	{
		void Use();

		void Prepare(Mesh mesh);

		void SetProjection(Matrix matrix);

		void SetColorTexture(Texture2D texture);

		void SetDepthTexture(Texture2D texture);

		void SetIndexRowLength(int length);
	}
}
