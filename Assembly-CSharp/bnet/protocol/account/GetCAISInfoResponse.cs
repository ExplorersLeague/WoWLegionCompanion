using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GetCAISInfoResponse : IProtoBuf
	{
		public CAIS CaisInfo
		{
			get
			{
				return this._CaisInfo;
			}
			set
			{
				this._CaisInfo = value;
				this.HasCaisInfo = (value != null);
			}
		}

		public void SetCaisInfo(CAIS val)
		{
			this.CaisInfo = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCaisInfo)
			{
				num ^= this.CaisInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetCAISInfoResponse getCAISInfoResponse = obj as GetCAISInfoResponse;
			return getCAISInfoResponse != null && this.HasCaisInfo == getCAISInfoResponse.HasCaisInfo && (!this.HasCaisInfo || this.CaisInfo.Equals(getCAISInfoResponse.CaisInfo));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetCAISInfoResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetCAISInfoResponse>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GetCAISInfoResponse.Deserialize(stream, this);
		}

		public static GetCAISInfoResponse Deserialize(Stream stream, GetCAISInfoResponse instance)
		{
			return GetCAISInfoResponse.Deserialize(stream, instance, -1L);
		}

		public static GetCAISInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			GetCAISInfoResponse getCAISInfoResponse = new GetCAISInfoResponse();
			GetCAISInfoResponse.DeserializeLengthDelimited(stream, getCAISInfoResponse);
			return getCAISInfoResponse;
		}

		public static GetCAISInfoResponse DeserializeLengthDelimited(Stream stream, GetCAISInfoResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetCAISInfoResponse.Deserialize(stream, instance, num);
		}

		public static GetCAISInfoResponse Deserialize(Stream stream, GetCAISInfoResponse instance, long limit)
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
				else if (instance.CaisInfo == null)
				{
					instance.CaisInfo = CAIS.DeserializeLengthDelimited(stream);
				}
				else
				{
					CAIS.DeserializeLengthDelimited(stream, instance.CaisInfo);
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
			GetCAISInfoResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetCAISInfoResponse instance)
		{
			if (instance.HasCaisInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.CaisInfo.GetSerializedSize());
				CAIS.Serialize(stream, instance.CaisInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasCaisInfo)
			{
				num += 1u;
				uint serializedSize = this.CaisInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		public bool HasCaisInfo;

		private CAIS _CaisInfo;
	}
}
