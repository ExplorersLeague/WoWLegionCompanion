using System;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	public class Privilege : IProtoBuf
	{
		public string Name { get; set; }

		public void SetName(string val)
		{
			this.Name = val;
		}

		public uint Value { get; set; }

		public void SetValue(uint val)
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
			Privilege privilege = obj as Privilege;
			return privilege != null && this.Name.Equals(privilege.Name) && this.Value.Equals(privilege.Value);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static Privilege ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Privilege>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			Privilege.Deserialize(stream, this);
		}

		public static Privilege Deserialize(Stream stream, Privilege instance)
		{
			return Privilege.Deserialize(stream, instance, -1L);
		}

		public static Privilege DeserializeLengthDelimited(Stream stream)
		{
			Privilege privilege = new Privilege();
			Privilege.DeserializeLengthDelimited(stream, privilege);
			return privilege;
		}

		public static Privilege DeserializeLengthDelimited(Stream stream, Privilege instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Privilege.Deserialize(stream, instance, num);
		}

		public static Privilege Deserialize(Stream stream, Privilege instance, long limit)
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
					if (num != 16)
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
						instance.Value = ProtocolParser.ReadUInt32(stream);
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
			Privilege.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Privilege instance)
		{
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.Value);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += ProtocolParser.SizeOfUInt32(this.Value);
			return num + 2u;
		}
	}
}
