using System;
using System.IO;
using System.Text;

namespace bnet.protocol.attribute
{
	public class Attribute : IProtoBuf
	{
		public string Name { get; set; }

		public void SetName(string val)
		{
			this.Name = val;
		}

		public Variant Value { get; set; }

		public void SetValue(Variant val)
		{
			this.Value = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Name.GetHashCode();
			return num ^ this.Value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Attribute attribute = obj as Attribute;
			return attribute != null && this.Name.Equals(attribute.Name) && this.Value.Equals(attribute.Value);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static Attribute ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Attribute>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			Attribute.Deserialize(stream, this);
		}

		public static Attribute Deserialize(Stream stream, Attribute instance)
		{
			return Attribute.Deserialize(stream, instance, -1L);
		}

		public static Attribute DeserializeLengthDelimited(Stream stream)
		{
			Attribute attribute = new Attribute();
			Attribute.DeserializeLengthDelimited(stream, attribute);
			return attribute;
		}

		public static Attribute DeserializeLengthDelimited(Stream stream, Attribute instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Attribute.Deserialize(stream, instance, num);
		}

		public static Attribute Deserialize(Stream stream, Attribute instance, long limit)
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
				else
				{
					instance.Name = ProtocolParser.ReadString(stream);
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
			Attribute.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Attribute instance)
		{
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
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
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			uint serializedSize = this.Value.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			return num + 2u;
		}
	}
}
