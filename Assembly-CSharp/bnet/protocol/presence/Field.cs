using System;
using System.IO;
using bnet.protocol.attribute;

namespace bnet.protocol.presence
{
	public class Field : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			Field.Deserialize(stream, this);
		}

		public static Field Deserialize(Stream stream, Field instance)
		{
			return Field.Deserialize(stream, instance, -1L);
		}

		public static Field DeserializeLengthDelimited(Stream stream)
		{
			Field field = new Field();
			Field.DeserializeLengthDelimited(stream, field);
			return field;
		}

		public static Field DeserializeLengthDelimited(Stream stream, Field instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Field.Deserialize(stream, instance, num);
		}

		public static Field Deserialize(Stream stream, Field instance, long limit)
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
				else if (num != 10)
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
					else if (instance.Value == null)
					{
						instance.Value = Variant.DeserializeLengthDelimited(stream);
					}
					else
					{
						Variant.DeserializeLengthDelimited(stream, instance.Value);
					}
				}
				else if (instance.Key == null)
				{
					instance.Key = FieldKey.DeserializeLengthDelimited(stream);
				}
				else
				{
					FieldKey.DeserializeLengthDelimited(stream, instance.Key);
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
			Field.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Field instance)
		{
			if (instance.Key == null)
			{
				throw new ArgumentNullException("Key", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Key.GetSerializedSize());
			FieldKey.Serialize(stream, instance.Key);
			if (instance.Value == null)
			{
				throw new ArgumentNullException("Value", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Value.GetSerializedSize());
			Variant.Serialize(stream, instance.Value);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.Key.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			uint serializedSize2 = this.Value.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			return num + 2u;
		}

		public FieldKey Key { get; set; }

		public void SetKey(FieldKey val)
		{
			this.Key = val;
		}

		public Variant Value { get; set; }

		public void SetValue(Variant val)
		{
			this.Value = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Key.GetHashCode();
			return num ^ this.Value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Field field = obj as Field;
			return field != null && this.Key.Equals(field.Key) && this.Value.Equals(field.Value);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static Field ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Field>(bs, 0, -1);
		}
	}
}
