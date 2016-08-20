using System;
using System.IO;

namespace bnet.protocol.presence
{
	public class RichPresence : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			RichPresence.Deserialize(stream, this);
		}

		public static RichPresence Deserialize(Stream stream, RichPresence instance)
		{
			return RichPresence.Deserialize(stream, instance, -1L);
		}

		public static RichPresence DeserializeLengthDelimited(Stream stream)
		{
			RichPresence richPresence = new RichPresence();
			RichPresence.DeserializeLengthDelimited(stream, richPresence);
			return richPresence;
		}

		public static RichPresence DeserializeLengthDelimited(Stream stream, RichPresence instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RichPresence.Deserialize(stream, instance, num);
		}

		public static RichPresence Deserialize(Stream stream, RichPresence instance, long limit)
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
					switch (num2)
					{
					case 21:
						instance.StreamId = binaryReader.ReadUInt32();
						break;
					default:
						if (num2 != 13)
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
						break;
					case 24:
						instance.Index = ProtocolParser.ReadUInt32(stream);
						break;
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
			RichPresence.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, RichPresence instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.ProgramId);
			stream.WriteByte(21);
			binaryWriter.Write(instance.StreamId);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.Index);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 4u;
			num += 4u;
			num += ProtocolParser.SizeOfUInt32(this.Index);
			return num + 3u;
		}

		public uint ProgramId { get; set; }

		public void SetProgramId(uint val)
		{
			this.ProgramId = val;
		}

		public uint StreamId { get; set; }

		public void SetStreamId(uint val)
		{
			this.StreamId = val;
		}

		public uint Index { get; set; }

		public void SetIndex(uint val)
		{
			this.Index = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ProgramId.GetHashCode();
			num ^= this.StreamId.GetHashCode();
			return num ^ this.Index.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			RichPresence richPresence = obj as RichPresence;
			return richPresence != null && this.ProgramId.Equals(richPresence.ProgramId) && this.StreamId.Equals(richPresence.StreamId) && this.Index.Equals(richPresence.Index);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static RichPresence ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RichPresence>(bs, 0, -1);
		}
	}
}
