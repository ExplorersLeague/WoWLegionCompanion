using System;
using System.Text;

namespace bgs
{
	[Serializable]
	public class FourCC
	{
		public FourCC()
		{
		}

		public FourCC(uint value)
		{
			this.m_value = value;
		}

		public FourCC(string stringVal)
		{
			this.SetString(stringVal);
		}

		public FourCC Clone()
		{
			FourCC fourCC = new FourCC();
			fourCC.CopyFrom(this);
			return fourCC;
		}

		public uint GetValue()
		{
			return this.m_value;
		}

		public void SetValue(uint val)
		{
			this.m_value = val;
		}

		public string GetString()
		{
			StringBuilder stringBuilder = new StringBuilder(4);
			for (int i = 24; i >= 0; i -= 8)
			{
				char c = (char)(this.m_value >> i & 255u);
				if (c != '\0')
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		public void SetString(string str)
		{
			this.m_value = 0u;
			int num = 0;
			while (num < str.Length && num < 4)
			{
				this.m_value = (this.m_value << 8 | (uint)((byte)str[num]));
				num++;
			}
		}

		public void CopyFrom(FourCC other)
		{
			this.m_value = other.m_value;
		}

		public static implicit operator FourCC(uint val)
		{
			return new FourCC(val);
		}

		public static bool operator ==(uint val, FourCC fourCC)
		{
			return !(fourCC == null) && val == fourCC.m_value;
		}

		public static bool operator ==(FourCC fourCC, uint val)
		{
			return !(fourCC == null) && fourCC.m_value == val;
		}

		public static bool operator !=(uint val, FourCC fourCC)
		{
			return !(val == fourCC);
		}

		public static bool operator !=(FourCC fourCC, uint val)
		{
			return !(fourCC == val);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			FourCC fourCC = obj as FourCC;
			return fourCC != null && this.m_value == fourCC.m_value;
		}

		public bool Equals(FourCC other)
		{
			return other != null && this.m_value == other.m_value;
		}

		public override int GetHashCode()
		{
			return this.m_value.GetHashCode();
		}

		public static bool operator ==(FourCC a, FourCC b)
		{
			return object.ReferenceEquals(a, b) || (a != null && b != null && a.m_value == b.m_value);
		}

		public static bool operator !=(FourCC a, FourCC b)
		{
			return !(a == b);
		}

		public override string ToString()
		{
			return this.GetString();
		}

		protected uint m_value;
	}
}
