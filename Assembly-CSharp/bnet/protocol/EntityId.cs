using System;
using System.IO;

namespace bnet.protocol
{
	public class EntityId : IProtoBuf
	{
		public ulong High { get; set; }

		public void SetHigh(ulong val)
		{
			this.High = val;
		}

		public ulong Low { get; set; }

		public void SetLow(ulong val)
		{
			this.Low = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.High.GetHashCode();
			return num ^ this.Low.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			EntityId entityId = obj as EntityId;
			return entityId != null && this.High.Equals(entityId.High) && this.Low.Equals(entityId.Low);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static EntityId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<EntityId>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			EntityId.Deserialize(stream, this);
		}

		public static EntityId Deserialize(Stream stream, EntityId instance)
		{
			return EntityId.Deserialize(stream, instance, -1L);
		}

		public static EntityId DeserializeLengthDelimited(Stream stream)
		{
			EntityId entityId = new EntityId();
			EntityId.DeserializeLengthDelimited(stream, entityId);
			return entityId;
		}

		public static EntityId DeserializeLengthDelimited(Stream stream, EntityId instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return EntityId.Deserialize(stream, instance, num);
		}

		public static EntityId Deserialize(Stream stream, EntityId instance, long limit)
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
					if (num != 17)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						uint field = key.Field;
						if (field == 0u)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Low = binaryReader.ReadUInt64();
					}
				}
				else
				{
					instance.High = binaryReader.ReadUInt64();
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
			EntityId.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, EntityId instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.High);
			stream.WriteByte(17);
			binaryWriter.Write(instance.Low);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 8u;
			num += 8u;
			return num + 2u;
		}
	}
}
