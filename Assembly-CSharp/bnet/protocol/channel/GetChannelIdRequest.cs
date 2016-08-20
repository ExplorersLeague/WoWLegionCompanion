using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class GetChannelIdRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GetChannelIdRequest.Deserialize(stream, this);
		}

		public static GetChannelIdRequest Deserialize(Stream stream, GetChannelIdRequest instance)
		{
			return GetChannelIdRequest.Deserialize(stream, instance, -1L);
		}

		public static GetChannelIdRequest DeserializeLengthDelimited(Stream stream)
		{
			GetChannelIdRequest getChannelIdRequest = new GetChannelIdRequest();
			GetChannelIdRequest.DeserializeLengthDelimited(stream, getChannelIdRequest);
			return getChannelIdRequest;
		}

		public static GetChannelIdRequest DeserializeLengthDelimited(Stream stream, GetChannelIdRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetChannelIdRequest.Deserialize(stream, instance, num);
		}

		public static GetChannelIdRequest Deserialize(Stream stream, GetChannelIdRequest instance, long limit)
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
			GetChannelIdRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetChannelIdRequest instance)
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
			return obj is GetChannelIdRequest;
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetChannelIdRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetChannelIdRequest>(bs, 0, -1);
		}
	}
}
