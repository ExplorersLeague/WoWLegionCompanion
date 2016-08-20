using System;
using System.IO;
using System.Text;

public static class ProtocolParser
{
	public static string ReadString(Stream stream)
	{
		return Encoding.UTF8.GetString(ProtocolParser.ReadBytes(stream));
	}

	public static byte[] ReadBytes(Stream stream)
	{
		int num = (int)ProtocolParser.ReadUInt32(stream);
		byte[] array = new byte[num];
		int num2;
		for (int i = 0; i < num; i += num2)
		{
			num2 = stream.Read(array, i, num - i);
			if (num2 == 0)
			{
				throw new ProtocolBufferException(string.Concat(new object[]
				{
					"Expected ",
					num - i,
					" got ",
					i
				}));
			}
		}
		return array;
	}

	public static void SkipBytes(Stream stream)
	{
		int num = (int)ProtocolParser.ReadUInt32(stream);
		if (stream.CanSeek)
		{
			stream.Seek((long)num, SeekOrigin.Current);
		}
		else
		{
			ProtocolParser.ReadBytes(stream);
		}
	}

	public static void WriteString(Stream stream, string val)
	{
		ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(val));
	}

	public static void WriteBytes(Stream stream, byte[] val)
	{
		ProtocolParser.WriteUInt32(stream, (uint)val.Length);
		stream.Write(val, 0, val.Length);
	}

	[Obsolete("Only for reference")]
	public static ulong ReadFixed64(BinaryReader reader)
	{
		return reader.ReadUInt64();
	}

	[Obsolete("Only for reference")]
	public static long ReadSFixed64(BinaryReader reader)
	{
		return reader.ReadInt64();
	}

	[Obsolete("Only for reference")]
	public static uint ReadFixed32(BinaryReader reader)
	{
		return reader.ReadUInt32();
	}

	[Obsolete("Only for reference")]
	public static int ReadSFixed32(BinaryReader reader)
	{
		return reader.ReadInt32();
	}

	[Obsolete("Only for reference")]
	public static void WriteFixed64(BinaryWriter writer, ulong val)
	{
		writer.Write(val);
	}

	[Obsolete("Only for reference")]
	public static void WriteSFixed64(BinaryWriter writer, long val)
	{
		writer.Write(val);
	}

	[Obsolete("Only for reference")]
	public static void WriteFixed32(BinaryWriter writer, uint val)
	{
		writer.Write(val);
	}

	[Obsolete("Only for reference")]
	public static void WriteSFixed32(BinaryWriter writer, int val)
	{
		writer.Write(val);
	}

	[Obsolete("Only for reference")]
	public static float ReadFloat(BinaryReader reader)
	{
		return reader.ReadSingle();
	}

	[Obsolete("Only for reference")]
	public static double ReadDouble(BinaryReader reader)
	{
		return reader.ReadDouble();
	}

	[Obsolete("Only for reference")]
	public static void WriteFloat(BinaryWriter writer, float val)
	{
		writer.Write(val);
	}

	[Obsolete("Only for reference")]
	public static void WriteDouble(BinaryWriter writer, double val)
	{
		writer.Write(val);
	}

	public static Key ReadKey(Stream stream)
	{
		uint num = ProtocolParser.ReadUInt32(stream);
		return new Key(num >> 3, (Wire)(num & 7u));
	}

	public static Key ReadKey(byte firstByte, Stream stream)
	{
		if (firstByte < 128)
		{
			return new Key((uint)(firstByte >> 3), (Wire)(firstByte & 7));
		}
		uint field = ProtocolParser.ReadUInt32(stream) << 4 | (uint)(firstByte >> 3 & 15);
		return new Key(field, (Wire)(firstByte & 7));
	}

	public static void WriteKey(Stream stream, Key key)
	{
		uint val = key.Field << 3 | (uint)key.WireType;
		ProtocolParser.WriteUInt32(stream, val);
	}

	public static void SkipKey(Stream stream, Key key)
	{
		switch (key.WireType)
		{
		case Wire.Varint:
			ProtocolParser.ReadSkipVarInt(stream);
			return;
		case Wire.Fixed64:
			stream.Seek(8L, SeekOrigin.Current);
			return;
		case Wire.LengthDelimited:
			stream.Seek((long)((ulong)ProtocolParser.ReadUInt32(stream)), SeekOrigin.Current);
			return;
		case Wire.Fixed32:
			stream.Seek(4L, SeekOrigin.Current);
			return;
		}
		throw new NotImplementedException("Unknown wire type: " + key.WireType);
	}

	public static byte[] ReadValueBytes(Stream stream, Key key)
	{
		int i = 0;
		switch (key.WireType)
		{
		case Wire.Varint:
			return ProtocolParser.ReadVarIntBytes(stream);
		case Wire.Fixed64:
		{
			byte[] array = new byte[8];
			while (i < 8)
			{
				i += stream.Read(array, i, 8 - i);
			}
			return array;
		}
		case Wire.LengthDelimited:
		{
			uint num = ProtocolParser.ReadUInt32(stream);
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				ProtocolParser.WriteUInt32(memoryStream, num);
				array = new byte[(ulong)num + (ulong)memoryStream.Length];
				memoryStream.ToArray().CopyTo(array, 0);
				i = (int)memoryStream.Length;
			}
			while (i < array.Length)
			{
				i += stream.Read(array, i, array.Length - i);
			}
			return array;
		}
		case Wire.Fixed32:
		{
			byte[] array = new byte[4];
			while (i < 4)
			{
				i += stream.Read(array, i, 4 - i);
			}
			return array;
		}
		}
		throw new NotImplementedException("Unknown wire type: " + key.WireType);
	}

	public static void ReadSkipVarInt(Stream stream)
	{
		for (;;)
		{
			int num = stream.ReadByte();
			if (num < 0)
			{
				break;
			}
			if ((num & 128) == 0)
			{
				return;
			}
		}
		throw new IOException("Stream ended too early");
	}

	public static byte[] ReadVarIntBytes(Stream stream)
	{
		byte[] array = new byte[10];
		int num = 0;
		for (;;)
		{
			int num2 = stream.ReadByte();
			if (num2 < 0)
			{
				break;
			}
			array[num] = (byte)num2;
			num++;
			if ((num2 & 128) == 0)
			{
				goto Block_2;
			}
			if (num >= array.Length)
			{
				goto Block_3;
			}
		}
		throw new IOException("Stream ended too early");
		Block_2:
		byte[] array2 = new byte[num];
		Array.Copy(array, array2, array2.Length);
		return array2;
		Block_3:
		throw new ProtocolBufferException("VarInt too long, more than 10 bytes");
	}

	[Obsolete("Use (int)ReadUInt64(stream); //yes 64")]
	public static int ReadInt32(Stream stream)
	{
		return (int)ProtocolParser.ReadUInt64(stream);
	}

	[Obsolete("Use WriteUInt64(stream, (ulong)val); //yes 64, negative numbers are encoded that way")]
	public static void WriteInt32(Stream stream, int val)
	{
		ProtocolParser.WriteUInt64(stream, (ulong)((long)val));
	}

	public static int ReadZInt32(Stream stream)
	{
		uint num = ProtocolParser.ReadUInt32(stream);
		return (int)(num >> 1 ^ (uint)((int)((int)num << 31) >> 31));
	}

	public static void WriteZInt32(Stream stream, int val)
	{
		ProtocolParser.WriteUInt32(stream, (uint)(val << 1 ^ val >> 31));
	}

	public static uint SizeOfZInt32(int val)
	{
		return ProtocolParser.SizeOfUInt32((uint)(val << 1 ^ val >> 31));
	}

	public static uint ReadUInt32(Stream stream)
	{
		uint num = 0u;
		for (int i = 0; i < 5; i++)
		{
			int num2 = stream.ReadByte();
			if (num2 < 0)
			{
				throw new IOException("Stream ended too early");
			}
			if (i == 4 && (num2 & 240) != 0)
			{
				throw new ProtocolBufferException("Got larger VarInt than 32bit unsigned");
			}
			if ((num2 & 128) == 0)
			{
				return num | (uint)((uint)num2 << 7 * i);
			}
			num |= (uint)((uint)(num2 & 127) << 7 * i);
		}
		throw new ProtocolBufferException("Got larger VarInt than 32bit unsigned");
	}

	public static void WriteUInt32(Stream stream, uint val)
	{
		byte b;
		for (;;)
		{
			b = (byte)(val & 127u);
			val >>= 7;
			if (val == 0u)
			{
				break;
			}
			b |= 128;
			stream.WriteByte(b);
		}
		stream.WriteByte(b);
	}

	public static uint SizeOfUInt32(int val)
	{
		return ProtocolParser.SizeOfUInt32((uint)val);
	}

	public static uint SizeOfUInt32(uint val)
	{
		uint num = 1u;
		for (;;)
		{
			val >>= 7;
			if (val == 0u)
			{
				break;
			}
			num += 1u;
		}
		return num;
	}

	[Obsolete("Use (long)ReadUInt64(stream); instead")]
	public static int ReadInt64(Stream stream)
	{
		return (int)ProtocolParser.ReadUInt64(stream);
	}

	[Obsolete("Use WriteUInt64 (stream, (ulong)val); instead")]
	public static void WriteInt64(Stream stream, int val)
	{
		ProtocolParser.WriteUInt64(stream, (ulong)((long)val));
	}

	public static long ReadZInt64(Stream stream)
	{
		ulong num = ProtocolParser.ReadUInt64(stream);
		return (long)(num >> 1 ^ num << 63 >> 63);
	}

	public static void WriteZInt64(Stream stream, long val)
	{
		ProtocolParser.WriteUInt64(stream, (ulong)(val << 1 ^ val >> 63));
	}

	public static uint SizeOfZInt64(long val)
	{
		return ProtocolParser.SizeOfUInt64((ulong)(val << 1 ^ val >> 63));
	}

	public static ulong ReadUInt64(Stream stream)
	{
		ulong num = 0UL;
		for (int i = 0; i < 10; i++)
		{
			int num2 = stream.ReadByte();
			if (num2 < 0)
			{
				throw new IOException("Stream ended too early");
			}
			if (i == 9 && (num2 & 254) != 0)
			{
				throw new ProtocolBufferException("Got larger VarInt than 64 bit unsigned");
			}
			if ((num2 & 128) == 0)
			{
				return num | (ulong)((ulong)((long)num2) << 7 * i);
			}
			num |= (ulong)((ulong)((long)(num2 & 127)) << 7 * i);
		}
		throw new ProtocolBufferException("Got larger VarInt than 64 bit unsigned");
	}

	public static void WriteUInt64(Stream stream, ulong val)
	{
		byte b;
		for (;;)
		{
			b = (byte)(val & 127UL);
			val >>= 7;
			if (val == 0UL)
			{
				break;
			}
			b |= 128;
			stream.WriteByte(b);
		}
		stream.WriteByte(b);
	}

	public static uint SizeOfUInt64(int val)
	{
		return ProtocolParser.SizeOfUInt64((ulong)((long)val));
	}

	public static uint SizeOfUInt64(ulong val)
	{
		uint num = 1u;
		for (;;)
		{
			val >>= 7;
			if (val == 0UL)
			{
				break;
			}
			num += 1u;
		}
		return num;
	}

	public static bool ReadBool(Stream stream)
	{
		int num = stream.ReadByte();
		if (num < 0)
		{
			throw new IOException("Stream ended too early");
		}
		if (num == 1)
		{
			return true;
		}
		if (num == 0)
		{
			return false;
		}
		throw new ProtocolBufferException("Invalid boolean value");
	}

	public static void WriteBool(Stream stream, bool val)
	{
		stream.WriteByte((!val) ? 0 : 1);
	}

	public static MemoryStreamStack Stack = new AllocationStack();
}
