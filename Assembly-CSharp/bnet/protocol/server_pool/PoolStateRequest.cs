using System;
using System.IO;

namespace bnet.protocol.server_pool
{
	public class PoolStateRequest : IProtoBuf
	{
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return obj is PoolStateRequest;
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static PoolStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PoolStateRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			PoolStateRequest.Deserialize(stream, this);
		}

		public static PoolStateRequest Deserialize(Stream stream, PoolStateRequest instance)
		{
			return PoolStateRequest.Deserialize(stream, instance, -1L);
		}

		public static PoolStateRequest DeserializeLengthDelimited(Stream stream)
		{
			PoolStateRequest poolStateRequest = new PoolStateRequest();
			PoolStateRequest.DeserializeLengthDelimited(stream, poolStateRequest);
			return poolStateRequest;
		}

		public static PoolStateRequest DeserializeLengthDelimited(Stream stream, PoolStateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PoolStateRequest.Deserialize(stream, instance, num);
		}

		public static PoolStateRequest Deserialize(Stream stream, PoolStateRequest instance, long limit)
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
			PoolStateRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, PoolStateRequest instance)
		{
		}

		public uint GetSerializedSize()
		{
			return 0u;
		}
	}
}
