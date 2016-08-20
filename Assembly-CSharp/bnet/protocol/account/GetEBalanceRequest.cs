using System;
using System.IO;
using System.Text;

namespace bnet.protocol.account
{
	public class GetEBalanceRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GetEBalanceRequest.Deserialize(stream, this);
		}

		public static GetEBalanceRequest Deserialize(Stream stream, GetEBalanceRequest instance)
		{
			return GetEBalanceRequest.Deserialize(stream, instance, -1L);
		}

		public static GetEBalanceRequest DeserializeLengthDelimited(Stream stream)
		{
			GetEBalanceRequest getEBalanceRequest = new GetEBalanceRequest();
			GetEBalanceRequest.DeserializeLengthDelimited(stream, getEBalanceRequest);
			return getEBalanceRequest;
		}

		public static GetEBalanceRequest DeserializeLengthDelimited(Stream stream, GetEBalanceRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetEBalanceRequest.Deserialize(stream, instance, num);
		}

		public static GetEBalanceRequest Deserialize(Stream stream, GetEBalanceRequest instance, long limit)
		{
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
				else
				{
					int num2 = num;
					if (num2 != 10)
					{
						if (num2 != 18)
						{
							if (num2 != 24)
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
								instance.CurrencyHomeRegion = ProtocolParser.ReadUInt32(stream);
							}
						}
						else
						{
							instance.Currency = ProtocolParser.ReadString(stream);
						}
					}
					else if (instance.AccountId == null)
					{
						instance.AccountId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.AccountId);
					}
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
			GetEBalanceRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetEBalanceRequest instance)
		{
			if (instance.AccountId == null)
			{
				throw new ArgumentNullException("AccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
			AccountId.Serialize(stream, instance.AccountId);
			if (instance.Currency == null)
			{
				throw new ArgumentNullException("Currency", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Currency));
			if (instance.HasCurrencyHomeRegion)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.CurrencyHomeRegion);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.AccountId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Currency);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.HasCurrencyHomeRegion)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.CurrencyHomeRegion);
			}
			return num + 2u;
		}

		public AccountId AccountId { get; set; }

		public void SetAccountId(AccountId val)
		{
			this.AccountId = val;
		}

		public string Currency { get; set; }

		public void SetCurrency(string val)
		{
			this.Currency = val;
		}

		public uint CurrencyHomeRegion
		{
			get
			{
				return this._CurrencyHomeRegion;
			}
			set
			{
				this._CurrencyHomeRegion = value;
				this.HasCurrencyHomeRegion = true;
			}
		}

		public void SetCurrencyHomeRegion(uint val)
		{
			this.CurrencyHomeRegion = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.AccountId.GetHashCode();
			num ^= this.Currency.GetHashCode();
			if (this.HasCurrencyHomeRegion)
			{
				num ^= this.CurrencyHomeRegion.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetEBalanceRequest getEBalanceRequest = obj as GetEBalanceRequest;
			return getEBalanceRequest != null && this.AccountId.Equals(getEBalanceRequest.AccountId) && this.Currency.Equals(getEBalanceRequest.Currency) && this.HasCurrencyHomeRegion == getEBalanceRequest.HasCurrencyHomeRegion && (!this.HasCurrencyHomeRegion || this.CurrencyHomeRegion.Equals(getEBalanceRequest.CurrencyHomeRegion));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetEBalanceRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetEBalanceRequest>(bs, 0, -1);
		}

		public bool HasCurrencyHomeRegion;

		private uint _CurrencyHomeRegion;
	}
}
