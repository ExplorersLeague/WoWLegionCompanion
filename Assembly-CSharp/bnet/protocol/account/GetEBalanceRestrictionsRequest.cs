using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GetEBalanceRestrictionsRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GetEBalanceRestrictionsRequest.Deserialize(stream, this);
		}

		public static GetEBalanceRestrictionsRequest Deserialize(Stream stream, GetEBalanceRestrictionsRequest instance)
		{
			return GetEBalanceRestrictionsRequest.Deserialize(stream, instance, -1L);
		}

		public static GetEBalanceRestrictionsRequest DeserializeLengthDelimited(Stream stream)
		{
			GetEBalanceRestrictionsRequest getEBalanceRestrictionsRequest = new GetEBalanceRestrictionsRequest();
			GetEBalanceRestrictionsRequest.DeserializeLengthDelimited(stream, getEBalanceRestrictionsRequest);
			return getEBalanceRestrictionsRequest;
		}

		public static GetEBalanceRestrictionsRequest DeserializeLengthDelimited(Stream stream, GetEBalanceRestrictionsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetEBalanceRestrictionsRequest.Deserialize(stream, instance, num);
		}

		public static GetEBalanceRestrictionsRequest Deserialize(Stream stream, GetEBalanceRestrictionsRequest instance, long limit)
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
					if (num2 != 8)
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
			}
			if (stream.Position == limit)
			{
				return instance;
			}
			throw new ProtocolBufferException("Read past max limit");
		}

		public void Serialize(Stream stream)
		{
			GetEBalanceRestrictionsRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetEBalanceRestrictionsRequest instance)
		{
			if (instance.HasCurrencyHomeRegion)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.CurrencyHomeRegion);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasCurrencyHomeRegion)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.CurrencyHomeRegion);
			}
			return num;
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
			if (this.HasCurrencyHomeRegion)
			{
				num ^= this.CurrencyHomeRegion.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetEBalanceRestrictionsRequest getEBalanceRestrictionsRequest = obj as GetEBalanceRestrictionsRequest;
			return getEBalanceRestrictionsRequest != null && this.HasCurrencyHomeRegion == getEBalanceRestrictionsRequest.HasCurrencyHomeRegion && (!this.HasCurrencyHomeRegion || this.CurrencyHomeRegion.Equals(getEBalanceRestrictionsRequest.CurrencyHomeRegion));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetEBalanceRestrictionsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetEBalanceRestrictionsRequest>(bs, 0, -1);
		}

		public bool HasCurrencyHomeRegion;

		private uint _CurrencyHomeRegion;
	}
}
