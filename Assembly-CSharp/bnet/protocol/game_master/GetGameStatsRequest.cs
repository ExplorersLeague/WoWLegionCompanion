using System;
using System.IO;
using bnet.protocol.attribute;

namespace bnet.protocol.game_master
{
	public class GetGameStatsRequest : IProtoBuf
	{
		public ulong FactoryId { get; set; }

		public void SetFactoryId(ulong val)
		{
			this.FactoryId = val;
		}

		public AttributeFilter Filter { get; set; }

		public void SetFilter(AttributeFilter val)
		{
			this.Filter = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.FactoryId.GetHashCode();
			return num ^ this.Filter.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GetGameStatsRequest getGameStatsRequest = obj as GetGameStatsRequest;
			return getGameStatsRequest != null && this.FactoryId.Equals(getGameStatsRequest.FactoryId) && this.Filter.Equals(getGameStatsRequest.Filter);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetGameStatsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameStatsRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GetGameStatsRequest.Deserialize(stream, this);
		}

		public static GetGameStatsRequest Deserialize(Stream stream, GetGameStatsRequest instance)
		{
			return GetGameStatsRequest.Deserialize(stream, instance, -1L);
		}

		public static GetGameStatsRequest DeserializeLengthDelimited(Stream stream)
		{
			GetGameStatsRequest getGameStatsRequest = new GetGameStatsRequest();
			GetGameStatsRequest.DeserializeLengthDelimited(stream, getGameStatsRequest);
			return getGameStatsRequest;
		}

		public static GetGameStatsRequest DeserializeLengthDelimited(Stream stream, GetGameStatsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGameStatsRequest.Deserialize(stream, instance, num);
		}

		public static GetGameStatsRequest Deserialize(Stream stream, GetGameStatsRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				else if (num != 9)
				{
					if (num != 18)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						uint field = key.Field;
						if (field == 0u)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.Filter == null)
					{
						instance.Filter = AttributeFilter.DeserializeLengthDelimited(stream);
					}
					else
					{
						AttributeFilter.DeserializeLengthDelimited(stream, instance.Filter);
					}
				}
				else
				{
					instance.FactoryId = binaryReader.ReadUInt64();
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
			GetGameStatsRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetGameStatsRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.FactoryId);
			if (instance.Filter == null)
			{
				throw new ArgumentNullException("Filter", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Filter.GetSerializedSize());
			AttributeFilter.Serialize(stream, instance.Filter);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 8u;
			uint serializedSize = this.Filter.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			return num + 2u;
		}
	}
}
