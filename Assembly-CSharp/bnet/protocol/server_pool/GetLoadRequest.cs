using System;
using System.IO;

namespace bnet.protocol.server_pool
{
	public class GetLoadRequest : IProtoBuf
	{
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return obj is GetLoadRequest;
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetLoadRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetLoadRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GetLoadRequest.Deserialize(stream, this);
		}

		public static GetLoadRequest Deserialize(Stream stream, GetLoadRequest instance)
		{
			return GetLoadRequest.Deserialize(stream, instance, -1L);
		}

		public static GetLoadRequest DeserializeLengthDelimited(Stream stream)
		{
			GetLoadRequest getLoadRequest = new GetLoadRequest();
			GetLoadRequest.DeserializeLengthDelimited(stream, getLoadRequest);
			return getLoadRequest;
		}

		public static GetLoadRequest DeserializeLengthDelimited(Stream stream, GetLoadRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetLoadRequest.Deserialize(stream, instance, num);
		}

		public static GetLoadRequest Deserialize(Stream stream, GetLoadRequest instance, long limit)
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
			GetLoadRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetLoadRequest instance)
		{
		}

		public uint GetSerializedSize()
		{
			return 0u;
		}
	}
}
