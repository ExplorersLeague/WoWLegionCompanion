using System;
using System.IO;

namespace bnet.protocol
{
	public class NoData : IProtoBuf
	{
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return obj is NoData;
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static NoData ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<NoData>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			NoData.Deserialize(stream, this);
		}

		public static NoData Deserialize(Stream stream, NoData instance)
		{
			return NoData.Deserialize(stream, instance, -1L);
		}

		public static NoData DeserializeLengthDelimited(Stream stream)
		{
			NoData noData = new NoData();
			NoData.DeserializeLengthDelimited(stream, noData);
			return noData;
		}

		public static NoData DeserializeLengthDelimited(Stream stream, NoData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return NoData.Deserialize(stream, instance, num);
		}

		public static NoData Deserialize(Stream stream, NoData instance, long limit)
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
			NoData.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, NoData instance)
		{
		}

		public uint GetSerializedSize()
		{
			return 0u;
		}
	}
}
