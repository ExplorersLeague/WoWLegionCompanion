using System;
using System.IO;

namespace bnet.protocol.game_master
{
	public class FactoryUpdateNotification : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			FactoryUpdateNotification.Deserialize(stream, this);
		}

		public static FactoryUpdateNotification Deserialize(Stream stream, FactoryUpdateNotification instance)
		{
			return FactoryUpdateNotification.Deserialize(stream, instance, -1L);
		}

		public static FactoryUpdateNotification DeserializeLengthDelimited(Stream stream)
		{
			FactoryUpdateNotification factoryUpdateNotification = new FactoryUpdateNotification();
			FactoryUpdateNotification.DeserializeLengthDelimited(stream, factoryUpdateNotification);
			return factoryUpdateNotification;
		}

		public static FactoryUpdateNotification DeserializeLengthDelimited(Stream stream, FactoryUpdateNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FactoryUpdateNotification.Deserialize(stream, instance, num);
		}

		public static FactoryUpdateNotification Deserialize(Stream stream, FactoryUpdateNotification instance, long limit)
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
					int num2 = num;
					if (num2 != 8)
					{
						if (num2 != 18)
						{
							if (num2 != 29)
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
								instance.ProgramId = binaryReader.ReadUInt32();
							}
						}
						else if (instance.Description == null)
						{
							instance.Description = GameFactoryDescription.DeserializeLengthDelimited(stream);
						}
						else
						{
							GameFactoryDescription.DeserializeLengthDelimited(stream, instance.Description);
						}
					}
					else
					{
						instance.Op = (FactoryUpdateNotification.Types.Operation)ProtocolParser.ReadUInt64(stream);
					}
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
			FactoryUpdateNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, FactoryUpdateNotification instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Op));
			if (instance.Description == null)
			{
				throw new ArgumentNullException("Description", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Description.GetSerializedSize());
			GameFactoryDescription.Serialize(stream, instance.Description);
			if (instance.HasProgramId)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.ProgramId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Op));
			uint serializedSize = this.Description.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasProgramId)
			{
				num += 1u;
				num += 4u;
			}
			return num + 2u;
		}

		public FactoryUpdateNotification.Types.Operation Op { get; set; }

		public void SetOp(FactoryUpdateNotification.Types.Operation val)
		{
			this.Op = val;
		}

		public GameFactoryDescription Description { get; set; }

		public void SetDescription(GameFactoryDescription val)
		{
			this.Description = val;
		}

		public uint ProgramId
		{
			get
			{
				return this._ProgramId;
			}
			set
			{
				this._ProgramId = value;
				this.HasProgramId = true;
			}
		}

		public void SetProgramId(uint val)
		{
			this.ProgramId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Op.GetHashCode();
			num ^= this.Description.GetHashCode();
			if (this.HasProgramId)
			{
				num ^= this.ProgramId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FactoryUpdateNotification factoryUpdateNotification = obj as FactoryUpdateNotification;
			return factoryUpdateNotification != null && this.Op.Equals(factoryUpdateNotification.Op) && this.Description.Equals(factoryUpdateNotification.Description) && this.HasProgramId == factoryUpdateNotification.HasProgramId && (!this.HasProgramId || this.ProgramId.Equals(factoryUpdateNotification.ProgramId));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static FactoryUpdateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FactoryUpdateNotification>(bs, 0, -1);
		}

		public bool HasProgramId;

		private uint _ProgramId;

		public static class Types
		{
			public enum Operation
			{
				ADD = 1,
				REMOVE,
				CHANGE
			}
		}
	}
}
