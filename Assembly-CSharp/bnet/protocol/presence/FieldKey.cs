using System;
using System.IO;

namespace bnet.protocol.presence
{
	public class FieldKey : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			FieldKey.Deserialize(stream, this);
		}

		public static FieldKey Deserialize(Stream stream, FieldKey instance)
		{
			return FieldKey.Deserialize(stream, instance, -1L);
		}

		public static FieldKey DeserializeLengthDelimited(Stream stream)
		{
			FieldKey fieldKey = new FieldKey();
			FieldKey.DeserializeLengthDelimited(stream, fieldKey);
			return fieldKey;
		}

		public static FieldKey DeserializeLengthDelimited(Stream stream, FieldKey instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FieldKey.Deserialize(stream, instance, num);
		}

		public static FieldKey Deserialize(Stream stream, FieldKey instance, long limit)
		{
			instance.Index = 0UL;
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
						if (num2 != 16)
						{
							if (num2 != 24)
							{
								if (num2 != 32)
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
									instance.Index = ProtocolParser.ReadUInt64(stream);
								}
							}
							else
							{
								instance.Field = ProtocolParser.ReadUInt32(stream);
							}
						}
						else
						{
							instance.Group = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.Program = ProtocolParser.ReadUInt32(stream);
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
			FieldKey.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, FieldKey instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Program);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.Group);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.Field);
			if (instance.HasIndex)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.Index);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(this.Program);
			num += ProtocolParser.SizeOfUInt32(this.Group);
			num += ProtocolParser.SizeOfUInt32(this.Field);
			if (this.HasIndex)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.Index);
			}
			return num + 3u;
		}

		public uint Program { get; set; }

		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		public uint Group { get; set; }

		public void SetGroup(uint val)
		{
			this.Group = val;
		}

		public uint Field { get; set; }

		public void SetField(uint val)
		{
			this.Field = val;
		}

		public ulong Index
		{
			get
			{
				return this._Index;
			}
			set
			{
				this._Index = value;
				this.HasIndex = true;
			}
		}

		public void SetIndex(ulong val)
		{
			this.Index = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Program.GetHashCode();
			num ^= this.Group.GetHashCode();
			num ^= this.Field.GetHashCode();
			if (this.HasIndex)
			{
				num ^= this.Index.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FieldKey fieldKey = obj as FieldKey;
			return fieldKey != null && this.Program.Equals(fieldKey.Program) && this.Group.Equals(fieldKey.Group) && this.Field.Equals(fieldKey.Field) && this.HasIndex == fieldKey.HasIndex && (!this.HasIndex || this.Index.Equals(fieldKey.Index));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static FieldKey ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FieldKey>(bs, 0, -1);
		}

		public bool HasIndex;

		private ulong _Index;
	}
}
