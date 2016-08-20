using System;
using System.IO;

namespace bnet.protocol.account
{
	public class FlagUpdateResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			FlagUpdateResponse.Deserialize(stream, this);
		}

		public static FlagUpdateResponse Deserialize(Stream stream, FlagUpdateResponse instance)
		{
			return FlagUpdateResponse.Deserialize(stream, instance, -1L);
		}

		public static FlagUpdateResponse DeserializeLengthDelimited(Stream stream)
		{
			FlagUpdateResponse flagUpdateResponse = new FlagUpdateResponse();
			FlagUpdateResponse.DeserializeLengthDelimited(stream, flagUpdateResponse);
			return flagUpdateResponse;
		}

		public static FlagUpdateResponse DeserializeLengthDelimited(Stream stream, FlagUpdateResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FlagUpdateResponse.Deserialize(stream, instance, num);
		}

		public static FlagUpdateResponse Deserialize(Stream stream, FlagUpdateResponse instance, long limit)
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
			FlagUpdateResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, FlagUpdateResponse instance)
		{
		}

		public uint GetSerializedSize()
		{
			return 0u;
		}

		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return obj is FlagUpdateResponse;
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static FlagUpdateResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FlagUpdateResponse>(bs, 0, -1);
		}
	}
}
