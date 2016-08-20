using System;
using System.IO;

namespace bnet.protocol.game_master
{
	public class UnregisterServerRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			UnregisterServerRequest.Deserialize(stream, this);
		}

		public static UnregisterServerRequest Deserialize(Stream stream, UnregisterServerRequest instance)
		{
			return UnregisterServerRequest.Deserialize(stream, instance, -1L);
		}

		public static UnregisterServerRequest DeserializeLengthDelimited(Stream stream)
		{
			UnregisterServerRequest unregisterServerRequest = new UnregisterServerRequest();
			UnregisterServerRequest.DeserializeLengthDelimited(stream, unregisterServerRequest);
			return unregisterServerRequest;
		}

		public static UnregisterServerRequest DeserializeLengthDelimited(Stream stream, UnregisterServerRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnregisterServerRequest.Deserialize(stream, instance, num);
		}

		public static UnregisterServerRequest Deserialize(Stream stream, UnregisterServerRequest instance, long limit)
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
			UnregisterServerRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, UnregisterServerRequest instance)
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
			return obj is UnregisterServerRequest;
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static UnregisterServerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnregisterServerRequest>(bs, 0, -1);
		}
	}
}
