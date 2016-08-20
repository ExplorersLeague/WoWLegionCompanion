using System;
using System.IO;

namespace bnet.protocol.game_master
{
	public class UnregisterUtilitiesRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			UnregisterUtilitiesRequest.Deserialize(stream, this);
		}

		public static UnregisterUtilitiesRequest Deserialize(Stream stream, UnregisterUtilitiesRequest instance)
		{
			return UnregisterUtilitiesRequest.Deserialize(stream, instance, -1L);
		}

		public static UnregisterUtilitiesRequest DeserializeLengthDelimited(Stream stream)
		{
			UnregisterUtilitiesRequest unregisterUtilitiesRequest = new UnregisterUtilitiesRequest();
			UnregisterUtilitiesRequest.DeserializeLengthDelimited(stream, unregisterUtilitiesRequest);
			return unregisterUtilitiesRequest;
		}

		public static UnregisterUtilitiesRequest DeserializeLengthDelimited(Stream stream, UnregisterUtilitiesRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnregisterUtilitiesRequest.Deserialize(stream, instance, num);
		}

		public static UnregisterUtilitiesRequest Deserialize(Stream stream, UnregisterUtilitiesRequest instance, long limit)
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
			UnregisterUtilitiesRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, UnregisterUtilitiesRequest instance)
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
			return obj is UnregisterUtilitiesRequest;
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static UnregisterUtilitiesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnregisterUtilitiesRequest>(bs, 0, -1);
		}
	}
}
