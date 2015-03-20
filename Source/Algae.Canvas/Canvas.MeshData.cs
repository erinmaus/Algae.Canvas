using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Graphics
{
	public partial class Canvas
	{
		class MeshData<T>
			where T : struct
		{
			int offset;
			T[] data;

			public MeshData(int length = 0)
			{
				data = new T[length];
			}

			public void Set(int index, T value)
			{
				data[index] = value;
			}

			public void Append(T value)
			{
				Append(new T[] { value });
			}

			public void Append(T[] other)
			{
				int newLength = offset + other.Length;
				T[] d = data;

				if (data.Length < newLength)
				{
					d = new T[offset + other.Length];
					Array.Copy(data, d, offset);

					data = d;
				}

				Array.Copy(other, 0, d, offset, other.Length);
				offset = newLength;
			}

			public void Reset()
			{
				offset = 0;
			}

			public int GetOffset()
			{
				return offset;
			}

			public T[] Get()
			{
				return data;
			}
		}
	}
}
