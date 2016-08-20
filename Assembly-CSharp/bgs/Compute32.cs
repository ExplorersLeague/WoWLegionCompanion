using System;
using System.Text;

namespace bgs
{
	public class Compute32
	{
		public static uint Hash(string str)
		{
			uint num = 2166136261u;
			foreach (byte b in Encoding.ASCII.GetBytes(str))
			{
				num ^= (uint)b;
				num *= 16777619u;
			}
			return num;
		}

		public const uint FNV_32_PRIME = 16777619u;

		public const uint COMPUTE_32_OFFSET = 2166136261u;
	}
}
