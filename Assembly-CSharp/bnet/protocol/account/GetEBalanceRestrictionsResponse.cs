using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account
{
	public class GetEBalanceRestrictionsResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GetEBalanceRestrictionsResponse.Deserialize(stream, this);
		}

		public static GetEBalanceRestrictionsResponse Deserialize(Stream stream, GetEBalanceRestrictionsResponse instance)
		{
			return GetEBalanceRestrictionsResponse.Deserialize(stream, instance, -1L);
		}

		public static GetEBalanceRestrictionsResponse DeserializeLengthDelimited(Stream stream)
		{
			GetEBalanceRestrictionsResponse getEBalanceRestrictionsResponse = new GetEBalanceRestrictionsResponse();
			GetEBalanceRestrictionsResponse.DeserializeLengthDelimited(stream, getEBalanceRestrictionsResponse);
			return getEBalanceRestrictionsResponse;
		}

		public static GetEBalanceRestrictionsResponse DeserializeLengthDelimited(Stream stream, GetEBalanceRestrictionsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetEBalanceRestrictionsResponse.Deserialize(stream, instance, num);
		}

		public static GetEBalanceRestrictionsResponse Deserialize(Stream stream, GetEBalanceRestrictionsResponse instance, long limit)
		{
			if (instance.CurrencyRestrictions == null)
			{
				instance.CurrencyRestrictions = new List<CurrencyRestriction>();
			}
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else if (num != 10)
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0u)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
				else
				{
					instance.CurrencyRestrictions.Add(CurrencyRestriction.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position == limit)
			{
				return instance;
			}
			throw new ProtocolBufferException("Read past max limit");
		}

		public void Serialize(Stream stream)
		{
			GetEBalanceRestrictionsResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetEBalanceRestrictionsResponse instance)
		{
			if (instance.CurrencyRestrictions.Count > 0)
			{
				foreach (CurrencyRestriction currencyRestriction in instance.CurrencyRestrictions)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, currencyRestriction.GetSerializedSize());
					CurrencyRestriction.Serialize(stream, currencyRestriction);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.CurrencyRestrictions.Count > 0)
			{
				foreach (CurrencyRestriction currencyRestriction in this.CurrencyRestrictions)
				{
					num += 1u;
					uint serializedSize = currencyRestriction.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		public List<CurrencyRestriction> CurrencyRestrictions
		{
			get
			{
				return this._CurrencyRestrictions;
			}
			set
			{
				this._CurrencyRestrictions = value;
			}
		}

		public List<CurrencyRestriction> CurrencyRestrictionsList
		{
			get
			{
				return this._CurrencyRestrictions;
			}
		}

		public int CurrencyRestrictionsCount
		{
			get
			{
				return this._CurrencyRestrictions.Count;
			}
		}

		public void AddCurrencyRestrictions(CurrencyRestriction val)
		{
			this._CurrencyRestrictions.Add(val);
		}

		public void ClearCurrencyRestrictions()
		{
			this._CurrencyRestrictions.Clear();
		}

		public void SetCurrencyRestrictions(List<CurrencyRestriction> val)
		{
			this.CurrencyRestrictions = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (CurrencyRestriction currencyRestriction in this.CurrencyRestrictions)
			{
				num ^= currencyRestriction.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetEBalanceRestrictionsResponse getEBalanceRestrictionsResponse = obj as GetEBalanceRestrictionsResponse;
			if (getEBalanceRestrictionsResponse == null)
			{
				return false;
			}
			if (this.CurrencyRestrictions.Count != getEBalanceRestrictionsResponse.CurrencyRestrictions.Count)
			{
				return false;
			}
			for (int i = 0; i < this.CurrencyRestrictions.Count; i++)
			{
				if (!this.CurrencyRestrictions[i].Equals(getEBalanceRestrictionsResponse.CurrencyRestrictions[i]))
				{
					return false;
				}
			}
			return true;
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetEBalanceRestrictionsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetEBalanceRestrictionsResponse>(bs, 0, -1);
		}

		private List<CurrencyRestriction> _CurrencyRestrictions = new List<CurrencyRestriction>();
	}
}
