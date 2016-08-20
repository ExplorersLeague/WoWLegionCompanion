using System;

namespace bgs
{
	public struct Error
	{
		public Error(BattleNetErrors code)
		{
			this.EnumVal = code;
		}

		public BattleNetErrors EnumVal { get; private set; }

		public uint Code
		{
			get
			{
				return (uint)this.EnumVal;
			}
		}

		public string Name
		{
			get
			{
				return this.EnumVal.ToString();
			}
		}

		public override string ToString()
		{
			return string.Format("{0} {1}", this.Code, this.Name);
		}

		public override bool Equals(object obj)
		{
			if (obj is BattleNetErrors)
			{
				return this.EnumVal == (BattleNetErrors)((uint)obj);
			}
			if (obj is Error)
			{
				return this.EnumVal == ((Error)obj).EnumVal;
			}
			if (obj is int)
			{
				return this.EnumVal == (BattleNetErrors)((int)obj);
			}
			if (obj is uint)
			{
				return this.EnumVal == (BattleNetErrors)((uint)obj);
			}
			if (obj is long)
			{
				return (ulong)this.EnumVal == (ulong)((long)obj);
			}
			if (obj is ulong)
			{
				return (ulong)this.EnumVal == (ulong)obj;
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return this.Code.GetHashCode();
		}

		public static implicit operator Error(BattleNetErrors code)
		{
			return new Error(code);
		}

		public static implicit operator Error(uint code)
		{
			return new Error((BattleNetErrors)code);
		}

		public static bool operator ==(Error a, BattleNetErrors b)
		{
			return a.EnumVal == b;
		}

		public static bool operator !=(Error a, BattleNetErrors b)
		{
			return a.EnumVal != b;
		}

		public static bool operator ==(Error a, uint b)
		{
			return a.EnumVal == (BattleNetErrors)b;
		}

		public static bool operator !=(Error a, uint b)
		{
			return a.EnumVal != (BattleNetErrors)b;
		}
	}
}
