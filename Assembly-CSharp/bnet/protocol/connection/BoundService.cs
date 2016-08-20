using System;
using System.IO;

namespace bnet.protocol.connection
{
	public class BoundService : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			BoundService.Deserialize(stream, this);
		}

		public static BoundService Deserialize(Stream stream, BoundService instance)
		{
			return BoundService.Deserialize(stream, instance, -1L);
		}

		public static BoundService DeserializeLengthDelimited(Stream stream)
		{
			BoundService boundService = new BoundService();
			BoundService.DeserializeLengthDelimited(stream, boundService);
			return boundService;
		}

		public static BoundService DeserializeLengthDelimited(Stream stream, BoundService instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BoundService.Deserialize(stream, instance, num);
		}

		public static BoundService Deserialize(Stream stream, BoundService instance, long limit)
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
				else
				{
					switch (num)
					{
					case 13:
						instance.Hash = binaryReader.ReadUInt32();
						continue;
					case 16:
						instance.Id = ProtocolParser.ReadUInt32(stream);
						continue;
					}
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
			BoundService.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, BoundService instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.Hash);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.Id);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 4u;
			num += ProtocolParser.SizeOfUInt32(this.Id);
			return num + 2u;
		}

		public uint Hash { get; set; }

		public void SetHash(uint val)
		{
			this.Hash = val;
		}

		public uint Id { get; set; }

		public void SetId(uint val)
		{
			this.Id = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Hash.GetHashCode();
			return num ^ this.Id.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			BoundService boundService = obj as BoundService;
			return boundService != null && this.Hash.Equals(boundService.Hash) && this.Id.Equals(boundService.Id);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static BoundService ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BoundService>(bs, 0, -1);
		}
	}
}
