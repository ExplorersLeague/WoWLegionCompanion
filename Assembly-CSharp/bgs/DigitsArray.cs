using System;
using System.Collections.Generic;

namespace bgs
{
	internal class DigitsArray
	{
		internal DigitsArray(int size)
		{
			this.Allocate(size, 0);
		}

		internal DigitsArray(int size, int used)
		{
			this.Allocate(size, used);
		}

		internal DigitsArray(uint[] copyFrom)
		{
			this.Allocate(copyFrom.Length);
			this.CopyFrom(copyFrom, 0, 0, copyFrom.Length);
			this.ResetDataUsed();
		}

		internal DigitsArray(DigitsArray copyFrom)
		{
			this.Allocate(copyFrom.Count, copyFrom.DataUsed);
			Array.Copy(copyFrom.m_data, 0, this.m_data, 0, copyFrom.Count);
		}

		internal static int DataSizeOf
		{
			get
			{
				return 4;
			}
		}

		internal static int DataSizeBits
		{
			get
			{
				return 32;
			}
		}

		public void Allocate(int size)
		{
			this.Allocate(size, 0);
		}

		public void Allocate(int size, int used)
		{
			this.m_data = new uint[size + 1];
			this.m_dataUsed = used;
		}

		internal void CopyFrom(uint[] source, int sourceOffset, int offset, int length)
		{
			Array.Copy(source, sourceOffset, this.m_data, 0, length);
		}

		internal void CopyTo(uint[] array, int offset, int length)
		{
			Array.Copy(this.m_data, 0, array, offset, length);
		}

		internal uint this[int index]
		{
			get
			{
				if (index < this.m_dataUsed)
				{
					return this.m_data[index];
				}
				return (!this.IsNegative) ? 0u : DigitsArray.AllBits;
			}
			set
			{
				this.m_data[index] = value;
			}
		}

		internal int DataUsed
		{
			get
			{
				return this.m_dataUsed;
			}
			set
			{
				this.m_dataUsed = value;
			}
		}

		internal int Count
		{
			get
			{
				return this.m_data.Length;
			}
		}

		internal bool IsZero
		{
			get
			{
				return this.m_dataUsed == 0 || (this.m_dataUsed == 1 && this.m_data[0] == 0u);
			}
		}

		internal bool IsNegative
		{
			get
			{
				return (this.m_data[this.m_data.Length - 1] & DigitsArray.HiBitSet) == DigitsArray.HiBitSet;
			}
		}

		internal void ResetDataUsed()
		{
			this.m_dataUsed = this.m_data.Length;
			if (this.IsNegative)
			{
				while (this.m_dataUsed > 1 && this.m_data[this.m_dataUsed - 1] == DigitsArray.AllBits)
				{
					this.m_dataUsed--;
				}
				this.m_dataUsed++;
			}
			else
			{
				while (this.m_dataUsed > 1 && this.m_data[this.m_dataUsed - 1] == 0u)
				{
					this.m_dataUsed--;
				}
				if (this.m_dataUsed == 0)
				{
					this.m_dataUsed = 1;
				}
			}
		}

		internal int ShiftRight(int shiftCount)
		{
			return DigitsArray.ShiftRight(this.m_data, shiftCount);
		}

		internal static int ShiftRight(uint[] buffer, int shiftCount)
		{
			int num = DigitsArray.DataSizeBits;
			int num2 = 0;
			int num3 = buffer.Length;
			while (num3 > 1 && buffer[num3 - 1] == 0u)
			{
				num3--;
			}
			for (int i = shiftCount; i > 0; i -= num)
			{
				if (i < num)
				{
					num = i;
					num2 = DigitsArray.DataSizeBits - num;
				}
				ulong num4 = 0UL;
				for (int j = num3 - 1; j >= 0; j--)
				{
					ulong num5 = (ulong)buffer[j] >> num;
					num5 |= num4;
					num4 = (ulong)buffer[j] << num2;
					buffer[j] = (uint)num5;
				}
			}
			while (num3 > 1 && buffer[num3 - 1] == 0u)
			{
				num3--;
			}
			return num3;
		}

		internal int ShiftLeft(int shiftCount)
		{
			return DigitsArray.ShiftLeft(this.m_data, shiftCount);
		}

		internal static int ShiftLeft(uint[] buffer, int shiftCount)
		{
			int num = DigitsArray.DataSizeBits;
			int num2 = buffer.Length;
			while (num2 > 1 && buffer[num2 - 1] == 0u)
			{
				num2--;
			}
			for (int i = shiftCount; i > 0; i -= num)
			{
				if (i < num)
				{
					num = i;
				}
				ulong num3 = 0UL;
				for (int j = 0; j < num2; j++)
				{
					ulong num4 = (ulong)buffer[j] << num;
					num4 |= num3;
					buffer[j] = (uint)(num4 & (ulong)DigitsArray.AllBits);
					num3 = num4 >> DigitsArray.DataSizeBits;
				}
				if (num3 != 0UL)
				{
					if (num2 + 1 > buffer.Length)
					{
						throw new OverflowException();
					}
					buffer[num2] = (uint)num3;
					num2++;
				}
			}
			return num2;
		}

		internal int ShiftLeftWithoutOverflow(int shiftCount)
		{
			List<uint> list = new List<uint>(this.m_data);
			int num = DigitsArray.DataSizeBits;
			for (int i = shiftCount; i > 0; i -= num)
			{
				if (i < num)
				{
					num = i;
				}
				ulong num2 = 0UL;
				for (int j = 0; j < list.Count; j++)
				{
					ulong num3 = (ulong)list[j] << num;
					num3 |= num2;
					list[j] = (uint)(num3 & (ulong)DigitsArray.AllBits);
					num2 = num3 >> DigitsArray.DataSizeBits;
				}
				if (num2 != 0UL)
				{
					list.Add(0u);
					list[list.Count - 1] = (uint)num2;
				}
			}
			this.m_data = new uint[list.Count];
			list.CopyTo(this.m_data);
			return this.m_data.Length;
		}

		private uint[] m_data;

		internal static readonly uint AllBits = uint.MaxValue;

		internal static readonly uint HiBitSet = 1u << DigitsArray.DataSizeBits - 1;

		private int m_dataUsed;
	}
}
