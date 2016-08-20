using System;
using System.IO;
using System.Text;

namespace bnet.protocol.profanity
{
	public class WordFilter : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			WordFilter.Deserialize(stream, this);
		}

		public static WordFilter Deserialize(Stream stream, WordFilter instance)
		{
			return WordFilter.Deserialize(stream, instance, -1L);
		}

		public static WordFilter DeserializeLengthDelimited(Stream stream)
		{
			WordFilter wordFilter = new WordFilter();
			WordFilter.DeserializeLengthDelimited(stream, wordFilter);
			return wordFilter;
		}

		public static WordFilter DeserializeLengthDelimited(Stream stream, WordFilter instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return WordFilter.Deserialize(stream, instance, num);
		}

		public static WordFilter Deserialize(Stream stream, WordFilter instance, long limit)
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
					int num2 = num;
					if (num2 != 10)
					{
						if (num2 != 18)
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
							instance.Regex = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.Type = ProtocolParser.ReadString(stream);
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
			WordFilter.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, WordFilter instance)
		{
			if (instance.Type == null)
			{
				throw new ArgumentNullException("Type", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Type));
			if (instance.Regex == null)
			{
				throw new ArgumentNullException("Regex", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Regex));
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Type);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Regex);
			num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			return num + 2u;
		}

		public string Type { get; set; }

		public void SetType(string val)
		{
			this.Type = val;
		}

		public string Regex { get; set; }

		public void SetRegex(string val)
		{
			this.Regex = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Type.GetHashCode();
			return num ^ this.Regex.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			WordFilter wordFilter = obj as WordFilter;
			return wordFilter != null && this.Type.Equals(wordFilter.Type) && this.Regex.Equals(wordFilter.Regex);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static WordFilter ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<WordFilter>(bs, 0, -1);
		}
	}
}
