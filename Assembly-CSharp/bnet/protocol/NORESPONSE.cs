using System;
using System.IO;

namespace bnet.protocol
{
	public class NORESPONSE : IProtoBuf
	{
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return obj is NORESPONSE;
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static NORESPONSE ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<NORESPONSE>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			NORESPONSE.Deserialize(stream, this);
		}

		public static NORESPONSE Deserialize(Stream stream, NORESPONSE instance)
		{
			return NORESPONSE.Deserialize(stream, instance, -1L);
		}

		public static NORESPONSE DeserializeLengthDelimited(Stream stream)
		{
			NORESPONSE noresponse = new NORESPONSE();
			NORESPONSE.DeserializeLengthDelimited(stream, noresponse);
			return noresponse;
		}

		public static NORESPONSE DeserializeLengthDelimited(Stream stream, NORESPONSE instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return NORESPONSE.Deserialize(stream, instance, num);
		}

		public static NORESPONSE Deserialize(Stream stream, NORESPONSE instance, long limit)
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
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0u)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
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
			NORESPONSE.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, NORESPONSE instance)
		{
		}

		public uint GetSerializedSize()
		{
			return 0u;
		}
	}
}
