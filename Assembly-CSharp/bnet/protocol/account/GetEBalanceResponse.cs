using System;
using System.IO;
using System.Text;

namespace bnet.protocol.account
{
	public class GetEBalanceResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GetEBalanceResponse.Deserialize(stream, this);
		}

		public static GetEBalanceResponse Deserialize(Stream stream, GetEBalanceResponse instance)
		{
			return GetEBalanceResponse.Deserialize(stream, instance, -1L);
		}

		public static GetEBalanceResponse DeserializeLengthDelimited(Stream stream)
		{
			GetEBalanceResponse getEBalanceResponse = new GetEBalanceResponse();
			GetEBalanceResponse.DeserializeLengthDelimited(stream, getEBalanceResponse);
			return getEBalanceResponse;
		}

		public static GetEBalanceResponse DeserializeLengthDelimited(Stream stream, GetEBalanceResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetEBalanceResponse.Deserialize(stream, instance, num);
		}

		public static GetEBalanceResponse Deserialize(Stream stream, GetEBalanceResponse instance, long limit)
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
						instance.Balance = ProtocolParser.ReadString(stream);
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
			GetEBalanceResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetEBalanceResponse instance)
		{
			if (instance.HasBalance)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Balance));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasBalance)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Balance);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		public string Balance
		{
			get
			{
				return this._Balance;
			}
			set
			{
				this._Balance = value;
				this.HasBalance = (value != null);
			}
		}

		public void SetBalance(string val)
		{
			this.Balance = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasBalance)
			{
				num ^= this.Balance.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetEBalanceResponse getEBalanceResponse = obj as GetEBalanceResponse;
			return getEBalanceResponse != null && this.HasBalance == getEBalanceResponse.HasBalance && (!this.HasBalance || this.Balance.Equals(getEBalanceResponse.Balance));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetEBalanceResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetEBalanceResponse>(bs, 0, -1);
		}

		public bool HasBalance;

		private string _Balance;
	}
}
