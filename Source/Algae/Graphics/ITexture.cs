using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	interface ITexture
	{
		TextureFormat Format
		{
			get;
		}

		TextureFilterMode Filter
		{
			get;
			set;
		}

		int Anisotropy
		{
			get;
			set;
		}

		TextureRepeatMode Repeat
		{
			get;
			set;
		}

		TextureDepthMode DepthMode
		{
			get;
			set;
		}

		TextureDepthFunction DepthFunction
		{
			get;
			set;
		}

		void SetData<T>(T[] data, TextureFormat format, int level = 0)
			where T : struct;

		void GetData<T>(T[] data, TextureFormat format, int level = 0)
			where T : struct;

		void GenerateMipmaps();

		void Bind(int sampler);
	}
}
