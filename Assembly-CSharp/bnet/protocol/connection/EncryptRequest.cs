using System;
using System.IO;

namespace bnet.protocol.connection
{
	public class EncryptRequest : IProtoBuf
	{
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return obj is EncryptRequest;
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static EncryptRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<EncryptRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			EncryptRequest.Deserialize(stream, this);
		}

		public static EncryptRequest Deserialize(Stream stream, EncryptRequest instance)
		{
			return EncryptRequest.Deserialize(stream, instance, -1L);
		}

		public static EncryptRequest DeserializeLengthDelimited(Stream stream)
		{
			EncryptRequest encryptRequest = new EncryptRequest();
			EncryptRequest.DeserializeLengthDelimited(stream, encryptRequest);
			return encryptRequest;
		}

		public static EncryptRequest DeserializeLengthDelimited(Stream stream, EncryptRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return EncryptRequest.Deserialize(stream, instance, num);
		}

		public static EncryptRequest Deserialize(Stream stream, EncryptRequest instance, long limit)
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
			EncryptRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, EncryptRequest instance)
		{
		}

		public uint GetSerializedSize()
		{
			return 0u;
		}
	}
}
